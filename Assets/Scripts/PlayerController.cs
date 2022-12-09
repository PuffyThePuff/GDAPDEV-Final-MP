using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private Button fireButton;
    private Vector2 screenBounds = new Vector2();
    private Vector2 position = new Vector2();
    private Vector2 spriteBounds = new Vector2();
    private Vector2 velocity = new Vector2();

    private Rigidbody2D rb;
    private float dirX;
    private float moveSpeed = 20f;

    private Transform m_transform;

    PlayerShip playerShip;
    //Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        //fireButton.onClick.AddListener(Fire);
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = CameraHandler.instance.CalculateScreenToWorldView();
        m_transform = transform;

        playerShip = GetComponent<PlayerShip>();
        GestureManager.Instance.OnTap += OnTap;
        //joystick = FindObjectOfType<Joystick>();
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnTap -= OnTap;
    }

#if false
    // Update is called once per frame
    void Update()
    {
        if (joystick.Direction.x != 0.0f || joystick.Direction.y != 0.0f)
        {
            PlayerMovement movement = playerShip.movement;
            movement.Move(joystick.Direction);
        }
    }
#endif

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //Copied ShipController code to here
        dirX = Input.acceleration.x * moveSpeed;
        velocity.x = dirX;
        velocity.y = 0.0f;
        rb.velocity = velocity;

        //clamp position so it doesn't go offscreen
        //TODO: MAKE THIS SCALEABLE OR ADD INVISIBLE BARRIERS TO SCREEN

        position.x = Mathf.Clamp(m_transform.position.x, -screenBounds.x + spriteBounds.x, screenBounds.x - spriteBounds.x);
        position.y = Mathf.Clamp(m_transform.position.y, -screenBounds.y + spriteBounds.y, screenBounds.y - spriteBounds.y);

        m_transform.position = position;
    }

    public void OnTap(object sender, TapEventArgs args)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

		playerShip.gun[0].Fire();
		playerShip.gun[1].Fire();
		playerShip.gun[2].Fire();
    }
}
