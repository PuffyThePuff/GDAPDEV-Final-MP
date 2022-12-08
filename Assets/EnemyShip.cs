using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShip : Killable
{
    public override void initialize()
    {
        RandomizeType();
    }

    public override void Die()
    {
        base.Die();
        RandomizeType();
        gameObject.SetActive(false);

		PlayerDataManager.instance.PlayerCurrency += 1;
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