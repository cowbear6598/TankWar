using Core.Tank.Domain.Adapters;
using Core.Tank.Infrastructure.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Core.Tank.Domain
{
	public class Tank : ITank
	{
		[Inject] private readonly TankScriptableObject _tankSettings;

		public Vector3    Position       { get; private set; } = Vector3.zero;
		public Quaternion BodyRotation   { get; private set; } = Quaternion.identity;
		public Quaternion TurretRotation { get; private set; } = Quaternion.identity;

		public void Move(float axis, float deltaTime)
		{
			var moveDelta = BodyRotation * Vector3.forward * axis * _tankSettings.MoveSpeed * deltaTime;

			Position += moveDelta;
		}

		public void RotateBody(float axis, float deltaTime)
		{
			var angle = axis * _tankSettings.BodyRotateSpeed * deltaTime;

			BodyRotation *= Quaternion.Euler(0, angle, 0);
		}

		public void RotateTurret(Vector3 targetPosition, float deltaTime)
		{
			var targetDirection = targetPosition - Position;
			targetDirection.y = 0;

			if (targetDirection == Vector3.zero)
				return;

			var targetRotation = Quaternion.LookRotation(targetDirection);
			TurretRotation = Quaternion.RotateTowards(TurretRotation, targetRotation, _tankSettings.TurretRotateSpeed * deltaTime);
		}
	}
}