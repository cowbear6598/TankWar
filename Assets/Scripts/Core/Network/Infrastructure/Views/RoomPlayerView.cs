using System;
using Core.User.Domain.Adapters;
using Mirror;
using VContainer;

namespace Core.Network.Infrastructure.Views
{
	public class RoomPlayerView : NetworkBehaviour
	{
		[SyncVar(hook = nameof(OnPlayerNameChanged))]  private string _playerName;
		[SyncVar(hook = nameof(OnPlayerReadyChanged))] private bool   _isReady;

		[Inject] private IUser _user;

		private Action<string> _onPlayerNameChanged;
		private Action<bool>   _onPlayerReadyChanged;

		private void Start()
		{
			if (!isLocalPlayer)
				return;

			CustomNetworkManager.Instance.InjectGameObject(gameObject);

			CmdSetName(_user.Name);
		}

		private void OnPlayerNameChanged(string _, string playerName) => _onPlayerNameChanged?.Invoke(playerName);
		private void OnPlayerReadyChanged(bool  _, bool   isReady)    => _onPlayerReadyChanged?.Invoke(isReady);

		[Command]
		private void CmdSetName(string name)
		{
			_playerName = name;
		}

		[Command]
		public void CmdSetReady(bool isReady)
		{
			_isReady = isReady;
		}

		public string PlayerName => _playerName;
		public bool   IsReady    => _isReady;

		public void SubscribeOnPlayerNameChanged(Action<string> onPlayerNameChanged)  => _onPlayerNameChanged += onPlayerNameChanged;
		public void SubscribeOnPlayerReadyChanged(Action<bool>  onPlayerReadyChanged) => _onPlayerReadyChanged += onPlayerReadyChanged;
	}
}