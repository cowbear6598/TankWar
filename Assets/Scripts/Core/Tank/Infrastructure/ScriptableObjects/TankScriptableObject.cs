using UnityEngine;

namespace Core.Tank.Infrastructure.ScriptableObjects
{
	[CreateAssetMenu(fileName = "TankData", menuName = "Data/TankData")]
	public class TankScriptableObject : ScriptableObject
	{
		public float MoveSpeed;
		public float BodyRotateSpeed;
		public float TurretRotateSpeed;
	}
}