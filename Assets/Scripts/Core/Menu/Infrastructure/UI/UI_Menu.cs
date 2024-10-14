using Core.Misc.UI;
using Core.Network.Infrastructure.Adapters;
using TMPro;
using UnityEngine;
using VContainer;

namespace Core.Menu.Infrastructure.UI
{
	public class UI_Menu : UI_Panel
	{
		[Inject] private readonly INetworkFacade _networkFacade;

		[SerializeField] private TMP_InputField _ipInputField;
		[SerializeField] private TMP_InputField _portInputField;

		public void Button_Connect()
		{
			_networkFacade.Connect(_ipInputField.text, ushort.Parse(_portInputField.text));
		}
	}
}