using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum RockPaperScissors { rock, paper, scissors };

[RequireComponent(typeof(Collider))]
public class Enemy : Killable
{
    [SerializeField] private EnemyAnimationManager m_AnimationManager;
    [SerializeField] private SwipeDirection swipeWeakness;
    public SwipeDirection SwipeWeakness
    {
        get { return swipeWeakness; }
    }

    public override void Die()
    {
        base.Die();
        m_AnimationManager.PlayDeathAnim(swipeWeakness);
    }

    
}
