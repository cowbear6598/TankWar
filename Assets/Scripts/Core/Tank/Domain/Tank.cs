using Core.Tank.Domain.Adapters;
using UnityEngine;

namespace Core.Tank.Domain
{
	public class Tank : ITank
	{
		public Vector3 Position { get; private set; }

		public void Move(Vector3 moveDelta)
		{
			Position += moveDelta;
		}
	}
}