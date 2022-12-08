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

    private void Start()
    {
        
    }

    public virtual void Fire() 
    {
        Bullet bul = (Bullet)Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);

        //check if attached to player to not add bullet type
        if (transform.tag != "Player")
        {
            bul.initialize("Enemy");
        }
        else
        {
            switch (currentGunType)
            {
                case GunType.rock:
                    bul.initialize("rock");
                    bul.GetComponent<SpriteRenderer>().color = Color.gray;
                    bul.GetComponent<TrailRenderer>().startColor = Color.gray;
                    this.gameObject.GetComponentInParent<SpriteRenderer>().color = Color.gray;
                    break;
                case GunType.paper:
                    bul.initialize("paper");
                    bul.GetComponent<SpriteRenderer>().color = Color.white;
                    bul.GetComponent<TrailRenderer>().startColor = Color.white;
                    this.gameObject.GetComponentInParent<SpriteRenderer>().color = Color.white;
                    break;
                default:
                    bul.initialize("scissor");
                    bul.GetComponent<SpriteRenderer>().color = Color.red;
                    bul.GetComponent<TrailRenderer>().startColor = Color.red;
                    this.gameObject.GetComponentInParent<SpriteRenderer>().color = Color.red;
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
        Debug.Log("switch to: " + currentGunType);
    }
}
