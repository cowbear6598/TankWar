using UnityEngine;

namespace Core.Bullet.Domain.Adapters
{
	public interface IBullet
	{
		void Move(float deltaTime);

		Vector3 Position { get; }
	}
}