using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Button fireButton;

    PlayerShip playerShip;
    Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        fireButton.onClick.AddListener(Fire);
        playerShip = GetComponent<PlayerShip>();
        joystick = FindObjectOfType<Joystick>();
    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (joystick.Direction.x != 0.0f || joystick.Direction.y != 0.0f)
        {
            PlayerMovement movement = playerShip.movement;
            movement.Move(joystick.Direction);
        }
    }

    public void Fire()
    {
        playerShip.gun.Fire();
    }
}
