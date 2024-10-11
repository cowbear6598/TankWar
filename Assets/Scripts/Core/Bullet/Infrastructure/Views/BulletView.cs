using Core.Bullet.Application.Adapters;
using Core.Bullet.Domain.Adapters;
using UnityEngine;
using VContainer;

namespace Core.Bullet.Infrastructure.Views
{
	public class BulletView : MonoBehaviour, IBulletView
	{
		[Inject] private readonly IBullet        _bullet;
		[Inject] private readonly IBulletFactory _bulletFactory;

		public void UpdatePosition(Vector3 position) => transform.position = position;

		public void Recycle() => _bulletFactory.Recycle(this);

		public void Reuse(Vector3 position, Quaternion rotation)
		{
			_bullet.Reuse(position, rotation);

			transform.SetPositionAndRotation(position, rotation);
		}
	}
}