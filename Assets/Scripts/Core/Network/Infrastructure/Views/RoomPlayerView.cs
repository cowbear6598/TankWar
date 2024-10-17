using System;
using Core.Network.Infrastructure.Repositories;
using Core.User.Domain.Adapters;
using Mirror;
using VContainer;

namespace Core.Network.Infrastructure.Views
{
	public class RoomPlayerView : NetworkBehaviour
	{
		[Inject] private readonly IUser                _user;
		[Inject] private readonly RoomPlayerRepository _roomPlayerRepository;

		[SyncVar(hook = nameof(OnPlayerNameChanged))]  public  string _playerName;
		[SyncVar(hook = nameof(OnReadyStatusChanged))] private bool   _isReady;

		private Action<string> onPlayerNameChanged;
		private Action<bool>   onReadyStatusChanged;

		private void Start()
		{
			_roomPlayerRepository.Add(netId, this);

			if (!isLocalPlayer)
				return;

			CmdSetPlayerName(_user.Name);
		}

		private void OnDestroy()
		{
			_roomPlayerRepository.Remove(netId);
		}

		[Command]
		private void CmdSetPlayerName(string playerName)
		{
			_playerName = playerName;
		}

		[Command]
		public void CmdSetReadyStatus()
		{
			_isReady = !_isReady;
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