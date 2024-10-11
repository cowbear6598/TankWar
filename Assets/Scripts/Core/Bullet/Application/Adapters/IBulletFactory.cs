using Core.Bullet.Infrastructure.Views;
using UnityEngine;

namespace Core.Bullet.Application.Adapters
{
	public interface IBulletFactory
	{
		void Reuse(Vector3 position, Quaternion rotation);

		void Recycle(BulletView bulletView);
	}
}