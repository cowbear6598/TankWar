using Core.Network.Infrastructure.Views;

namespace Core.Network.Common
{
	public struct OnServerConnected { }
	public struct OnClientConnected { }

	public struct OnPlayerAdded
	{
		public readonly int            ConnectionID;
		public readonly RoomPlayerView RoomPlayerView;

		public OnPlayerAdded(int connectionID, RoomPlayerView roomPlayerView)
		{
			ConnectionID   = connectionID;
			RoomPlayerView = roomPlayerView;
		}
	}

	public struct OnPlayerRemoved
	{
		public readonly int ConnectionID;

		public OnPlayerRemoved(int connectionID) => ConnectionID = connectionID;
	}
}