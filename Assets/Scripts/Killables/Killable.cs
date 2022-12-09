using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Killable : MonoBehaviour
{
	[SerializeField] protected int startingHP;
	[SerializeField] protected int maxHP;

	protected int currentHP;
	public bool isDead { get; private set; } = false;

	public void Start()
	{
		initialize();
	}

	public virtual void initialize()
	{
		currentHP = startingHP;
		isDead = false;
	}

	public virtual void Damage(int damage)
	{
		currentHP -= damage;
		currentHP = Mathf.Clamp(currentHP, 0, maxHP);
		if (currentHP <= 0)
		{
			Die();
		}
	}

	public virtual void Die()
	{
		isDead = true;
		//Destroy(gameObject);
	}

	public void SelfDestruct()
	{
		Destroy(gameObject);
	}

}
