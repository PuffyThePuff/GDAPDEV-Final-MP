using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    private enum GunType {rock, paper, scissor}

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform barrelLocation;
	[SerializeField] private float gunCooldown; 
	[SerializeField] private float coolDownTimer;

	[SerializeField] private float rapidFireTimer = 5.0f;
	[SerializeField] private float rapidFireCount = 0.0f;

	public bool rapidFire = false;

    private bool isHoldingTrigger;

    private GunType currentGunType = 0;

    private void Start()
    {
		switch (PlayerDataManager.instance.fireRateUpgradeLevel)
		{

			case 1: gunCooldown = 1.0f;
				break;

			case 2: gunCooldown = 0.7f;
				break;

			case 3: gunCooldown = 0.5f;
				break;

			case 4: gunCooldown = 0.3f;
				break;

			case 5: gunCooldown = 0.2f;
				break;

			case 6: gunCooldown = 0.0f;
				break;

		}
    }

	private void Update()
	{
		if(coolDownTimer > 0)
		{
				coolDownTimer -= Time.deltaTime;
		}
		if (rapidFire) {
			if (rapidFireCount < rapidFireTimer)
			{
				rapidFireCount += Time.deltaTime;
			}
			else rapidFire = false;
		}
	}

	public virtual void Fire() 
    {
		if(coolDownTimer <= 0 || rapidFire)
		{

        Bullet bul = (Bullet)Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);

        //check if attached to player to not add bullet type
        if (transform.tag != "Player")
        {
            bul.initialize("Enemy");
        }
        else
        {
			bul.updateDamage(PlayerDataManager.instance.damageUpgradeLevel);
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

				if(!rapidFire) coolDownTimer = gunCooldown;

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
