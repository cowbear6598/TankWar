using System;
using Core.Menu.Common;
using Core.Menu.Domain;
using Core.Misc.UI;
using Core.Network.Infrastructure.Views;
using Core.User.Domain.Adapters;
using MessagePipe;
using TMPro;
using UnityEngine;
using VContainer;

namespace Core.Menu.Infrastructure.UI
{
	public class UI_Menu : UI_Panel
	{
		[Inject] private readonly ISubscriber<OnMenuStateChanged> _onMenuStateChanged;

		[Inject] private readonly IUser _user;

		[SerializeField] private TMP_InputField _ipInputField;
		[SerializeField] private TMP_InputField _portInputField;
		[SerializeField] private TMP_InputField _nameInputField;

		private IDisposable _subscription;

		private void OnEnable()  => _subscription = _onMenuStateChanged.Subscribe(OnMenuStateChanged);
		private void OnDisable() => _subscription.Dispose();

		public void Button_Client()
		{
			_user.SetName(_nameInputField.text);

			CustomNetworkManager.Instance.Connect(_ipInputField.text, ushort.Parse(_portInputField.text), false);
		}

		public void Button_Host()
		{
			_user.SetName(_nameInputField.text);

			CustomNetworkManager.Instance.Connect(_ipInputField.text, ushort.Parse(_portInputField.text), true);
		}

		private void OnMenuStateChanged(OnMenuStateChanged e)
		{
			if (e.PrevState == MenuState.Connect)
				Hide();
			else if (e.State == MenuState.Connect)
				Show();
		}
	}
}