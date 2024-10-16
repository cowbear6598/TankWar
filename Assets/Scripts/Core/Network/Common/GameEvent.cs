using Core.Network.Infrastructure.Views;

namespace Core.Network.Common
{
	public struct OnServerConnected { }
	public struct OnClientConnected { }

	public struct OnRoomPlayerAdded
	{
		public readonly RoomPlayerView RoomPlayerView;

		public OnRoomPlayerAdded(RoomPlayerView roomPlayerView) => RoomPlayerView = roomPlayerView;
	}

	public struct OnRoomPlayerRemoved
	{
		public readonly RoomPlayerView RoomPlayerView;

		public OnRoomPlayerRemoved(RoomPlayerView roomPlayerView) => RoomPlayerView = roomPlayerView;
	}
}