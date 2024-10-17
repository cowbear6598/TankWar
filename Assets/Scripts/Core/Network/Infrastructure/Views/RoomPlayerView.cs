using System;
using Core.Network.Common;
using Core.Network.Infrastructure.Repositories;
using Core.User.Domain.Adapters;
using MessagePipe;
using Mirror;
using VContainer;

namespace Core.Network.Infrastructure.Views
{
	public class RoomPlayerView : NetworkBehaviour
	{
		[Inject] private readonly IPublisher<OnPlayerReadyStatusChanged> _onPlayerReadyStatusChanged;

		[Inject] private readonly IUser                _user;
		[Inject] private readonly RoomPlayerRepository _roomPlayerRepository;

		[SyncVar]                                      private int    _connectionID;
		[SyncVar(hook = nameof(OnPlayerNameChanged))]  private string _playerName;
		[SyncVar(hook = nameof(OnReadyStatusChanged))] private bool   _isReady;

		private Action<string> onPlayerNameChanged;
		private Action<bool>   onReadyStatusChanged;

		private void Start()
		{
			_roomPlayerRepository.Add(_connectionID, this);

			if (!isLocalPlayer)
				return;

			CmdSetPlayerName(_user.Name);
		}

		private void OnDestroy()
		{
			_roomPlayerRepository.Remove(_connectionID);
		}

		public void SetConnectionID(int connectionID) => _connectionID = connectionID;

		[Command]
		private void CmdSetPlayerName(string playerName)
		{
			_playerName = playerName;
		}

		[Command]
		public void CmdSetReadyStatus()
		{
			_isReady = !_isReady;

			_onPlayerReadyStatusChanged.Publish(new OnPlayerReadyStatusChanged());
		}

		private void OnPlayerNameChanged(string _, string playerName)
			=> onPlayerNameChanged?.Invoke(playerName);

		private void OnReadyStatusChanged(bool _, bool isReady)
			=> onReadyStatusChanged?.Invoke(isReady);

		public void SubscribeOnPlayerNameChanged(Action<string> action) => onPlayerNameChanged += action;
		public void SubscribeOnReadyStatusChanged(Action<bool>  action) => onReadyStatusChanged += action;

		public string PlayerName => _playerName;
		public bool   IsReady    => _isReady;
	}
}