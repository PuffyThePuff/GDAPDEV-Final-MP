using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Killable
{
	[SerializeField] private TMP_Text PlayerHPText;

    // Update is called once per frame
    void Update()
    {
		PlayerHPText.SetText($"HP: {currentHP}");
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        PlayerHPText.SetText($"HP: {currentHP}");
    }
}
