using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShip : Killable
{
    public PlayerMovement movement { get; private set; }
    public Gun gun { get; private set; }

    [Header("UI")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Animator screenAnimator;

    private readonly static int _Damage = Animator.StringToHash("Damage");

    //public Collider2D m_Collider2D { get; private set; }

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        gun = GetComponentInChildren<Gun>();
        //m_Collider2D = GetComponent<Collider2D>();
    }

    public override void initialize()
    {
        base.initialize();
        healthText.SetText($"HP: {currentHP}");
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        healthText.text = currentHP.ToString();
        screenAnimator.Play(_Damage);
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
        GameOverManager.Instance.OnGameOver(GameOverState.lose);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            //Debug.Log($"{tag} || {bullet.tagOfOrigin}");
            if (!gameObject.CompareTag(bullet.tagOfOrigin))
            {
                Damage(bullet.Damage);
                Destroy(bullet.gameObject);
            }
        }
    }
}
