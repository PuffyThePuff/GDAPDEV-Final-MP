using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private AudioClip slapSFX;
    private void Start()
    {
        GestureManager.Instance.OnSwipe += OnSwipe;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnSwipe -= OnSwipe;
    }

    public void OnSwipe(object sender, SwipeEventArgs args)
    {
        if (args.HitObject != null)
        {
            if(args.HitObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                if (args.SwipeDirection == enemy.SwipeWeakness)
                {
                    AudioManager.Instance.PlaySFX(slapSFX, 1);
                    enemy.Die();
                }
            }
            
        }
    }
}
