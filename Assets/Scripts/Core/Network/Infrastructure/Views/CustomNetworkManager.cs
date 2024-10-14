using Core.Network.Common;
using Core.Network.Domain;
using Core.Network.Infrastructure.Repositories;
using MessagePipe;
using Mirror;
using UnityEngine;
using VContainer;

namespace Core.Network.Infrastructure.Views
{
	public class CustomNetworkManager : NetworkManager
	{
		[Inject] private readonly IPublisher<OnServerConnected> _onServerConnected;
		[Inject] private readonly RoomPlayerRepository          _roomPlayerRepository;

		[SerializeField] private RoomPlayer _roomPlayerPrefab;

		public override void OnStartServer()
		{
			Debug.Log("Server started");

			_onServerConnected.Publish(new OnServerConnected());
		}

		public override void OnStartClient()
		{
			Debug.Log("Client started");
		}

		public override void OnClientConnect()
		{
			Debug.Log("Client connected");

			NetworkClient.Ready();
			NetworkClient.AddPlayer();
		}

		public override void OnServerAddPlayer(NetworkConnectionToClient conn)
		{
			Debug.Log("Add Room Player");

			var roomPlayer = Instantiate(_roomPlayerPrefab);

			_roomPlayerRepository.Add(roomPlayer);

			NetworkServer.AddPlayerForConnection(conn, roomPlayer.gameObject);
		}

		public override void OnServerDisconnect(NetworkConnectionToClient conn) { }
	}
}