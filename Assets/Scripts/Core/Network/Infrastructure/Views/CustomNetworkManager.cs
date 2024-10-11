using System;
using Mirror;
using UnityEngine;

namespace Core.Network.Infrastructure.Views
{
	public class CustomNetworkManager : NetworkManager
	{
		public override void Start()
		{
			base.Start();

			StartServer();
		}

		public override void OnStartServer()
		{
			Debug.Log("Server started");
		}

		public override void OnStartClient()
		{
			Debug.Log("Client started");
		}

		public override void OnClientConnect()
		{
			Debug.Log("Client connected");
		}
	}
}