using VContainer;
using VContainer.Unity;

namespace Core.Unity.Menu
{
	public class MenuLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<Core.Menu.Domain.Menu>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}
	}
}