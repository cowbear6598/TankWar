using System.Collections.Generic;
using Core.Network.Common;
using Core.Network.Infrastructure.Views;
using MessagePipe;
using Mirror;
using VContainer;

namespace Core.Network.Infrastructure.Repositories
{
	public class RoomPlayerRepository
	{
		[Inject] private readonly IPublisher<OnPlayerAdded>   _onPlayerAdded;
		[Inject] private readonly IPublisher<OnPlayerRemoved> _onPlayerRemoved;

		private readonly Dictionary<int, RoomPlayerView> _roomPlayers = new();

		public void Add(int connectionID, RoomPlayerView roomPlayerView)
		{
			_roomPlayers.Add(connectionID, roomPlayerView);

			_onPlayerAdded.Publish(new OnPlayerAdded(connectionID, roomPlayerView));
		}

		public void Remove(int connectionID)
		{
			_roomPlayers.Remove(connectionID);

			_onPlayerRemoved.Publish(new OnPlayerRemoved(connectionID));
		}

		public void Destroy(int connectionID)
		{
			var roomPlayerView = _roomPlayers[connectionID];

			NetworkServer.Destroy(roomPlayerView.gameObject);
		}

		public IList<RoomPlayerView> GetRoomPlayerViews() => new List<RoomPlayerView>(_roomPlayers.Values);
	}
}