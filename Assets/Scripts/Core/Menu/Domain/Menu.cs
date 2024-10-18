using System;
using Core.Menu.Common;
using Core.Network.Common;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Core.Menu.Domain
{
	public class Menu : IInitializable, IDisposable
	{
		[Inject] private readonly IPublisher<OnMenuStateChanged> _onMenuStateChanged;
		[Inject] private readonly ISubscriber<OnServerConnected> _onServerConnected;
		[Inject] private readonly ISubscriber<OnClientConnected> _onClientConnected;

		private MenuState State = MenuState.Connect;

		private IDisposable _subscription;

		public void Initialize()
		{
			var bag = DisposableBag.CreateBuilder();

			_onServerConnected.Subscribe(OnServerConnected).AddTo(bag);
			_onClientConnected.Subscribe(OnClientConnected).AddTo(bag);

			_subscription = bag.Build();
		}

		public void Dispose() => _subscription.Dispose();

		private void ChangeState(MenuState state)
		{
			if (State == state)
				return;

			var prevState = State;

			State = state;

			_onMenuStateChanged.Publish(new OnMenuStateChanged(prevState, state));
		}

		private void OnServerConnected(OnServerConnected e) => ChangeState(MenuState.Room);
		private void OnClientConnected(OnClientConnected e) => ChangeState(MenuState.Room);
	}
}