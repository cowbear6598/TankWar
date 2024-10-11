using System;
using Core.Bullet.Infrastructure.Adapters;
using Core.Bullet.Infrastructure.Views;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.Bullet.Infrastructure.Factories
{
	public class BulletFactory : IInitializable, IBulletFactory
	{
		[Inject] private readonly Settings         _settings;
		[Inject] private readonly Func<BulletView> _bulletFactory;

		private IObjectPool<BulletView> _bulletPool;

		private Transform _parent;

		public void Initialize()
		{
			_parent = new GameObject("Bullets").transform;

			_bulletPool = new ObjectPool<BulletView>(
				OnCreateBullet,
				OnReuseBullet,
				OnReleaseBullet,
				OnDisposeBullet,
				defaultCapacity: 100
			);

			Preload();
		}

		private void Preload()
		{
			var tempBullets = new BulletView[100];

			for (var i = 0; i < 100; i++)
			{
				tempBullets[i] = _bulletPool.Get();
			}

			for (var i = 0; i < 100; i++)
			{
				_bulletPool.Release(tempBullets[i]);
			}
		}

		public void Spawn(Vector3 position, Quaternion rotation)
		{
			var bullet = _bulletPool.Get();

			bullet.Reuse(position, rotation);
		}

		#region Object Pool Aciton

		private BulletView OnCreateBullet()
		{
			var bullet = _bulletFactory.Invoke();

			bullet.transform.SetParent(_parent);
			bullet.gameObject.SetActive(false);
			return bullet;
		}

		private void OnReuseBullet(BulletView bullet)
		{
			bullet.gameObject.SetActive(true);
		}

		private void OnReleaseBullet(BulletView bullet)
		{
			bullet.gameObject.SetActive(false);
		}

		private void OnDisposeBullet(BulletView bullet)
		{
			Object.Destroy(bullet.gameObject);
		}

		#endregion

		[Serializable]
		public class Settings
		{
			public BulletView BulletPrefab;
		}
	}
}