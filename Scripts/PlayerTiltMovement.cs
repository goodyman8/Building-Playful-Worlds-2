using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTiltMovement : MonoBehaviour
{
    // Simple script that makes the player tilt to the left and right as he moves.
    private float smooth = 15.0f; //How fast is the rotation
    private float tiltAngle = 15.0f; // How far does it tilt 

    // Update is called once per frame
    void Update()
    {
        // Tilts a transform towards the target rotation
        float tiltaroundZ = Input.GetAxis("Horizontal") * -tiltAngle; // Gets the horizontal axis

        // Rotate by converting the angles into a quaternion (unity scriptreference)
        Quaternion target = Quaternion.Euler(0, 0, tiltaroundZ);

        // Move back to the center using slerp (unity script reference)
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
