using UnityEngine;

namespace Core.Bullet.Infrastructure.ScriptableObjects
{
	[CreateAssetMenu(fileName = "BulletData", menuName = "Data/BulletData")]
	public class BulletScriptableObject : ScriptableObject
	{
		public float MoveSpeed;
	}
}