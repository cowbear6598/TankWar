using Core.Network.Domain.Adapters;
using Mirror;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Network.Infrastructure.Views
{
	public class RoomPlayerView : NetworkBehaviour
	{
		private readonly IObjectResolver _resolver;

		private IRoomPlayer _roomPlayer;

		private void Awake()
		{
			if (!isLocalPlayer)
				return;

			Debug.Log("I control this player");

			FindFirstObjectByType<LifetimeScope>().Container.InjectGameObject(gameObject);
		}
	}
}