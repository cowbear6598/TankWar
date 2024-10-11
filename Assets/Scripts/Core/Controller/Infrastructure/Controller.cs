using Core.Controller.Domain;
using UnityEngine;
using VContainer;

namespace Core.Controller.Infrastructure
{
	public class Controller : IController
	{
		[Inject] private readonly PCInput _input;

		public Vector2 MoveAxis      => _input.MoveAxis;
		public Vector2 MousePosition => _input.MousePosition;

		public bool IsShoot => _input.IsShoot;
	}
}