using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private Gun gun;
    // Start is called before the first frame update
    void OnEnable()
    {
        gun = GetComponentInChildren<Gun>();
        InvokeRepeating("Shoot", 2.0f, 1.4f);

        //InvokeRepeating("SpecialAttack", 1.5f, 3.0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Shoot()
    {
        gun.Fire();
    }

    private void SpecialAttack()
    {
       // gun.SpecialFire();
    }
}
