using UnityEngine;

namespace Core.Tank.Domain.Adapters
{
	public interface ITank
	{
		void Move(float           axis,           float deltaTime);
		void RotateBody(float     axis,           float deltaTime);
		void RotateTurret(Vector3 targetPosition, float deltaTime);

		Vector3    Position       { get; }
		Quaternion BodyRotation   { get; }
		Quaternion TurretRotation { get; }
	}
}