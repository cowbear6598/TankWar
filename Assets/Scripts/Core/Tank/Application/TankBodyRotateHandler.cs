using Core.Controller;
using Core.Tank.Application.Adapters;
using Core.Tank.Domain.Adapters;
using Core.Tank.Infrastructure.ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Tank.Application
{
	public class TankBodyRotateHandler : ITickable
	{
		[Inject] private readonly TankScriptableObject _tankData;
		[Inject] private readonly IController          _controller;
		[Inject] private readonly ITankView            _tankView;
		[Inject] private readonly ITank                _tank;

		public void Tick()
		{
			var axis = _controller.MoveAxis.x;

			var rotationAngle = axis * _tankData.BodyRotateSpeed * Time.deltaTime;

			Debug.Log($"Rotation angle: {rotationAngle}");

			_tank.BodyRotate(rotationAngle);

			Debug.Log($"Body rotation: {_tank.BodyRotation}");

			_tankView.UpdateBodyRotation(_tank.BodyRotation);
		}
	}
}