using System;
using System.Linq;
using Core.Menu.Common;
using Core.Network.Common;
using Core.Network.Infrastructure.Repositories;
using MessagePipe;
using Mirror;
using UniRx;
using UnityEngine;
using VContainer;

namespace Core.Menu.Infrastructure.Views
{
	public class MenuManager : NetworkBehaviour
	{
		[Inject] private readonly IPublisher<OnCountdownStarted>          _onCountdownStarted;
		[Inject] private readonly IPublisher<OnCountdownChanged>          _onCountdownChanged;
		[Inject] private readonly IPublisher<OnCountdownStopped>          _onCountdownStopped;
		[Inject] private readonly ISubscriber<OnPlayerReadyStatusChanged> _onPlayerReadyStatusChanged;

		[Inject] private readonly RoomPlayerRepository _roomPlayerRepository;

		private IDisposable _subscription;
		private IDisposable _countdownTimer;

		[SyncVar(hook = nameof(OnCountdownChanged))] private int _countdown = -1;

		private bool _isCountdownStarted;

		private void OnEnable()  => _subscription = _onPlayerReadyStatusChanged.Subscribe(OnPlayerReadyStatusChanged);
		private void OnDisable() => _subscription.Dispose();

		private void OnPlayerReadyStatusChanged(OnPlayerReadyStatusChanged e)
		{
			var roomPlayerViews = _roomPlayerRepository.GetRoomPlayerViews();

			var isAllReady = roomPlayerViews.All(x => x.IsReady);

			if (isAllReady && !_isCountdownStarted)
			{
				CountdownStart();
			}
			else if (!isAllReady && _isCountdownStarted)
			{
				CountdownStop();
			}
		}

		private void CountdownTick(long _)
		{
			_countdown--;

			if (_countdown > 0)
				return;

			_countdownTimer?.Dispose();
			Debug.Log("Game Start");
		}

		#region Network Event

		[Server]
		private void CountdownStart()
		{
			_countdown = 10;

			_isCountdownStarted = true;

			_countdownTimer = Observable.Interval(TimeSpan.FromSeconds(1))
			                            .Subscribe(CountdownTick);

			RpcOnCountdownStarted();
		}

		[Server]
		private void CountdownStop()
		{
			_isCountdownStarted = false;

			_countdownTimer?.Dispose();

			_countdown = -1;

			RpcOnCountdownStopped();
		}

		[ClientRpc]
		private void RpcOnCountdownStarted()
		{
			_onCountdownStarted.Publish(new OnCountdownStarted());
		}

		[ClientRpc]
		private void RpcOnCountdownStopped()
		{
			_onCountdownStopped.Publish(new OnCountdownStopped());
		}

		private void OnCountdownChanged(int _, int countdown)
		{
			_onCountdownChanged.Publish(new OnCountdownChanged(countdown));
		}

		#endregion
	}
}