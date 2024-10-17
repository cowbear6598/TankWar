using System;
using System.Collections.Generic;
using Core.Menu.Common;
using Core.Menu.Domain;
using Core.Misc.UI;
using Core.Network.Common;
using MessagePipe;
using Mirror;
using UnityEngine;
using VContainer;

namespace Core.Menu.Infrastructure.UI
{
	public class UI_Room : UI_Panel
	{
		[Inject] private readonly ISubscriber<OnMenuStateChanged> _onMenuStateChanged;
		[Inject] private readonly ISubscriber<OnPlayerAdded>      _onPlayerAdded;

		[SerializeField] private UI_RoomPlayer _uiRoomPlayerPrefab;
		[SerializeField] private Transform     _roomPlayerParent;

		private readonly Dictionary<uint, UI_RoomPlayer> _uiRoomPlayers = new();

		private IDisposable _subscription;

		private void OnEnable()
		{
			var bag = DisposableBag.CreateBuilder();

			_onMenuStateChanged.Subscribe(OnMenuStateChanged).AddTo(bag);
			_onPlayerAdded.Subscribe(OnPlayerAdded).AddTo(bag);

			_subscription = bag.Build();
		}

		private void OnDisable() => _subscription.Dispose();

		#region 事件

		private void OnPlayerAdded(OnPlayerAdded e)
		{
			var uiRoomPlayer = Instantiate(_uiRoomPlayerPrefab, _roomPlayerParent);

			uiRoomPlayer.Initialize(e.RoomPlayerView);

			_uiRoomPlayers.Add(e.RoomPlayerView.netId, uiRoomPlayer);
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