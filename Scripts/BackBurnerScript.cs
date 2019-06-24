using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBurnerScript : MonoBehaviour
{
    public ParticleSystem backburner; // Backburner particles

    // Start is called before the first frame update
    void Awake()
    {
        backburner = gameObject.GetComponent<ParticleSystem>(); // Get particle system of current object
        backburner.Play(); //Activate it
    }
}
