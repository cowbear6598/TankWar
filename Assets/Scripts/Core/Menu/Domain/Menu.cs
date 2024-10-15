using Core.Menu.Common;
using MessagePipe;
using Mirror;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Menu.Domain
{
	public class Menu : IStartable
	{
		[Inject] private readonly IPublisher<OnMenuStateChanged> _onMenuStateChanged;

		public MenuState State { get; private set; }

		public void Start()
		{
			if (NetworkServer.active)
				ChangeState(MenuState.Room);
		}

		private void ChangeState(MenuState state)
		{
			var prevState = State;

			State = state;

			_onMenuStateChanged.Publish(new OnMenuStateChanged(prevState, state));
		}
	}
}