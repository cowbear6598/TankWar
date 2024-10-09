using Core.Tank.Application;
using Core.Tank.Infrastructure.ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Tank.Infrastructure.LifetimeScopes
{
	public class TankLifetimeScope : LifetimeScope
	{
		[SerializeField] private TankScriptableObject     _data;
		[SerializeField] private TankMoveHandler.Settings _moveSettings;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(_data);
			builder.RegisterInstance(_moveSettings);

			builder.Register<Domain.Tank>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<TankMoveHandler>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}
	}
}