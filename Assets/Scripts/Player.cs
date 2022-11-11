using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

	[SerializeField] private int PlayerMaxHP = 5;
	[SerializeField] private int PlayerCurrHP;
	[SerializeField] private TMP_Text PlayerHPText;

    // Start is called before the first frame update
    void Start()
    {
		PlayerCurrHP = PlayerMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
		PlayerHPText.SetText($"HP: {PlayerCurrHP}");
    }

	public void updateHpValue(int value)
	{
		PlayerCurrHP += value;
	}
}
