using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
	[Header("Player Currency")]
	[SerializeField] protected int PremiumCurrency;
	public int PlayerCurrency;

	[Header("Upgrade Levels")]
	public int damageUpgradeLevel = 1;
	public int fireRateUpgradeLevel = 1;
	public int healthUpgradeLevel = 1;
	public int gunUpgradeLevel = 1;
	public int power1Stored = 0;
	public int power2Stored = 0;



	private static PlayerDataManager sharedInstance = null;
	public static PlayerDataManager instance
	{
		get
		{
			if (sharedInstance == null)
			{
				sharedInstance = new PlayerDataManager();
			}
			return sharedInstance;
		}
		set
		{
			sharedInstance = new PlayerDataManager();
		}
	}

	private void Awake()
	{
		if (PlayerDataManager.instance != null)
		{
			Debug.Log("PlayerDataManager Detected! Deleting new copy");
			Destroy(gameObject);
			return;
		}

		sharedInstance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void newGame()
	{
		PlayerCurrency = Config.startingMoney;

		damageUpgradeLevel = 1;
		fireRateUpgradeLevel = 1;
		healthUpgradeLevel = 1;
		gunUpgradeLevel = 1;

		power1Stored = 0;
		power2Stored = 0;
}

	public void UpdatePremiumCurrency(int currency)
	{
		PremiumCurrency += currency;
	}

}
