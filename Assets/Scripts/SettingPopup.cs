using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SettingPopup : MonoBehaviour
{
	[SerializeField]
	private GameObject popup;

	[SerializeField]
	private Text musicStatusLabel;

	[SerializeField]
	private Text soundStatusLabel;

	[SerializeField]
	private Image musicIconImage;

	[SerializeField]
	private Image soundIconImage;

	[SerializeField]
	private Image musicButtonImage;

	[SerializeField]
	private Image soundButtonImage;

	[SerializeField]
	private Sprite musicEnableSprite;

	[SerializeField]
	private Sprite soundEnableSprite;

	[SerializeField]
	private Sprite musicDisableSprite;

	[SerializeField]
	private Sprite soundDisableSprite;

	[SerializeField]
	private Sprite buttonEnableSprite;

	[SerializeField]
	private Sprite buttonDisableSprite;

	[SerializeField]
	private Color enableOutlineColor;

	[SerializeField]
	private Color disableOutlinecolor;

	public void MusicChange(bool value)
	{
		this.musicIconImage.sprite = ((!value) ? this.musicEnableSprite : this.musicDisableSprite);
		this.musicButtonImage.sprite = ((!value) ? this.buttonEnableSprite : this.buttonDisableSprite);
		this.musicStatusLabel.gameObject.GetComponent<Outline>().effectColor = ((!value) ? this.enableOutlineColor : this.disableOutlinecolor);

		var title = YandexGame.lang == "ru" ? "Музыка" : "Music";
		var state = (!value) ? "On" : "Off";
		switch (state)
		{
			case "Off":
				state = YandexGame.lang == "ru" ? "Выкл." : "Off";
				break;
			case "On":
				state = YandexGame.lang == "ru" ? "Вкл." : "On";
				break;
		}
		GameUtilities.String.ToText(this.musicStatusLabel, $"{title}: {state}");
	}

	public void SoundChange(bool value)
	{
		this.soundIconImage.sprite = ((!value) ? this.soundEnableSprite : this.soundDisableSprite);
		this.soundButtonImage.sprite = ((!value) ? this.buttonEnableSprite : this.buttonDisableSprite);
		this.soundStatusLabel.gameObject.GetComponent<Outline>().effectColor = ((!value) ? this.enableOutlineColor : this.disableOutlinecolor);

		var title = YandexGame.lang == "ru" ? "Звук" : "Sound";
		var state = (!value) ? "On" : "Off";
		switch (state)
		{
			case "Off":
				state = YandexGame.lang == "ru" ? "Выкл." : "Off";
				break;
			case "On":
				state = YandexGame.lang == "ru" ? "Вкл." : "On";
				break;
		}
		GameUtilities.String.ToText(this.soundStatusLabel, $"{title}: {state}");
	}

	public void Show(bool value)
	{
		if (value)
		{
			Singleton<SoundManager>.Instance.Play("Popup");
		}
		this.popup.SetActive(value);
	}
}
