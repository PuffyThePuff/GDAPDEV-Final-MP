using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Enemy2 : Killable
{
    [SerializeField] private SpreadOrPinch gestureWeakness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public SpreadOrPinch GestureWeakness
    {
        get { return gestureWeakness; }
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
