using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

//public enum RockPaperScissors { rock, paper, scissors };

[RequireComponent(typeof(Collider))]
public class Enemy : Killable
{
    [SerializeField] private SwipeDirection swipeWeakness;

	[SerializeField] private float maxTime = 10.0f;
	private float timeLeft = 10.0f;
	GameObject gameManager;
    private Currency enemyCurrency;
	

    private void Start()
    {
		gameManager = GameObject.FindGameObjectWithTag("GameController");
	}
	private void FixedUpdate()
	{
		if(timeLeft > 0)
		{
			timeLeft -= Time.deltaTime;
		}
		else
		{
			timeLeft = maxTime;
			inflictDmg();
			Die();
		}
	}

	public SwipeDirection SwipeWeakness
    {
        get { return swipeWeakness; }
    }

	private void inflictDmg()
	{
		if (Config.Singleton != null && Config.infiniteHealth == false)
        {
			Debug.Log("Damage taken!");
			gameManager.GetComponent<Player>().updateHpValue(-1);
        }
		else
        {
			Debug.Log("Infinite Health On!");
        }
	}

    public override void Die()
    {
        base.Die();
        //SpawnCurrency();
        Destroy(gameObject);
    }

    /*
    public void SpawnCurrency()
    {
        int i = 0;
        for (i = 0; i < 2; i++)
        {
           // Currency newCoin = Instantiate(enemyCurrency, new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0), Quaternion.identity);
        }
    }
    */
}
