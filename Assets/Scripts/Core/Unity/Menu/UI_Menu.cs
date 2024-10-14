﻿using Core.Network.Infrastructure.Adapters;
using TMPro;
using UnityEngine;
using VContainer;

namespace Core.Unity.Menu
{
	public class UI_Menu : MonoBehaviour
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