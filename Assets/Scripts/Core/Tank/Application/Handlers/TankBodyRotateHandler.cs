using Core.Controller;
using Core.Tank.Application.Adapters;
using Core.Tank.Domain.Adapters;
using Core.Tank.Infrastructure.ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Tank.Application.Handlers
{
	public class TankBodyRotateHandler : ITickable
	{
		[Inject] private readonly IController _controller;
		[Inject] private readonly ITankView   _tankView;
		[Inject] private readonly ITank       _tank;

		public void Tick()
		{
			var axis = _controller.MoveAxis.x;

			_tank.BodyRotate(axis, Time.deltaTime);

			_tankView.UpdateBodyRotation(_tank.BodyRotation);
		}
	}
}