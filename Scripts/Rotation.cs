using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Transform rotateAround; //What does it rotate around

    public float orbitDistance = 10.0f;
    public float orbitDegreesPerSec = 180.0f;

    // Update is called once per frame
    private void LateUpdate()
    {
        //  transform.RotateAround(rotateAround.transform.position, axis, speed);
        Orbit();
    }

    private void Orbit()
    {
        transform.position = rotateAround.position + (transform.position - rotateAround.position).normalized * orbitDistance;
        transform.RotateAround(rotateAround.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
    }
}
