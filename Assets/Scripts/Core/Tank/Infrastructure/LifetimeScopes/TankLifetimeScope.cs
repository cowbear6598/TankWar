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
		[SerializeField] private TankScriptableObject _settings;
		[SerializeField] private TankView             _view;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(_settings);

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

			builder.Register<TankShootHandler>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.RegisterComponent(_view)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}
	}
}