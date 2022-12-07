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
                GetComponentInChildren<SpriteRenderer>().color = Color.gray;
                break;
            case 1:
                gameObject.tag = "paper";
                GetComponentInChildren<SpriteRenderer>().color = Color.white;
                break;
            case 2:
                gameObject.tag = "scissor";
                GetComponentInChildren<SpriteRenderer>().color = Color.red;
                break;
            default:
                gameObject.tag = "rock";
                break;
        }
    }
}
