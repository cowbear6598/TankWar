using Core.Tank.Application.Adapters;
using UnityEngine;

namespace Core.Tank.Infrastructure.View
{
	public class TankView : MonoBehaviour, ITankView
	{
		[SerializeField] private Transform _bodyTransform;
		[SerializeField] private Transform _turretTransform;
		[SerializeField] private Transform _bulletSpawnTransform;

		public void UpdatePosition(Vector3          position) => transform.position = position;
		public void UpdateBodyRotation(Quaternion   rotation) => _bodyTransform.rotation = rotation;
		public void UpdateTurretRotation(Quaternion rotation) => _turretTransform.rotation = rotation;

		public (Vector3 spawnPosition, Quaternion spawnRotation) GetSpawnPosition() => (_bulletSpawnTransform.position, _bulletSpawnTransform.rotation);
	}
}