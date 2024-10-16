using System.Collections.Generic;
using Core.Network.Common;
using Core.Network.Infrastructure.Views;
using Mirror;

namespace Core.Network.Infrastructure.Repositories
{
	public class RoomPlayerRepository : NetworkBehaviour
	{
		public static RoomPlayerRepository Instance { get; private set; }

		private readonly SyncDictionary<int, RoomPlayerView> _roomPlayers = new();

		private void Awake()
		{
			Instance = this;
		}

		public void Add(int connectionID, RoomPlayerView roomPlayer)
		{
			_roomPlayers.TryAdd(connectionID, roomPlayer);
		}

		public void Remove(int connectionID)
		{
			if (_roomPlayers.TryGetValue(connectionID, out var roomPlayer))
				NetworkServer.Destroy(roomPlayer.gameObject);

			_roomPlayers.Remove(connectionID);
		}

		public RoomPlayerView Get(int connectionID) => _roomPlayers[connectionID];
	}
}