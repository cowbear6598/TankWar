using Core.Bullet.Application.Adapters;
using Core.Controller;
using Core.Tank.Application.Adapters;
using VContainer;
using VContainer.Unity;

namespace Core.Tank.Application.Handlers
{
	public class TankShootHandler : ITickable
	{
		[Inject] private readonly IController    _controller;
		[Inject] private readonly IBulletFactory _bulletFactory;
		[Inject] private readonly ITankView      _tankView;

		public void Tick()
		{
			if (_controller.IsShoot)
			{
				var (spawnPosition, spawnRotation) = _tankView.GetSpawnPosition();

				_bulletFactory.Reuse(spawnPosition, spawnRotation);
			}
		}
	}
}