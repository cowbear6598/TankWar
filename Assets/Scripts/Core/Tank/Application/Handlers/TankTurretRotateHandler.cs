using Core.Controller;
using Core.Tank.Application.Adapters;
using Core.Tank.Domain.Adapters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Tank.Application.Handlers
{
	public class TankTurretRotateHandler : IInitializable, ITickable
	{
		[Inject] private readonly IController _controller;
		[Inject] private readonly ITank       _tank;
		[Inject] private readonly ITankView   _tankView;

		private LayerMask _groundLayer;

		public void Initialize()
		{
			_groundLayer = LayerMask.GetMask("Ground");
		}

		public void Tick()
		{
			var cam = Camera.main;

			var mousePosition = _controller.MousePosition;

			var ray = cam.ScreenPointToRay(mousePosition);

			if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, _groundLayer))
				return;

			var targetPosition = hit.point;

			_tank.RotateTurret(targetPosition, Time.deltaTime);

			_tankView.UpdateTurretRotation(_tank.TurretRotation);
		}
	}
}