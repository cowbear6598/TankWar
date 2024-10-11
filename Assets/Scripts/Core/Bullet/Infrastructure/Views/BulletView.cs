using Core.Bullet.Application.Adapaters;
using Core.Bullet.Domain.Adapters;
using UnityEngine;
using VContainer;

namespace Core.Bullet.Infrastructure.Views
{
	public class BulletView : MonoBehaviour, IBulletView
	{
		[Inject] private IBullet _bullet;

		public void UpdatePosition(Vector3 position) => transform.position = position;

		public void Reuse(Vector3 position, Quaternion rotation)
		{
			_bullet.Reuse(position, rotation);

			transform.SetPositionAndRotation(position, rotation);
		}
	}
}