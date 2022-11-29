using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponentInChildren<Gun>();
        InvokeRepeating("Shoot", 1.0f, 2.0f);
    }

    private void Shoot()
    {
        gun.Fire();
    }
}
