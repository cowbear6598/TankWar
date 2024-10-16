using System;
using Core.Network.Infrastructure.Views;
using TMPro;
using UnityEngine;

namespace Core.Menu.Infrastructure.UI
{
	public class UI_RoomPlayer : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _nameText;
		[SerializeField] private TextMeshProUGUI _readyText;

		private RoomPlayerView _roomPlayerView;

		public void SetRoomPlayerView(RoomPlayerView roomPlayerView)
		{
			_roomPlayerView = roomPlayerView;

			_nameText.text  = _roomPlayerView.PlayerName;
			_readyText.text = _roomPlayerView.IsReady ? "Ready" : "Not Ready";

			_roomPlayerView.SubscribeOnPlayerNameChanged(OnPlayerNameChanged);
			_roomPlayerView.SubscribeOnPlayerReadyChanged(OnPlayerReadyChanged);
		}

		private void OnPlayerNameChanged(string playerName) => _nameText.text = playerName;

		private void OnPlayerReadyChanged(bool isReady) => _readyText.text = isReady ? "Ready" : "Not Ready";
	}
}