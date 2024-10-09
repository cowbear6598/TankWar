using UnityEngine;

namespace Core.Tank.Domain.Adapters
{
	public interface ITank
	{
		void Move(float       axis, float deltaTime);
		void BodyRotate(float axis, float deltaTime);

		Vector3    Position     { get; }
		Quaternion BodyRotation { get; }
	}
}