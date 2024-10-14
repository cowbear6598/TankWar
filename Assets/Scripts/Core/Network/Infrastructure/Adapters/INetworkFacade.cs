namespace Core.Network.Infrastructure.Adapters
{
	public interface INetworkFacade
	{
		void Connect(string ip, ushort port);
		void StartServer();
	}
}