using Core.Tank.Application.Adapters;
using UnityEngine;

namespace Core.Tank.Infrastructure.View
{
	public class TankView : MonoBehaviour, ITankView
	{
		public void UpdatePosition(Vector3 position) => transform.position = position;
	}
}