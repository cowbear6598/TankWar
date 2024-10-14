using Core.Network.Infrastructure;
using Core.Network.Infrastructure.Views;
using MessagePipe;
using SoapTools.SceneController.Application.Repository;
using VContainer;
using VContainer.Unity;

namespace Core.Unity.Main
{
	public class MainLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			RegisterNetwork(builder);
			RegisterMessagePipe(builder);
			RegisterScene(builder);
		}

		private void RegisterNetwork(IContainerBuilder builder)
		{
			builder.RegisterComponentInHierarchy<CustomNetworkManager>();
			builder.Register<NetworkFacade>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}

		private void RegisterScene(IContainerBuilder builder)
		{
			builder.Register<SceneRepository>(Lifetime.Singleton);
		}

		private void RegisterMessagePipe(IContainerBuilder builder)
		{
			builder.RegisterMessagePipe();

			builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
		}
	}
}