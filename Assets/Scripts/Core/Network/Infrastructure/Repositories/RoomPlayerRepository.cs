using System.Collections.Generic;
using Core.Network.Domain;

namespace Core.Network.Infrastructure.Repositories
{
	public class RoomPlayerRepository
	{
		private readonly HashSet<RoomPlayer> _players = new();
	}
}