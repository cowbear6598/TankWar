using System;
using System.Linq;
using Core.Menu.Common;
using Core.Network.Common;
using Core.Network.Infrastructure.Repositories;
using MessagePipe;
using Mirror;
using SoapTools.SceneController.Application.Repository;
using SoapTools.SceneController.Infrastructure;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Core.Menu.Infrastructure.Views
{
	public class LobbyController : NetworkBehaviour
	{
		[Inject] private readonly IPublisher<OnCountdownStarted>          _onCountdownStarted;
		[Inject] private readonly IPublisher<OnCountdownChanged>          _onCountdownChanged;
		[Inject] private readonly IPublisher<OnCountdownStopped>          _onCountdownStopped;
		[Inject] private readonly ISubscriber<OnPlayerReadyStatusChanged> _onPlayerReadyStatusChanged;

		[Inject] private readonly RoomPlayerRepository _roomPlayerRepository;
		[Inject] private readonly SceneRepository      _sceneRepository;

		[SerializeField] private AssetReference gameScene;

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

			if (isAllReady && !_isCountdownStarted) // 所有玩家準備好了，且倒數尚未開始
			{
				CountdownStart();
			}
			else if (!isAllReady && _isCountdownStarted) // 有玩家尚未準備好，且倒數已經開始
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

			GameStart();
		}

		private async void LoadGameScene()
		{
			await new SceneControllerBuilder(_sceneRepository)
			      .UnloadAllScenes()
			      .LoadScene(gameScene)
			      .Execute();
		}

		#region Network Event

		[Server]
		private void CountdownStart()
		{
			_countdown = 2;

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

		[Server]
		private void GameStart()
		{
			LoadGameScene();
			RpcGameStart();
		}

		[ClientRpc]
		private void RpcOnCountdownStarted()
		{
			if (NetworkServer.active)
				return;

			_onCountdownStarted.Publish(new OnCountdownStarted());
		}

		[ClientRpc]
		private void RpcOnCountdownStopped()
		{
			if (NetworkServer.active)
				return;

			_onCountdownStopped.Publish(new OnCountdownStopped());
		}

		[ClientRpc]
		private void RpcGameStart()
		{
			if (NetworkServer.active)
				return;

			LoadGameScene();
		}

		private void OnCountdownChanged(int _, int countdown)
		{
			_onCountdownChanged.Publish(new OnCountdownChanged(countdown));
		}

		#endregion
	}
}