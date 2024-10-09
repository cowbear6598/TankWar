using Core.Tank.Domain.Adapters;
using UnityEngine;

namespace Core.Tank.Domain
{
	public class Tank : ITank
	{
		public Vector3    Position     { get; private set; } = Vector3.zero;
		public Quaternion BodyRotation { get; private set; } = Quaternion.identity;

		public void Move(Vector3 moveDelta) => Position += moveDelta;

		public void BodyRotate(float rotationAngle) => BodyRotation *= Quaternion.Euler(0, rotationAngle, 0);
	}
}