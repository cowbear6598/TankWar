using Core.Bullet.Infrastructure.Factories;
using Core.Controller.Domain;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Unity.Game
{
	public class GameLifetimeScope : LifetimeScope
	{
		[SerializeField] private BulletFactory.Settings _bulletFactorySettings;

		protected override void Configure(IContainerBuilder builder)
		{
			RegisterBullet(builder);
			RegisterController(builder);
		}

		private void RegisterBullet(IContainerBuilder builder)
		{
			builder.RegisterInstance(_bulletFactorySettings);

			builder.Register<BulletFactory>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
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