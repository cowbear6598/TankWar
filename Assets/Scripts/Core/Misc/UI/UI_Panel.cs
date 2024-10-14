using UnityEngine;

namespace Core.Misc.UI
{
	public class UI_Panel : MonoBehaviour
	{
		[SerializeField] private CanvasGroup _canvasGroup;

		protected virtual void Show()
		{
			_canvasGroup.alpha          = 1;
			_canvasGroup.blocksRaycasts = true;
			_canvasGroup.interactable   = true;
		}

		protected virtual void Hide()
		{
			_canvasGroup.alpha          = 0;
			_canvasGroup.blocksRaycasts = false;
			_canvasGroup.interactable   = false;
		}
	}
}