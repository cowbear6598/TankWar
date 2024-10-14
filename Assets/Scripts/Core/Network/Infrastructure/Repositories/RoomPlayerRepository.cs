using System.Collections.Generic;
using Core.Network.Domain;

namespace Core.Network.Infrastructure.Repositories
{
	public class RoomPlayerRepository
	{
		private readonly List<RoomPlayer> _players = new();

		public void Add(RoomPlayer roomPlayer) => _players.Add(roomPlayer);
	}
}