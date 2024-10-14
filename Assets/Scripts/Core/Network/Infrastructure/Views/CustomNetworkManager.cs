using Core.Network.Domain;
using MessagePipe;
using Mirror;
using UnityEngine;
using VContainer;

namespace Core.Network.Infrastructure.Views
{
	public class CustomNetworkManager : NetworkManager
	{
		[Inject] private readonly IPublisher<OnConnected> _onConnected;

		public override void OnStartServer()
		{
			Debug.Log("Server started");
		}

		public override void OnStartClient()
		{
			Debug.Log("Client started");

			_onConnected.Publish(new OnConnected());
		}

		public override void OnClientConnect()
		{
			Debug.Log("Client connected");
		}
	}
}