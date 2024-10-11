using Core.Bullet.Application.Handlers;
using Core.Bullet.Infrastructure.ScriptableObjects;
using Core.Bullet.Infrastructure.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Bullet.Infrastructure.LifetimeScopes
{
	public class BulletLifetimeScope : LifetimeScope
	{
		[SerializeField] private BulletView             _view;
		[SerializeField] private BulletScriptableObject _settings;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(_settings);

			builder.Register<Domain.Bullet>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<BulletMoveHandler>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<BulletLifetimeHandler>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.RegisterComponent(_view)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}
	}
}