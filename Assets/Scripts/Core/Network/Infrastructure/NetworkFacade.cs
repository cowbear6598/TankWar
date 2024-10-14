using Core.Network.Infrastructure.Adapters;
using Core.Network.Infrastructure.Views;
using Mirror;
using VContainer;

namespace Core.Network.Infrastructure
{
	public class NetworkFacade : INetworkFacade
	{
		[Inject] private readonly CustomNetworkManager _networkManager;

		public void Connect(string ip, ushort port)
		{
			_networkManager.networkAddress                               = ip;
			_networkManager.transport.GetComponent<PortTransport>().Port = (ushort)port;
			_networkManager.StartClient();
		}
	}
}