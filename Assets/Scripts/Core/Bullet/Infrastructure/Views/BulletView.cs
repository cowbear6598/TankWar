using Core.Bullet.Application.Adapaters;
using UnityEngine;

namespace Core.Bullet.Infrastructure.Views
{
	public class BulletView : MonoBehaviour, IBulletView
	{
		public void UpdatePosition(Vector3 position) => transform.position = position;
	}
}