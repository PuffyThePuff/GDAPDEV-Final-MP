using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
	[Header("Player Money Text")]
	[SerializeField] TMP_Text playerMoneyText;

	[Header("Buy Buttons")]
	[SerializeField] Button FireRateUpgradeButton;
	[SerializeField] Button BulletDamageUpgradeButton;

	[SerializeField] Button HealthUpgradeButton;
	[SerializeField] Button Stat4UpgradePrice;

	[SerializeField] Button Power1UpgradeButton;
	[SerializeField] Button Power2UpgradeButton;

	[Header("Upgrade Price Text")]
	[SerializeField] TMP_Text FireRatePriceText;
	[SerializeField] TMP_Text BulletDamagePriceText;

	[SerializeField] TMP_Text HealthPriceText;
	[SerializeField] TMP_Text UpgradablePriceText;

	[SerializeField] TMP_Text Power1PriceText;
	[SerializeField] TMP_Text Power2PriceText;

	[Header("Upgrade Prices")]
	[SerializeField] float FireRatePrice;
	[SerializeField] float BulletDamagePrice;

	[SerializeField] float HealthPrice;
	[SerializeField] float Stat4Price;

	[SerializeField] float Power1Price;
	[SerializeField] float Power2Price;

	private void Update()
	{
		playerMoneyText.text = $"Money : {PlayerDataManager.instance.PlayerCurrency}";

		FireRatePriceText.text = $"{FireRatePrice} Currency";
		BulletDamagePriceText.text = $"{BulletDamagePrice} Currency";
		HealthPriceText.text = $"{HealthPrice} Currency";
		UpgradablePriceText.text = $"{Stat4Price} Currency";
		Power1PriceText.text = $"{Power1Price} Currency";
		Power2PriceText.text = $"{Power2Price} Currency";

		if(PlayerDataManager.instance.PlayerCurrency > 0)
		{

			if(PlayerDataManager.instance.PlayerCurrency < FireRatePrice)
			{
				FireRateUpgradeButton.interactable = false;
			}
			else
			{
				FireRateUpgradeButton.interactable = true;
			}

			if (PlayerDataManager.instance.PlayerCurrency < BulletDamagePrice)
			{
				BulletDamageUpgradeButton.interactable = false;
			}
			else
			{
				BulletDamageUpgradeButton.interactable = true;
			}

			if(PlayerDataManager.instance.PlayerCurrency < HealthPrice)
			{
				HealthUpgradeButton.interactable = false;
			}
			else
			{
				HealthUpgradeButton.interactable = true;
			}

			if(PlayerDataManager.instance.PlayerCurrency < Stat4Price)
			{
				Stat4UpgradePrice.interactable = false;
			}
			else
			{
				Stat4UpgradePrice.interactable = true;
			}

			if(PlayerDataManager.instance.PlayerCurrency < Power1Price)
			{
				Power1UpgradeButton.interactable = false;
			}
			else
			{
				Power1UpgradeButton.interactable = true;
			}

			if(PlayerDataManager.instance.PlayerCurrency < Power2Price)
			{
				Power2UpgradeButton.interactable = false;
			}
			else
			{
				Power2UpgradeButton.interactable = true;
			}
		}
		else
		{
			 FireRateUpgradeButton.interactable = false;
			 BulletDamageUpgradeButton.interactable = false;

			 HealthUpgradeButton.interactable = false;
			 Stat4UpgradePrice.interactable = false;

			 Power1UpgradeButton.interactable = false;
			 Power2UpgradeButton.interactable = false;
		}
	}

	public void UpgradeFireRate()
	{
		PlayerDataManager.instance.PlayerCurrency -= (int)FireRatePrice;
	}

	public void UpgradeDamage()
	{
		PlayerDataManager.instance.PlayerCurrency -= (int)BulletDamagePrice;

	}

	public void UpgradeHealth()
	{
		PlayerDataManager.instance.PlayerCurrency -= (int)HealthPrice;

	}

	public void UpgradeStat4()
	{
		PlayerDataManager.instance.PlayerCurrency -= (int)Stat4Price;

	}

	public void BuyPower1()
	{
		PlayerDataManager.instance.PlayerCurrency -= (int)Power1Price;

	}

	public void BuyPower2()
	{
		PlayerDataManager.instance.PlayerCurrency -= (int)Power2Price;

	}
}
