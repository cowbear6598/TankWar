using System;
using System.Collections.Generic;
using Core.Menu.Common;
using Core.Menu.Domain;
using Core.Misc.UI;
using Core.Network.Common;
using Core.Network.Infrastructure.Views;
using MessagePipe;
using UnityEngine;
using VContainer;

namespace Core.Menu.Infrastructure.UI
{
	public class UI_Room : UI_Panel
	{
		[Inject] private readonly ISubscriber<OnMenuStateChanged>  _onMenuStateChanged;
		[Inject] private readonly ISubscriber<OnRoomPlayerAdded>   _onRoomPlayerAdded;
		[Inject] private readonly ISubscriber<OnRoomPlayerRemoved> _onRoomPlayerRemoved;

		[SerializeField] private UI_RoomPlayer _uiRoomPlayerPrefab;
		[SerializeField] private Transform     _roomPlayerParent;

		private readonly Dictionary<uint, UI_RoomPlayer> _uiRoomPlayers = new();

		private IDisposable _subscription;

		private void OnEnable()
		{
			var bag = DisposableBag.CreateBuilder();

			_onMenuStateChanged.Subscribe(OnMenuStateChanged).AddTo(bag);
			_onRoomPlayerAdded.Subscribe(OnRoomPlayerAdded).AddTo(bag);
			_onRoomPlayerRemoved.Subscribe(OnRoomPlayerRemoved).AddTo(bag);

			_subscription = bag.Build();
		}

		private void OnDisable() => _subscription.Dispose();

		private void OnRoomPlayerAdded(OnRoomPlayerAdded e)
		{
			var uiRoomPlayer = Instantiate(_uiRoomPlayerPrefab, _roomPlayerParent.transform);

			uiRoomPlayer.SetRoomPlayerView(e.RoomPlayerView);

			_uiRoomPlayers.Add(e.RoomPlayerView.netId, uiRoomPlayer);
		}

		private void OnRoomPlayerRemoved(OnRoomPlayerRemoved e)
		{
			var uiRoomPlayer = _uiRoomPlayers[e.RoomPlayerView.netId];

			_uiRoomPlayers.Remove(e.RoomPlayerView.netId);

			Destroy(uiRoomPlayer.gameObject);
		}

		private void OnMenuStateChanged(OnMenuStateChanged e)
		{
			if (e.PrevState == MenuState.Room)
				Hide();
			else if (e.State == MenuState.Room)
				Show();
		}
	}
}