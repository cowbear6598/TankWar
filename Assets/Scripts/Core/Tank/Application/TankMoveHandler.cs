using System;
using Core.Controller;
using Core.Tank.Application.Adapters;
using Core.Tank.Domain.Adapters;
using Core.Tank.Infrastructure.ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Tank.Application
{
	public class TankMoveHandler : ITickable
	{
		[Inject] private readonly TankScriptableObject _tankData;
		[Inject] private readonly IController          _controller;
		[Inject] private readonly ITankView            _tankView;
		[Inject] private readonly ITank                _tank;

		public void Tick()
		{
			var axis = _controller.MoveAxis.y;

			var moveDelta = _tank.BodyRotation * Vector3.forward * axis * _tankData.MoveSpeed * Time.deltaTime;

			_tank.Move(moveDelta);

			_tankView.UpdatePosition(_tank.Position);
		}
	}
}