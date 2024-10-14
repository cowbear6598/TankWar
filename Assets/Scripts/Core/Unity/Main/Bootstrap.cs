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

		[SerializeField] private bool _isServer;

		[SerializeField] private AssetReference _menuScene;
		[SerializeField] private AssetReference _gameScene;

		private async void Awake()
		{
			if (_isServer)
			{
				await new SceneControllerBuilder(_sceneRepository)
				      .LoadScene(_gameScene)
				      .Execute();
			}
			else
			{
				await new SceneControllerBuilder(_sceneRepository)
				      .LoadScene(_menuScene)
				      .Execute();
			}

			Destroy(gameObject);
		}
	}
}