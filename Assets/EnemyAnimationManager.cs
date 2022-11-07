using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    private Animator enemyAnim;

    private static readonly int DieDOWN = Animator.StringToHash("Enemy_Die_DOWN");
    private static readonly int DieUP = Animator.StringToHash("Enemy_Die_UP");
    private static readonly int DieLEFT = Animator.StringToHash("Enemy_Die_LEFT");
    private static readonly int DieRIGHT = Animator.StringToHash("Enemy_Die_RIGHT");

    private void Start()
    {
        enemyAnim = GetComponent<Animator>();
    }

    public void PlayDeathAnim(SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeDirection.DOWN:
                enemyAnim.Play(DieDOWN);
                break;
            case SwipeDirection.UP:
                enemyAnim.Play(DieUP);
                break;
            case SwipeDirection.LEFT:
                enemyAnim.Play(DieLEFT);
                break;
            case SwipeDirection.RIGHT:
                enemyAnim.Play(DieRIGHT);
                break;
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
