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

    private void Start()
    {
        GestureManager.Instance.OnSwipe += OnSwipe;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnSwipe -= OnSwipe;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void OnSwipe(object sender, SwipeEventArgs args)
    {
        if(args.HitObject != null)
        {
            args.HitObject.GetComponent<Killable>().Die();
        }
    }
}
