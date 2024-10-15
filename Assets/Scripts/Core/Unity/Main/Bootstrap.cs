using System;
using System.Threading.Tasks;
using Core.Network.Common;
using Core.Network.Infrastructure.Views;
using Cysharp.Threading.Tasks;
using MessagePipe;
using SoapTools.SceneController.Application.Repository;
using SoapTools.SceneController.Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Core.Unity.Main
{
	public class Bootstrap : MonoBehaviour
	{
		[Inject] private readonly ISubscriber<OnServerConnected> _onServerConnected;
		[Inject] private readonly SceneRepository                _sceneRepository;

		[SerializeField] private bool _isServer;

		[SerializeField] private AssetReference _menuScene;

		private IDisposable _subscription;

		private async void Start()
		{
			if (_isServer)
			{
				CustomNetworkManager.Instance.StartHost();
				return;
			}

			await LoadMenuScene();
		}

		private void OnEnable()  => _subscription = _onServerConnected.Subscribe(OnServerConnected);
		private void OnDisable() => _subscription.Dispose();

		private async void OnServerConnected(OnServerConnected e) => await LoadMenuScene();

		private async UniTask LoadMenuScene()
		{
			await new SceneControllerBuilder(_sceneRepository)
			      .LoadScene(_menuScene)
			      .Execute();

			Destroy(gameObject);
		}
	}
}