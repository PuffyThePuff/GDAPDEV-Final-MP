using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Killable
{
    [SerializeField] private AudioClip hurt;

    public Slider healthBar;

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHP;
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        AudioManager.Instance.PlaySFX(hurt);
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
            if (!gameObject.CompareTag(bullet.tagOfOrigin))
            {
                Damage(bullet.Damage);
                Destroy(bullet.gameObject);
            }
        }
    }

}
