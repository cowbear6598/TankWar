using Core.Network.Infrastructure.Repositories;
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
			builder.Register<RoomPlayerRepository>(Lifetime.Singleton);

			RegisterUser(builder);
			RegisterMessagePipe(builder);
			RegisterScene(builder);
		}

		private void RegisterUser(IContainerBuilder builder)
		{
			builder.Register<User.Domain.User>(Lifetime.Singleton)
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