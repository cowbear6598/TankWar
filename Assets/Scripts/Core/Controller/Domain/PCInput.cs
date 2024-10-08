using System;
using UnityEngine;
using VContainer.Unity;

namespace Core.Controller.Domain
{
	public class PCInput : IInitializable, IDisposable
	{
		private readonly ControlMap _controlMap = new();

		public void Initialize() => _controlMap.Enable();
		public void Dispose()    => _controlMap.Disable();

		public Vector2 MoveAxis => _controlMap.Player.Move.ReadValue<Vector2>();
		public bool    IsShoot  => _controlMap.Player.Shoot.triggered;
	}
}