using UnityEngine;

namespace Core.Bullet.Application.Adapters
{
	public interface IBulletView
	{
		void UpdatePosition(Vector3 position);
		void Recycle();
	}
}