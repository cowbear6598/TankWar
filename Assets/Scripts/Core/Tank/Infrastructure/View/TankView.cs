﻿using Core.Tank.Application.Adapters;
using UnityEngine;

namespace Core.Tank.Infrastructure.View
{
	public class TankView : MonoBehaviour, ITankView
	{
		[SerializeField] private Transform _bodyTransform;

		public void UpdatePosition(Vector3        position) => transform.position = position;
		public void UpdateBodyRotation(Quaternion rotation) => _bodyTransform.rotation = rotation;
	}
}