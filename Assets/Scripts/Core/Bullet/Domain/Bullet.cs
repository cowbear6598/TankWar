using Core.Bullet.Domain.Adapters;
using Core.Bullet.Infrastructure.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Core.Bullet.Domain
{
	public class Bullet : IBullet
	{
		[Inject] private readonly BulletScriptableObject _bulletSettings;

		public Vector3    Position { get; private set; }
		public Quaternion Rotation { get; private set; }

		public bool IsActive { get; private set; }

		public float Lifetime { get; private set; }

		public void Reuse(Vector3 position, Quaternion rotation)
		{
			Position = position;
			Rotation = rotation;

			IsActive = true;

			Lifetime = _bulletSettings.Lifetime;
		}

		public void Move(float deltaTime)
		{
			var forward = Rotation * Vector3.forward;

			Position += _bulletSettings.MoveSpeed * forward * deltaTime;
		}

		public void LifetimeDecrease(float deltaTime)
		{
			Lifetime -= deltaTime;

			if (Lifetime > 0)
				return;

			Lifetime = 0;
			IsActive = false;
		}
	}
}