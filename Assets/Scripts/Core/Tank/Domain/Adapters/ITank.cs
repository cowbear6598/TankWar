using UnityEngine;

namespace Core.Tank.Domain.Adapters
{
	public interface ITank
	{
		void Move(Vector3     moveDelta);
		void BodyRotate(float rotationAngle);

		Vector3    Position     { get; }
		Quaternion BodyRotation { get; }
	}
}