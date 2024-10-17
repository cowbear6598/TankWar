using Core.Network.Common;
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
		[Inject] private readonly IPublisher<OnClientConnected> _onClientConnected;

		[Inject] private readonly RoomPlayerRepository _roomPlayerRepository;

		[SerializeField] private RoomPlayerView _roomPlayerViewPrefab;

		public static CustomNetworkManager Instance { get; private set; }

		public override void Awake()
		{
			base.Awake();

			Instance = this;
		}

		public void Connect(string ip, ushort port, bool isServer)
		{
			networkAddress                     = ip;
			GetComponent<PortTransport>().Port = port;

			if (isServer)
				StartHost();
			else
				StartClient();
		}

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

			_onClientConnected.Publish(new OnClientConnected());

			NetworkClient.Ready();
			NetworkClient.AddPlayer();
		}

		public override void OnServerAddPlayer(NetworkConnectionToClient conn)
		{
			Debug.Log("Player added");

			var roomPlayerView = Instantiate(_roomPlayerViewPrefab);
			roomPlayerView.SetConnectionID(conn.connectionId);

			NetworkServer.AddPlayerForConnection(conn, roomPlayerView.gameObject);
		}

		public override void OnServerDisconnect(NetworkConnectionToClient conn)
		{
			Debug.Log("Player disconnected");

			_roomPlayerRepository.Destroy(conn.connectionId);
		}
	}
}