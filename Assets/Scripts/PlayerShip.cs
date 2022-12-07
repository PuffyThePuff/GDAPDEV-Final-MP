using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;

public class PlayerShip : Killable
{
    [Header("Invicibility")]
    [SerializeField] private float invicibilityDuration;
    private bool isInvicible;
    private WaitForSeconds waitTime;

    [Header("UI")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Animator screenAnimator;

    public PlayerMovement movement { get; private set; }
    public Gun gun { get; private set; }
    private readonly static int _Damage = Animator.StringToHash("Damage");

    //public Collider2D m_Collider2D { get; private set; }

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        gun = GetComponentInChildren<Gun>();
        waitTime = new WaitForSeconds(invicibilityDuration);
        //m_Collider2D = GetComponent<Collider2D>();
    }

    public override void initialize()
    {
        base.initialize();
        healthText.SetText($"HP: {currentHP}");
    }

    public override void Damage(int damage)
    {
        if (isInvicible) return;

        base.Damage(damage);
        healthText.text = currentHP.ToString();
        screenAnimator.Play(_Damage);

        if (!isInvicible)
        {
            StartCoroutine(Invicibility());
        }
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
        GameOverManager.Instance.OnGameOver(GameOverState.lose);
    }

    IEnumerator Invicibility()
    {
        if (isInvicible) StopCoroutine(Invicibility());

        isInvicible = true;

        yield return waitTime;

        isInvicible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            //Debug.Log($"{tag} || {bullet.tagOfOrigin}");
            if (gameObject.tag != bullet.tagOfOrigin)
            {
                Damage(bullet.Damage);
                Destroy(bullet.gameObject);
            }
        }
    }
}
