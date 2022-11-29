using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform barrelLocation;

    private bool isHoldingTrigger;

    public virtual void Fire() 
    {
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        //Instantiate bullet (use Pooling)
        //Bullet will move itself
    }
}
