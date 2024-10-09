using UnityEngine;

namespace Core.Controller
{
	public interface IController
	{
		Vector2 MoveAxis      { get; }
		Vector2 MousePosition { get; }
	}
}