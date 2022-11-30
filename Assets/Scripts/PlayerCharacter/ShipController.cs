using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Vector2 screenBounds = new Vector2();
    private Vector2 position = new Vector2();
    private Vector2 spriteBounds = new Vector2();
    private Vector2 velocity = new Vector2();

    private Rigidbody2D rb;
    private float dirX;
    private float moveSpeed = 20f;

    private Transform m_transform;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = CameraHandler.instance.CalculateScreenToWorldView();
        m_transform = transform;
    }

    private void Update()
    {
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
}
