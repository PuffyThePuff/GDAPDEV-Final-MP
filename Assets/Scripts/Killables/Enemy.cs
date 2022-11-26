using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

//public enum RockPaperScissors { rock, paper, scissors };

[RequireComponent(typeof(Collider))]
public class Enemy : Killable
{
	[SerializeField] private Animator animator;
    [SerializeField] private SwipeDirection swipeWeakness;

	[SerializeField] private float maxTime = 10.0f;
	private float timeLeft = 10.0f;
	GameObject gameManager;
    private Currency enemyCurrency;

	private static readonly int SWIPE_UP = Animator.StringToHash("Swipe_UP");
    private static readonly int SWIPE_DOWN = Animator.StringToHash("Swipe_DOWN");
    private static readonly int SWIPE_LEFT = Animator.StringToHash("Swipe_LEFT");
    private static readonly int SWIPE_RIGHT = Animator.StringToHash("Swipe_RIGHT");
    
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
			Attack();
			Die();
		}
	}

	public SwipeDirection SwipeWeakness
    {
        get { return swipeWeakness; }
    }

	private void Attack()
	{
		if (Config.Singleton != null && Config.infiniteHealth == false)
        {
			Debug.Log("Damage taken!");
			gameManager.GetComponent<Player>().Damage(-1);
        }
		else
        {
			Debug.Log("Infinite Health On!");
        }
	}

    public override void Die()
    {
        base.Die();

		switch (swipeWeakness)
		{
			case SwipeDirection.RIGHT:
				animator.Play(SWIPE_RIGHT);
				break;
			case SwipeDirection.LEFT:
                animator.Play(SWIPE_LEFT);
                break;
			case SwipeDirection.UP:
                animator.Play(SWIPE_UP);
                break;
			case SwipeDirection.DOWN:
                animator.Play(SWIPE_DOWN);
                break;
		}
        //SpawnCurrency();
        //Destroy(gameObject);
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
