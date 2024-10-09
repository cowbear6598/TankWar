using UnityEngine;

namespace Core.Tank.Domain.Adapters
{
	public interface ITank
	{
		void Move(Vector3 moveDelta);

		Vector3 Position { get; }
	}
}