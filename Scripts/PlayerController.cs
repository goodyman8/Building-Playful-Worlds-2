using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb; // Enter the player rigidbody
    public float speed; // Speed, how fast do we go
    public float health;

    void Update()
    {
        movement();
    }

    private void movement() //Move
    {
        float horizontal = Input.GetAxis("Horizontal"); // Gets the horizontal axis
        float vertical = Input.GetAxis("Vertical"); // Gets the vertical axis

        playerRb.MovePosition(transform.localPosition + (transform.forward * vertical + transform.right * horizontal).normalized * speed * Time.fixedDeltaTime);
    }
}
