using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
	[SerializeField] protected int PremiumCurrency;
	public int PlayerCurrency;

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
		PlayerCurrency = 0;
	}

	public void UpdatePremiumCurrency(int currency)
	{
		PremiumCurrency += currency;
	}

}
