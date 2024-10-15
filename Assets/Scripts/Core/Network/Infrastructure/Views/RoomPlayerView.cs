using System;
using Core.User.Domain.Adapters;
using Mirror;
using UnityEngine;
using VContainer;

namespace Core.Network.Infrastructure.Views
{
	public class RoomPlayerView : NetworkBehaviour
	{
		[SyncVar] private string _playerName;
		[SyncVar] private bool   _isReady;

		[Inject] private IUser _user;

		private IDisposable _subscription;

		private void Start()
		{
			if (!isLocalPlayer)
				return;

			CustomNetworkManager.Instance.InjectGameObject(gameObject);

			CmdSetName(_user.Name);
		}

		private void Update()
		{
			Debug.Log($"Player name: {_playerName}");
		}

		[Command]
		public void CmdSetName(string name)
		{
			_playerName = name;
		}

		[Command]
		public void CmdSetReady(bool isReady)
		{
			_isReady = isReady;
		}
	}
}