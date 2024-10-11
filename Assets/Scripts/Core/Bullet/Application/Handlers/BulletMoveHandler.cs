using Core.Bullet.Application.Adapters;
using Core.Bullet.Domain.Adapters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Bullet.Application.Handlers
{
	public class BulletMoveHandler : ITickable
	{
		[Inject] private readonly IBullet     _bullet;
		[Inject] private readonly IBulletView _bulletView;

		public void Tick()
		{
			if (!_bullet.IsActive)
				return;

			_bullet.Move(Time.deltaTime);
			_bulletView.UpdatePosition(_bullet.Position);
		}
	}
}