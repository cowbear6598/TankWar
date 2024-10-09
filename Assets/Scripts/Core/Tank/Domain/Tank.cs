using Core.Tank.Domain.Adapters;
using Core.Tank.Infrastructure.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Core.Tank.Domain
{
	public class Tank : ITank
	{
		[Inject] private readonly TankScriptableObject _tankData;

		public Vector3    Position     { get; private set; } = Vector3.zero;
		public Quaternion BodyRotation { get; private set; } = Quaternion.identity;

		public void Move(float axis, float deltaTime)
		{
			var moveDelta = BodyRotation * Vector3.forward * axis * _tankData.MoveSpeed * deltaTime;

			Position += moveDelta;
		}

		public void BodyRotate(float axis, float deltaTime)
		{
			var angle = axis * _tankData.BodyRotateSpeed * deltaTime;

			BodyRotation *= Quaternion.Euler(0, angle, 0);
		}
	}
}