using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Killable
{
	[SerializeField] private TMP_Text PlayerHPText;
    [SerializeField] private Animator playerAnimator;

    private static readonly int _Damage = Animator.StringToHash("Damage");
    void Update()
    {
		PlayerHPText.SetText($"HP: {currentHP}");
    }

    public override void Damage(int damage)
    {
        if (isDead) return;
        if (Config.Singleton != null && Config.infiniteHealth == false)
        {
            Debug.Log("Damage taken!");
            base.Damage(damage);
            //player.Damage(1);
        }
        else
        {
            Debug.Log("Infinite Health On!");
        }
        
        PlayerHPText.SetText($"HP: {currentHP}");

        if(playerAnimator != null)
            playerAnimator.Play(_Damage);

        if (currentHP <= 0)
        {
            Die();
        }
            
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Died");
        GameOverManager.Instance.OnGameOver(GameOverState.lose);
        CancelInvoke("Die");
    }
}
