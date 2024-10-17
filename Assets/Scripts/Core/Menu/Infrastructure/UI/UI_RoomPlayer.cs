using Core.Network.Infrastructure.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Menu.Infrastructure.UI
{
	public class UI_RoomPlayer : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _nameText;
		[SerializeField] private TextMeshProUGUI _readyText;
		[SerializeField] private Button          _readyBtn;
		[SerializeField] private TextMeshProUGUI _readyBtnText;

		private RoomPlayerView _roomPlayerView;

		public void Initialize(RoomPlayerView roomPlayerView)
		{
			_roomPlayerView = roomPlayerView;

			if (!_roomPlayerView.isLocalPlayer)
				_readyBtn.gameObject.SetActive(false);

			_roomPlayerView.SubscribeOnPlayerNameChanged(OnPlayerNameChanged);
			_roomPlayerView.SubscribeOnReadyStatusChanged(OnReadyStatusChanged);

			OnPlayerNameChanged(_roomPlayerView.PlayerName);
			OnReadyStatusChanged(_roomPlayerView.IsReady);
		}

		private void OnPlayerNameChanged(string playerName)
		{
			_nameText.text = playerName;
		}

		private void OnReadyStatusChanged(bool isReady)
		{
			_readyBtnText.text = isReady ? "Cancel" : "Ready";
			_readyText.text    = isReady ? "Ready" : "Not Ready";
		}

		public void Button_Ready() => _roomPlayerView.CmdSetReadyStatus();
	}
}