﻿using UnityEngine;

namespace Core.Tank.Application.Adapters
{
	public interface ITankView
	{
		void UpdatePosition(Vector3          position);
		void UpdateBodyRotation(Quaternion   rotation);
		void UpdateTurretRotation(Quaternion rotation);
	}
}