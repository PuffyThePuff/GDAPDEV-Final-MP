using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Rigidbody rb;
    private float dirX;
    private float moveSpeed = 20f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        dirX = Input.acceleration.x * moveSpeed;
        rb.velocity = new Vector3(dirX, 0, 0);

        //clamp position so it doesn't go offscreen
        //TODO: MAKE THIS SCALEABLE OR ADD INVISIBLE BARRIERS TO SCREEN
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y, transform.position.z);
    }
}
