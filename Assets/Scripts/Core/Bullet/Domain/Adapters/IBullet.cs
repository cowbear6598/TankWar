using UnityEngine;

namespace Core.Bullet.Domain.Adapters
{
	public interface IBullet
	{
		void Reuse(Vector3          position, Quaternion rotation);
		void Move(float             deltaTime);
		void LifetimeDecrease(float deltaTime);

		Vector3 Position { get; }
		bool    IsActive { get; }
		float   Lifetime { get; }
	}
}