using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Killable
{
    public PlayerMovement movement { get; private set; }
    public Gun gun { get; private set; }

    //public Collider2D m_Collider2D { get; private set; }

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        gun = GetComponentInChildren<Gun>();
        //m_Collider2D = GetComponent<Collider2D>();
    }
}
