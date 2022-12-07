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
	[SerializeField] Button UpgradableUpgradeButton;

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
	[SerializeField] float UpgradablePrice;

	[SerializeField] float Power1Price;
	[SerializeField] float Power2Price;

	float player_money;

	private void Update()
	{
		playerMoneyText.text = $"Money : {player_money}";

		FireRatePriceText.text = $"{FireRatePrice} Currency";
		BulletDamagePriceText.text = $"{BulletDamagePrice} Currency";
		HealthPriceText.text = $"{HealthPrice} Currency";
		UpgradablePriceText.text = $"{UpgradablePrice} Currency";
		Power1PriceText.text = $"{Power1Price} Currency";
		Power2PriceText.text = $"{Power2Price} Currency";

		if(player_money > 0)
		{

			if(player_money < FireRatePrice)
			{
				FireRateUpgradeButton.interactable = false;
			}
			else
			{
				FireRateUpgradeButton.interactable = true;
			}

			if (player_money < BulletDamagePrice)
			{
				BulletDamageUpgradeButton.interactable = false;
			}
			else
			{
				BulletDamageUpgradeButton.interactable = true;
			}

			if(player_money < HealthPrice)
			{
				HealthUpgradeButton.interactable = false;
			}
			else
			{
				HealthUpgradeButton.interactable = true;
			}

			if(player_money < UpgradablePrice)
			{
				UpgradableUpgradeButton.interactable = false;
			}
			else
			{
				UpgradableUpgradeButton.interactable = true;
			}

			if(player_money < Power1Price)
			{
				Power1UpgradeButton.interactable = false;
			}
			else
			{
				Power1UpgradeButton.interactable = true;
			}

			if(player_money < Power2Price)
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
			 UpgradableUpgradeButton.interactable = false;

			 Power1UpgradeButton.interactable = false;
			 Power2UpgradeButton.interactable = false;
		}
	}

	public void UpgradeFireRate()
	{

	}

	public void UpgradeDamage()
	{

	}

	public void UpgradeHealth()
	{

	}

	public void UpgradeStat4()
	{

	}

	public void BuyPower1()
	{

	}

	public void BuyPower2()
	{

	}
}
