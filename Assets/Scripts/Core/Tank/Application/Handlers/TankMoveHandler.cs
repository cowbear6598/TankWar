using System;
using Core.Controller;
using Core.Tank.Application.Adapters;
using Core.Tank.Domain.Adapters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Tank.Application.Handlers
{
	public class TankMoveHandler : ITickable, IDisposable
	{
		[Inject] private readonly IController _controller;
		[Inject] private readonly ITankView   _tankView;
		[Inject] private readonly ITank       _tank;

		public void Tick()
		{
			var axis = _controller.MoveAxis.y;

			_tank.Move(axis, Time.deltaTime);

			_tankView.UpdatePosition(_tank.Position);
		}
		public void Dispose()
		{
			Debug.Log("TankMoveHandler disposed");
		}
	}
}