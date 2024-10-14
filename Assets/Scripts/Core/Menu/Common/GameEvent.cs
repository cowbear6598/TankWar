using Core.Menu.Domain;

namespace Core.Menu.Common
{
	public struct OnMenuStateChanged
	{
		public readonly MenuState PrevState;
		public readonly MenuState State;

		public OnMenuStateChanged(MenuState prevState, MenuState state)
		{
			PrevState = prevState;
			State     = state;
		}
	}
}