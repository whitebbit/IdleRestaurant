using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class KitchenPopup : Singleton<KitchenPopup>
{
	[SerializeField]
	private GameObject popup;

	[SerializeField]
	private Text title;

	[SerializeField]
	private Text currentLevel;

	[SerializeField]
	private Text nextLevel;

	[SerializeField]
	private Text currentTotal;

	[SerializeField]
	private Text nextTotal;

	[SerializeField]
	private Text currentTransporter;

	[SerializeField]
	private Text nextTransporter;

	[SerializeField]
	private Text currentCapacity;

	[SerializeField]
	private Text nextCapacity;

	[SerializeField]
	private Text currentWorking;

	[SerializeField]
	private Text nextWorking;

	[SerializeField]
	private Text currentSpeed;

	[SerializeField]
	private Text nextSpeed;

	[SerializeField]
	private Image currentLevelFill;

	[SerializeField]
	private Image nextLevelFill;

	[SerializeField]
	private Text bonusDiamond;

	[SerializeField]
	private Text levelNumber;

	[SerializeField]
	private Text upgradePrice;

	[SerializeField]
	private Image upgradeButton;

	[SerializeField]
	private Sprite enableSprite;

	[SerializeField]
	private Sprite disableSprite;

	[SerializeField]
	private Transform handle;

	[SerializeField]
	private Transform[] selection;

	private int upgradeStep = 1;

	private KitchenController kitchenController;

	private void Start()
	{
		GameManager expr_05 = Singleton<GameManager>.Instance;
		expr_05.onCashChange = (Action<double>)Delegate.Combine(expr_05.onCashChange, new Action<double>(this.OnCashChange));
	}

	public void SelectUpgradeStep(int value)
	{
		int num = 0;
		this.upgradeStep = value;
		int num2 = this.upgradeStep;
		if (num2 != 0)
		{
			if (num2 != 1)
			{
				if (num2 != 10)
				{
					if (num2 == 50)
					{
						num = 2;
					}
				}
				else
				{
					num = 1;
				}
			}
			else
			{
				num = 0;
			}
		}
		else
		{
			num = 3;
		}
		this.handle.transform.localPosition = this.selection[num].localPosition;
		if (!this.IsMaxLevel())
		{
			this.Show(this.kitchenController);
		}
	}

	private string  seconds => YandexGame.lang == "ru" ? "/c." : "/s";
	public void Show(KitchenController kitchenController)
	{
		this.kitchenController = kitchenController;
		var title = YandexGame.lang == "ru" ? "Кухня ур. " : "Kitchen Lv. ";
		var level = YandexGame.lang == "ru" ? "Уровень " : "Level ";
		var boost = YandexGame.lang == "ru" ? "Следующий бустер\n на уровне  " : "Next boost at level ";
		var boostMax = YandexGame.lang == "ru" ? "Максимальный бустер" : "Boost maximum.";
		
		GameUtilities.String.ToText(this.title, title + kitchenController.kitchenData.level.ToString());
		int lastBonusAtLevel = Singleton<GameProcess>.Instance.GetLastBonusAtLevel(kitchenController.kitchenData.level, Location.Restaurant);
		int nextBonusAtLevel = Singleton<GameProcess>.Instance.GetNextBonusAtLevel(kitchenController.kitchenData.level, Location.Restaurant);

		GameUtilities.String.ToText(this.currentLevel, level + kitchenController.kitchenData.level.ToString());
		GameUtilities.String.ToText(this.nextLevel, (nextBonusAtLevel != 2147483647) ? (boost + nextBonusAtLevel.ToString()) : boostMax);
		this.currentLevelFill.fillAmount = ((nextBonusAtLevel != 2147483647) ? ((float)(kitchenController.kitchenData.level - lastBonusAtLevel) / (float)(nextBonusAtLevel - lastBonusAtLevel)) : 1f);
		KitchenProperties kitchenProperties = kitchenController.kitchenProperties;
		GameUtilities.String.ToText(this.currentTotal, GameUtilities.Currencies.Convert(kitchenProperties.totalExtraction) + seconds);
		GameUtilities.String.ToText(this.currentTransporter, kitchenProperties.transporter.ToString());
		GameUtilities.String.ToText(this.currentSpeed, Math.Round((double)kitchenProperties.walkingSpeed, 2).ToString());
		GameUtilities.String.ToText(this.currentWorking, GameUtilities.Currencies.Convert(kitchenProperties.workingSpeed) + seconds);
		GameUtilities.String.ToText(this.currentCapacity, GameUtilities.Currencies.Convert(kitchenProperties.transporterCapacity));
		var max = YandexGame.lang == "ru" ? "Макс." : "Max";
		if (this.IsMaxLevel())
		{
			GameUtilities.String.ToText(this.nextTotal, max);
			GameUtilities.String.ToText(this.nextSpeed, max);
			GameUtilities.String.ToText(this.nextTransporter, max);
			GameUtilities.String.ToText(this.nextWorking, max);
			GameUtilities.String.ToText(this.levelNumber, max);
			GameUtilities.String.ToText(this.nextCapacity, max);
			GameUtilities.String.ToText(this.upgradePrice, "0");
			this.upgradeButton.sprite = this.disableSprite;
		}
		else
		{
			int num;
			if (this.upgradeStep != 0)
			{
				num = this.upgradeStep;
			}
			else
			{
				num = Singleton<GameProcess>.Instance.GetMaxUpgradeLevel(Singleton<GameManager>.Instance.database.cash, kitchenController.boostController.upgradeCostReduced, kitchenController.kitchenData.level, Location.Kitchen, kitchenController.kitchenData.floor);
			}
			num = Mathf.Clamp(num, 1, Singleton<GameProcess>.Instance.GetMaxLevel(Location.Kitchen) - 1);
			float distance = Vector3.Distance(kitchenController.gatheringPoint.position, kitchenController.exploitedPoint.position);
			KitchenProperties kitchenProperties2 = Singleton<GameProcess>.Instance.GetKitchenProperties(distance, kitchenController.kitchenData.floor, kitchenController.kitchenData.level + num, 1f);
			GameUtilities.String.ToText(this.nextCapacity, "+" + GameUtilities.Currencies.Convert(kitchenProperties2.transporterCapacity - kitchenProperties.transporterCapacity));
			GameUtilities.String.ToText(this.nextTransporter, "+" + (kitchenProperties2.transporter - kitchenProperties.transporter).ToString());
			GameUtilities.String.ToText(this.nextSpeed, "+" + Math.Round((double)(kitchenProperties2.walkingSpeed - kitchenProperties.walkingSpeed), 2).ToString());
			GameUtilities.String.ToText(this.nextWorking, "+" + GameUtilities.Currencies.Convert(kitchenProperties2.workingSpeed - kitchenProperties.workingSpeed) + seconds);
			GameUtilities.String.ToText(this.nextTotal, "+" + GameUtilities.Currencies.Convert(kitchenProperties2.totalExtraction - kitchenProperties.totalExtraction) + seconds);
			double num2 = Singleton<GameProcess>.Instance.GetUpgradePrice(kitchenController.kitchenData.level, num, kitchenController.boostController.upgradeCostReduced, Location.Kitchen, kitchenController.kitchenData.floor);
			var levelUpX = YandexGame.lang == "ru" ? "Повышение\nуровня X" : "Level Up x";
			GameUtilities.String.ToText(this.levelNumber, levelUpX + num.ToString());
			GameUtilities.String.ToText(this.upgradePrice, GameUtilities.Currencies.Convert(num2));
			this.upgradeButton.sprite = ((Singleton<GameManager>.Instance.database.cash < num2 || kitchenController.kitchenData.level + num > Singleton<GameProcess>.Instance.GetMaxLevel(Location.Kitchen)) ? this.disableSprite : this.enableSprite);
			this.nextLevelFill.fillAmount = ((nextBonusAtLevel != 2147483647) ? ((float)(kitchenController.kitchenData.level + num - lastBonusAtLevel) / (float)(nextBonusAtLevel - lastBonusAtLevel)) : 1f);
			int diamondBonus = Singleton<GameProcess>.Instance.GetDiamondBonus(kitchenController.kitchenData.level + num, kitchenController.kitchenData.level, Location.Elevator);
			GameUtilities.String.ToText(this.bonusDiamond, "+" + diamondBonus.ToString());
			this.bonusDiamond.gameObject.SetActive(diamondBonus > 0);
		}
		if (!this.popup.activeInHierarchy)
		{
			Singleton<SoundManager>.Instance.Play("Popup");
			this.popup.SetActive(true);
		}
	}

	public void Close()
	{
		this.kitchenController = null;
		this.popup.SetActive(false);
	}

	public void Upgrade()
	{
		if (this.IsMaxLevel())
		{
			return;
		}
		int num;
		if (this.upgradeStep != 0)
		{
			num = this.upgradeStep;
		}
		else
		{
			num = Singleton<GameProcess>.Instance.GetMaxUpgradeLevel(Singleton<GameManager>.Instance.database.cash, this.kitchenController.boostController.upgradeCostReduced, this.kitchenController.kitchenData.level, Location.Kitchen, this.kitchenController.kitchenData.floor);
		}
		num = Mathf.Clamp(num, 1, Singleton<GameProcess>.Instance.GetMaxLevel(Location.Kitchen) - 1);
		double num2 = Singleton<GameProcess>.Instance.GetUpgradePrice(this.kitchenController.kitchenData.level, num, this.kitchenController.boostController.upgradeCostReduced, Location.Kitchen, this.kitchenController.kitchenData.floor);
		if (Singleton<GameManager>.Instance.database.cash < num2)
		{
			return;
		}
		int diamondBonus = Singleton<GameProcess>.Instance.GetDiamondBonus(this.kitchenController.kitchenData.level + num, this.kitchenController.kitchenData.level, Location.Kitchen);
		Singleton<GameManager>.Instance.SetDiamond(diamondBonus);
		this.kitchenController.kitchenData.level += num;
		this.kitchenController.Upgrade();
		Singleton<GameManager>.Instance.SetCash(-num2);
		Singleton<GameManager>.Instance.IdleCashCompute();
		Singleton<SoundManager>.Instance.Play("Upgrade");
		this.Show(this.kitchenController);
	}

	private bool IsMaxLevel()
	{
		int maxLevel = Singleton<GameProcess>.Instance.GetMaxLevel(Location.Kitchen);
		return this.kitchenController.kitchenData.level == maxLevel;
	}

	private void OnCashChange(double cash)
	{
		if (!this.popup.activeInHierarchy || this.IsMaxLevel())
		{
			return;
		}
		this.Show(this.kitchenController);
	}
}
