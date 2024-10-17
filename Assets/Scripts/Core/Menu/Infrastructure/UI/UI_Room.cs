using System;
using System.Collections.Generic;
using Core.Menu.Common;
using Core.Menu.Domain;
using Core.Misc.UI;
using Core.Network.Common;
using MessagePipe;
using TMPro;
using UnityEngine;
using VContainer;

namespace Core.Menu.Infrastructure.UI
{
	public class UI_Room : UI_Panel
	{
		[Inject] private readonly ISubscriber<OnMenuStateChanged> _onMenuStateChanged;
		[Inject] private readonly ISubscriber<OnPlayerAdded>      _onPlayerAdded;
		[Inject] private readonly ISubscriber<OnPlayerRemoved>    _onPlayerRemoved;
		[Inject] private readonly ISubscriber<OnCountdownStarted> _onCountdownStarted;
		[Inject] private readonly ISubscriber<OnCountdownChanged> _onCountdownChanged;
		[Inject] private readonly ISubscriber<OnCountdownStopped> _onCountdownStopped;

		[SerializeField] private TextMeshProUGUI _countdownText;
		[SerializeField] private UI_RoomPlayer   _uiRoomPlayerPrefab;
		[SerializeField] private Transform       _roomPlayerParent;

		private readonly Dictionary<int, UI_RoomPlayer> _uiRoomPlayers = new();

		private IDisposable _subscription;

		private void OnEnable()
		{
			var bag = DisposableBag.CreateBuilder();

			_onMenuStateChanged.Subscribe(OnMenuStateChanged).AddTo(bag);
			_onPlayerAdded.Subscribe(OnPlayerAdded).AddTo(bag);
			_onPlayerRemoved.Subscribe(OnPlayerRemoved).AddTo(bag);
			_onCountdownStarted.Subscribe(OnCountdownStarted).AddTo(bag);
			_onCountdownChanged.Subscribe(OnCountdownChanged).AddTo(bag);
			_onCountdownStopped.Subscribe(OnCountdownStopped).AddTo(bag);

			_subscription = bag.Build();
		}

		private void OnDisable() => _subscription.Dispose();

		#region 事件

		private void OnPlayerAdded(OnPlayerAdded e)
		{
			var uiRoomPlayer = Instantiate(_uiRoomPlayerPrefab, _roomPlayerParent);

			uiRoomPlayer.Initialize(e.RoomPlayerView);

			_uiRoomPlayers.Add(e.ConnectionID, uiRoomPlayer);
		}

		private void OnPlayerRemoved(OnPlayerRemoved e)
		{
			if (!_uiRoomPlayers.TryGetValue(e.ConnectionID, out var uiRoomPlayer))
				return;

			Destroy(uiRoomPlayer.gameObject);
			_uiRoomPlayers.Remove(e.ConnectionID);
		}

		private void OnCountdownStarted(OnCountdownStarted e)
		{
			_countdownText.gameObject.SetActive(true);
		}

		private void OnCountdownChanged(OnCountdownChanged e)
		{
			_countdownText.text = e.Countdown.ToString();
		}

		private void OnCountdownStopped(OnCountdownStopped e)
		{
			_countdownText.gameObject.SetActive(false);
		}

		private void OnMenuStateChanged(OnMenuStateChanged e)
		{
			if (e.PrevState == MenuState.Room)
				Hide();
			else if (e.State == MenuState.Room)
				Show();
		}

		#endregion
	}
}