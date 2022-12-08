using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShip : Killable
{
    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);

		PlayerDataManager.instance.PlayerCurrency += 1;
    }

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            //Debug.Log($"{tag} || {bullet.tagOfOrigin}");
            if (!gameObject.CompareTag(bullet.tagOfOrigin))
            {
                Damage(bullet.Damage);
                Destroy(bullet.gameObject);
            } 
        }
    }
}
