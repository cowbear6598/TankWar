using Core.Tank.Application;
using Core.Tank.Application.Adapters;
using Core.Tank.Application.Handlers;
using Core.Tank.Infrastructure.ScriptableObjects;
using Core.Tank.Infrastructure.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Tank.Infrastructure.LifetimeScopes
{
	public class TankLifetimeScope : LifetimeScope
	{
		[SerializeField] private TankScriptableObject _data;
		[SerializeField] private TankView             _tankView;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(_data);

			builder.Register<Domain.Tank>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<TankMoveHandler>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<TankBodyRotateHandler>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<TankTurretRotateHandler>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.RegisterComponent(_tankView)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}
	}
}