using Core.Network.Common;
using Core.Network.Infrastructure.Repositories;
using MessagePipe;
using Mirror;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Network.Infrastructure.Views
{
	public class CustomNetworkManager : NetworkManager
	{
		[Inject] private readonly IPublisher<OnServerConnected>   _onServerConnected;
		[Inject] private readonly IPublisher<OnClientConnected>   _onClientConnected;
		[Inject] private readonly IPublisher<OnRoomPlayerAdded>   _onRoomPlayerAdded;
		[Inject] private readonly IPublisher<OnRoomPlayerRemoved> _onRoomPlayerRemoved;

		[Inject] private readonly IObjectResolver _resolver;

		[SerializeField] private RoomPlayerView       _roomPlayerPrefab;
		[SerializeField] private RoomPlayerRepository _roomPlayerRepositoryPrefab;

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

			var roomPlayerRepository = Instantiate(_roomPlayerRepositoryPrefab);

			NetworkServer.Spawn(roomPlayerRepository.gameObject);

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

			var roomPlayer = Instantiate(_roomPlayerPrefab);

			NetworkServer.AddPlayerForConnection(conn, roomPlayer.gameObject);

			RoomPlayerRepository.Instance.Add(conn.connectionId, roomPlayer);

			_onRoomPlayerAdded.Publish(new OnRoomPlayerAdded(roomPlayer));
		}

		public override void OnServerDisconnect(NetworkConnectionToClient conn)
		{
			Debug.Log("Player removed");

			var roomPlayer = RoomPlayerRepository.Instance.Get(conn.connectionId);

			RoomPlayerRepository.Instance.Remove(conn.connectionId);

			_onRoomPlayerRemoved.Publish(new OnRoomPlayerRemoved(roomPlayer));
		}

		public void InjectGameObject(GameObject gameObject) => _resolver.InjectGameObject(gameObject);
	}
}