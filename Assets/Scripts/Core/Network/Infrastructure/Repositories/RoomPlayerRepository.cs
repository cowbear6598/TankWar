using System.Collections.Generic;
using Core.Network.Common;
using Core.Network.Infrastructure.Views;
using MessagePipe;
using VContainer;

namespace Core.Network.Infrastructure.Repositories
{
	public class RoomPlayerRepository
	{
		[Inject] private readonly IPublisher<OnPlayerAdded>   _onPlayerAdded;
		[Inject] private readonly IPublisher<OnPlayerRemoved> _onPlayerRemoved;

		private readonly Dictionary<uint, RoomPlayerView> _roomPlayers = new();

		public void Add(uint netID, RoomPlayerView roomPlayerView)
		{
			_roomPlayers.Add(netID, roomPlayerView);

			_onPlayerAdded.Publish(new OnPlayerAdded(roomPlayerView));
		}

		public void Remove(uint netID)
		{
			_roomPlayers.Remove(netID);

			_onPlayerRemoved.Publish(new OnPlayerRemoved(netID));
		}
	}
}