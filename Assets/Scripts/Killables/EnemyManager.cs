using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private AudioClip slapSFX;
    private void Start()
    {
        GestureManager.Instance.OnSwipe += OnSwipe;
        GestureManager.Instance.OnSpread += OnSpread; 
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnSwipe -= OnSwipe;
        GestureManager.Instance.OnSpread -= OnSpread;
    }

    public void OnSwipe(object sender, SwipeEventArgs args)
    {
        if (args.HitObject != null)
        {
            if(args.HitObject.TryGetComponent(out Enemy enemy))
            {
                if (args.SwipeDirection == enemy.SwipeWeakness)
                {
                    AudioManager.Instance.PlaySFX(slapSFX);
                    enemy.Die();
                }
            }
            
        }
    }

    public void OnSpread(object sender, SpreadEventArgs args)
    {
        if (args.HitObject != null)
        {
            if (args.HitObject.TryGetComponent(out Enemy2 enemy2))
            {
                if (args.SpreadOrPinch == enemy2.GestureWeakness)
                {
                    AudioManager.Instance.PlaySFX(slapSFX);
                    enemy2.Die();
                }
            }

        }
    }
}
