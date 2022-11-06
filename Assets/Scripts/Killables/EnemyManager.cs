using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
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
                    enemy.Die();
            }
            
        }
    }
}
