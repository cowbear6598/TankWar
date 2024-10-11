using UnityEngine;

namespace Core.Bullet.Infrastructure.Adapters
{
	public interface IBulletFactory
	{
		void Spawn(Vector3 position, Quaternion rotation);
	}
}