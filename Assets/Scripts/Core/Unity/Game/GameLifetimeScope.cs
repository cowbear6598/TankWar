using Core.Controller.Domain;
using VContainer;
using VContainer.Unity;

namespace Core.Unity.Game
{
	public class GameLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			RegisterController(builder);
		}

		private void RegisterController(IContainerBuilder builder)
		{
			builder.Register<PCInput>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<Controller.Infrastructure.Controller>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}
	}
}