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
		[Inject] private readonly IPublisher<OnConnected> _onConnected;
		[Inject] private readonly RoomPlayerRepository    _roomPlayerRepository;

		[SerializeField] private RoomPlayer _roomPlayerPrefab;

		public override void OnStartServer()
		{
			Debug.Log("Server started");
		}

		public override void OnStartClient()
		{
			Debug.Log("Client started");
		}

		public override void OnClientConnect()
		{
			Debug.Log("Client connected");
		}

		public override void OnServerAddPlayer(NetworkConnectionToClient conn)
		{
			var playerObj = Instantiate(_roomPlayerPrefab);

			playerObj.name = $"Player_{conn.connectionId}";

			NetworkServer.AddPlayerForConnection(conn, playerObj.gameObject);
		}

		public override void OnServerDisconnect(NetworkConnectionToClient conn) { }
	}
}