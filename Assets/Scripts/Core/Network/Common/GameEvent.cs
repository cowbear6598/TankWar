using Core.Network.Infrastructure.Views;

namespace Core.Network.Common
{
	public struct OnServerConnected { }
	public struct OnClientConnected { }

	public struct OnPlayerAdded
	{
		public readonly RoomPlayerView RoomPlayerView;

		public OnPlayerAdded(RoomPlayerView roomPlayerView) => RoomPlayerView = roomPlayerView;
	}

	public struct OnPlayerRemoved
	{
		public readonly uint NetID;

		public OnPlayerRemoved(uint netID) => NetID = netID;
	}
}