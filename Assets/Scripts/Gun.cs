using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    private enum GunType {rock, paper, scissor}

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform barrelLocation;

    private bool isHoldingTrigger;

    private GunType currentGunType = 0;

    public virtual void Fire() 
    {
        Bullet bul = (Bullet)Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);

        //check if attached to player to not add bullet type
        if (transform.tag != "Player")
        {
            bul.initialize(transform.tag);
        }
        else
        {
            switch (currentGunType)
            {
                case GunType.rock:
                    bul.initialize("rock");
                    break;
                case GunType.paper:
                    bul.initialize("paper");
                    break;
                default:
                    bul.initialize("scissor");
                    break;
            }
        }

        //Instantiate bullet (use Pooling)
        //Bullet will move itself
    }

    public void changeGun(SwipeDirection direction)
    {
        if (direction == SwipeDirection.LEFT)
        {
            currentGunType++;
            if (currentGunType > GunType.scissor){currentGunType = GunType.rock;}
        }
        else if (direction == SwipeDirection.RIGHT)
        {
            currentGunType--;
            if (currentGunType < GunType.rock){currentGunType = GunType.scissor;}
        }
    }
}
