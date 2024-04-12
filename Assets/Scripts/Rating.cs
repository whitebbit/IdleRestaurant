using System;
using UnityEngine;

public class Rating : MonoBehaviour
{
	[SerializeField]
	private GameObject popup;

	[SerializeField]
	private string marketURL;

	public void Show(bool value)
	{
		this.popup.SetActive(value);
		if (value)
		{
			
		}
	}

	public void Rate()
	{
		this.Show(false);
		Application.OpenURL(this.marketURL);
	}

	public void Close()
	{
		this.Show(false);
	}
}
