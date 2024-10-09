using Core.Bullet.Domain.Adapters;
using Core.Bullet.Infrastructure.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Core.Bullet.Domain
{
	public class Bullet : IBullet
	{
		[Inject] private readonly BulletScriptableObject _bulletSettings;

		public Vector3 Position { get; private set; }

		public void Move(float deltaTime)
		{
			Position += _bulletSettings.MoveSpeed * Vector3.forward * deltaTime;
		}
	}
}