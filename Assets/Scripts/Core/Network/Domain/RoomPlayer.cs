using Mirror;

namespace Core.Network.Domain
{
	public class RoomPlayer : NetworkBehaviour
	{
		[SyncVar] private bool _isReady;
		[SyncVar] private int  _playerId;
	}
}