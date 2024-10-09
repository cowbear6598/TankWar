using System;
using Core.Controller;
using Core.Tank.Domain.Adapters;
using Core.Tank.Infrastructure.ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Tank.Application
{
	public class TankMoveHandler : ITickable
	{
		[Inject] private readonly Settings             _settings;
		[Inject] private readonly TankScriptableObject _tankData;
		[Inject] private readonly IController          _controller;
		[Inject] private readonly ITank                _tank;

		public void Tick()
		{
			var moveAxis      = _controller.MoveAxis;
			var moveDirection = new Vector3(moveAxis.x, 0, moveAxis.y);

			_tank.Move(moveDirection * _tankData.MoveSpeed * Time.deltaTime);

			var transform = _settings.Transform;
			transform.position = _tank.Position;
		}

		[Serializable]
		public class Settings
		{
			public Transform Transform;
		}
	}
}