using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{

	[SerializeField] GameObject Gun1;
	[SerializeField] GameObject Gun2;
    // Start is called before the first frame update
    void Start()
    {
		if (PlayerDataManager.instance.gunUpgradeLevel == 2) Gun1.SetActive(true);
		if (PlayerDataManager.instance.gunUpgradeLevel == 3) Gun1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
