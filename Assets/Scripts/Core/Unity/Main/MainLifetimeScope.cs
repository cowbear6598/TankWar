using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Core.Unity.Main
{
	public class MainLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			RegisterMessagePipe(builder);
		}

		private void RegisterMessagePipe(IContainerBuilder builder)
		{
			builder.RegisterMessagePipe();

			builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
		}
	}
}