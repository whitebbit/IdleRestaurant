using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SellManagerPopup : MonoBehaviour
{
	private Action onSellHalf;

	private Action onSellFull;

	[SerializeField]
	private Text price;

	[SerializeField]
	private Text duration;

	[SerializeField]
	private Text effective;

	[SerializeField]
	private Text experience;

	[SerializeField]
	private Image skillIamge;

	[SerializeField]
	private Image avatarImage;

	[SerializeField]
	private GameObject popup;

	public void Show(ManagerProfile profile, Action sellHalf, Action sellFull)
	{
        var upgradeCost = YandexGame.lang == "ru" ? " Стоимость обновления" : " Upgrade Cost";
        var walkingSpeed = YandexGame.lang == "ru" ? " Скорость ходьбы" : " Walking Speed";
        var cookingSpeed = YandexGame.lang == "ru" ? " Скорость приготовления" : " Cooking Speed";
        var loadingSpeed = YandexGame.lang == "ru" ? " Скорость загрузки" : " Loading Speed";
        var movementSpeed = YandexGame.lang == "ru" ? " Скорость перемещения" : " Movement Speed";
        var loadExpansion = YandexGame.lang == "ru" ? " Увеличение нагрузки" : " Load Expansion";
        var duration = YandexGame.lang == "ru" ? "Продолжительность: " : "Duration: ";

        this.onSellHalf = sellHalf;
		this.onSellFull = sellFull;
		this.skillIamge.sprite = Singleton<GameProcess>.Instance.GetManagerSkillSprite(profile.skill, true);
		this.avatarImage.sprite = Singleton<GameProcess>.Instance.GetManagerAvatarSprite(profile.experience);
		GameUtilities.String.ToText(this.price, GameUtilities.Currencies.Convert(profile.price));
		GameUtilities.String.ToText(this.experience, Enum.GetName(typeof(Experience), profile.experience));
		this.experience.color = Singleton<GameProcess>.Instance.GetManagerTextColor(profile.experience);
		this.experience.gameObject.GetComponent<Outline>().effectColor = Singleton<GameProcess>.Instance.GetManagerBorderColor(profile.experience);
		GameUtilities.String.ToText(this.duration, duration + GameUtilities.DateTime.Convert(Singleton<GameProcess>.Instance.GetManagerSkillDuration(profile.experience, profile.skill)));
		switch (profile.skill)
		{
		case ManagerSkill.UpgradeCost:
			GameUtilities.String.ToText(this.effective, "-" + Singleton<GameProcess>.Instance.GetManagerSkillEffective(profile.experience, profile.skill) + upgradeCost);
			break;
		case ManagerSkill.WalkingSpeed:
			GameUtilities.String.ToText(this.effective, "x" + Singleton<GameProcess>.Instance.GetManagerSkillEffective(profile.experience, profile.skill) + walkingSpeed);
			break;
		case ManagerSkill.CookingSpeed:
			GameUtilities.String.ToText(this.effective, "x" + Singleton<GameProcess>.Instance.GetManagerSkillEffective(profile.experience, profile.skill) + cookingSpeed);
			break;
		case ManagerSkill.LoadingSpeed:
			GameUtilities.String.ToText(this.effective, "x" + Singleton<GameProcess>.Instance.GetManagerSkillEffective(profile.experience, profile.skill) + loadingSpeed);
			break;
		case ManagerSkill.MovementSpeed:
			GameUtilities.String.ToText(this.effective, "x" + Singleton<GameProcess>.Instance.GetManagerSkillEffective(profile.experience, profile.skill) + movementSpeed);
			break;
		case ManagerSkill.LoadExpansion:
			GameUtilities.String.ToText(this.effective, "x" + Singleton<GameProcess>.Instance.GetManagerSkillEffective(profile.experience, profile.skill) + loadExpansion);
			break;
		}
		Singleton<SoundManager>.Instance.Play("Popup");
		this.popup.SetActive(true);
	}

	public void Sell(bool value)
	{
		if (value)
		{
			AdsControl.Instance.PlayDelegateRewardVideo(delegate
			{
				this.onSellFull();
				this.Cancel();
			});
		}
		else
		{
			this.onSellHalf();
			this.Cancel();
		}
	}

	public void Cancel()
	{
		this.onSellHalf = null;
		this.onSellFull = null;
		this.popup.SetActive(false);
	}
}
