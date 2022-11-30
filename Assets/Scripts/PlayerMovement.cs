using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerShip))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Put Camera at 0,0")]
    [SerializeField] private float sensitivity = 1;

    private Vector2 screenBounds = new Vector2();
    private Vector2 position = new Vector2();
    private Vector2 spriteBounds = new Vector2();

    private Transform m_Transform;
    private void Start()
    {
        CameraHandler c = CameraHandler.instance;
        screenBounds = c.CalculateScreenToWorldView();
        //Debug.Log($"PlayerMovement: {screenBounds}");

        Collider2D collider = GetComponent<Collider2D>();
        spriteBounds.x = collider.bounds.extents.x;
        spriteBounds.y = collider.bounds.extents.y;

        m_Transform = transform;
    }

    public void Move(Vector2 direction)
    {
        if (direction.x != 0.0f || direction.y != 0.0f)
        {
            position.x = (direction.x * sensitivity * Time.deltaTime) + m_Transform.position.x;
            position.y = (direction.y * sensitivity * Time.deltaTime) + m_Transform.position.y;

            position.x = Mathf.Clamp(position.x, -screenBounds.x + spriteBounds.x, screenBounds.x - spriteBounds.x);
            position.y = Mathf.Clamp(position.y, -screenBounds.y + spriteBounds.y, screenBounds.y - spriteBounds.y);

            m_Transform.position = position;
        }
    }
}
