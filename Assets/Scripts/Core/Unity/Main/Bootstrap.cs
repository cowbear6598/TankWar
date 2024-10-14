using Core.Network.Infrastructure.Adapters;
using SoapTools.SceneController.Application.Repository;
using SoapTools.SceneController.Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Core.Unity.Main
{
	public class Bootstrap : MonoBehaviour
	{
		[Inject] private readonly SceneRepository _sceneRepository;
		[Inject] private readonly INetworkFacade  _networkFacade;

		[SerializeField] private bool _isServer;

		[SerializeField] private AssetReference _menuScene;

		private async void Awake()
		{
			if (_isServer)
			{
				_networkFacade.StartServer();
			}

			await new SceneControllerBuilder(_sceneRepository)
			      .LoadScene(_menuScene)
			      .Execute();

			Destroy(gameObject);
		}
	}
}