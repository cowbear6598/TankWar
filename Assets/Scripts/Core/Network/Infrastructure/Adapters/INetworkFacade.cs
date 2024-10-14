namespace Core.Network.Infrastructure.Adapters
{
	public interface INetworkFacade
	{
		void StartClient(string ip, ushort port);
		void StartServer();
	}
}