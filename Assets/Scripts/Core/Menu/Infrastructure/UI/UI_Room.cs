using System;
using Core.Menu.Common;
using Core.Menu.Domain;
using Core.Misc.UI;
using MessagePipe;
using VContainer;

namespace Core.Menu.Infrastructure.UI
{
	public class UI_Room : UI_Panel
	{
		[Inject] private readonly ISubscriber<OnMenuStateChanged> _onMenuStateChanged;

		private IDisposable _subscription;

		private void OnEnable()  => _subscription = _onMenuStateChanged.Subscribe(OnMenuStateChanged);
		private void OnDisable() => _subscription.Dispose();

		private void OnMenuStateChanged(OnMenuStateChanged e)
		{
			if (e.PrevState == MenuState.Room)
				Hide();
			else if (e.State == MenuState.Room)
				Show();
		}
	}
}