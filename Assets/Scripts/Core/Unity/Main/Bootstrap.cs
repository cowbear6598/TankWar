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
		[Inject] private readonly SceneRepository _sceneRepository;

		[SerializeField] private AssetReference _menuScene;

		private async void Start()
		{
			await new SceneControllerBuilder(_sceneRepository)
			      .LoadScene(_menuScene)
			      .Execute();

			Destroy(gameObject);
		}
	}
}