using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Killable : MonoBehaviour
{
    [SerializeField] private int startingHP;
    private int currentHP;
    private int maxHP;
    private bool isDead;

    public virtual void Damage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        if(currentHP <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    
}
