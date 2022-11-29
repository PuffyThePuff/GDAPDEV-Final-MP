using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerShip))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Put Camera at 0,0")]
    [SerializeField] private float sensitivity = 1;

    private Vector2 screenBounds = new Vector2();
    private Vector2 position = new Vector2();
    private float spriteWidth, spriteHeight;

    private Transform m_Transform;
    private void Start()
    {
        CameraHandler c = CameraHandler.instance;
        screenBounds = c.CalculateScreenToWorldView();

        Collider2D collider = GetComponent<Collider2D>();
        spriteWidth = collider.bounds.extents.x;
        spriteHeight = collider.bounds.extents.y;

        m_Transform = transform;
    }

    public void Move(Vector2 direction)
    {
        if (direction.x != 0.0f || direction.y != 0.0f)
        {
            position.x = (direction.x * sensitivity * Time.deltaTime) + m_Transform.position.x;
            position.y = (direction.y * sensitivity * Time.deltaTime) + m_Transform.position.y;

            position.x = Mathf.Clamp(position.x, -screenBounds.x + spriteWidth, screenBounds.x - spriteWidth);
            position.y = Mathf.Clamp(position.y, -screenBounds.y + spriteHeight, screenBounds.y - spriteHeight);

            m_Transform.position = position;
        }
    }
}
