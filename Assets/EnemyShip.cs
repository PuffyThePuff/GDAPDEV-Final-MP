using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShip : Killable
{
    [SerializeField] private bool isBoss;
	EnemyPool pool;

	private void Awake()
	{
		pool = FindObjectOfType<EnemyPool>();
	}

	public override void initialize()
    {
        RandomizeType();
    }

    public override void Die()
    {
        base.Die();
        RandomizeType();
		pool.setUnused(this);
        PlayerDataManager.instance.PlayerCurrency += 1;
        ScoreManager.Singleton.addScore(1);

        if (isBoss)
        {
            GameOverManager.Instance.OnGameOver(GameOverState.win);
        }

        gameObject.SetActive(false);
    }

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            //Debug.Log($"{tag} || {bullet.tagOfOrigin}");
            //check if same typing
            if (gameObject.CompareTag(bullet.tag))
            {
                Damage(bullet.Damage);
                Destroy(bullet.gameObject);
            } 
        }
    }

    private void RandomizeType()
    {
        int rng = Random.Range(0, 3);

        switch (rng)
        {
            case 0:
                gameObject.tag = "rock";
                break;
            case 1:
                gameObject.tag = "paper";
                break;
            case 2:
                gameObject.tag = "scissor";
                break;
            default:
                gameObject.tag = "rock";
                break;
        }
    }
}