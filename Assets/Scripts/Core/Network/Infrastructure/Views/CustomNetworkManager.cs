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
		[Inject] private readonly IPublisher<OnServerConnected> _onServerConnected;
		[Inject] private readonly IPublisher<OnRoomPlayerAdded> _onRoomPlayerAdded;

		[Inject] private readonly IObjectResolver _resolver;

		[SerializeField] private RoomPlayerView       _roomPlayerPrefab;
		[SerializeField] private RoomPlayerRepository _roomPlayerRepositoryPrefab;

		public static CustomNetworkManager Instance { get; private set; }

		public override void Awake()
		{
			base.Awake();

			Instance = this;
		}

		public void StartClient(string ip, ushort port)
		{
			networkAddress                     = ip;
			GetComponent<PortTransport>().Port = port;

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

			NetworkClient.Ready();
			NetworkClient.AddPlayer();
		}

		public override void OnServerAddPlayer(NetworkConnectionToClient conn)
		{
			var roomPlayer = Instantiate(_roomPlayerPrefab);

			NetworkServer.AddPlayerForConnection(conn, roomPlayer.gameObject);

			RoomPlayerRepository.Instance.Add(conn.connectionId, roomPlayer);

			_onRoomPlayerAdded.Publish(new OnRoomPlayerAdded());
		}

		public override void OnServerDisconnect(NetworkConnectionToClient conn)
		{
			RoomPlayerRepository.Instance.Remove(conn.connectionId);
		}

		public void InjectGameObject(GameObject gameObject) => _resolver.InjectGameObject(gameObject);
	}
}