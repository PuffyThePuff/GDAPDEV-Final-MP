using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Killable : MonoBehaviour
{
    [SerializeField] private int startingHP;
    [SerializeField] private int maxHP;

    private int currentHP;
    private bool isDead;

    public void OnEnable()
    {
        isDead = false;
    }

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
        isDead = true;
        //Destroy(gameObject);
    }

    
}
