﻿using Core.Network.Infrastructure.Adapters;
using Core.Network.Infrastructure.Views;
using Mirror;
using VContainer;

namespace Core.Network.Infrastructure
{
	public class NetworkFacade : INetworkFacade
	{
		[Inject] private readonly CustomNetworkManager _networkManager;

		public void StartClient(string ip, ushort port)
		{
			_networkManager.networkAddress                               = ip;
			_networkManager.transport.GetComponent<PortTransport>().Port = port;
			_networkManager.StartClient();
		}

		public void StartServer() => _networkManager.StartHost();
	}
}