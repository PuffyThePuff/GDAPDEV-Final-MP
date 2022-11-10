using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum RockPaperScissors { rock, paper, scissors };

[RequireComponent(typeof(Collider))]
public class Enemy : Killable
{
    [SerializeField] private SwipeDirection swipeWeakness;

    private Currency enemyCurrency;

    private void Start()
    {
       
    }

    public SwipeDirection SwipeWeakness
    {
        get { return swipeWeakness; }
    }

    public override void Die()
    {
        base.Die();
        SpawnCurrency();
        Destroy(gameObject);
    }

    public void SpawnCurrency()
    {
        int i = 0;
        for (i = 0; i < 2; i++)
        {
            Currency newCoin = Instantiate(enemyCurrency, new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0), Quaternion.identity);
        }
    }
}
