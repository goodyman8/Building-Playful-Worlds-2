using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    public Transform shootAt; // Used by turrets to rotate towards player

    // Update is called once per frame
    void Update()
    {
        if (shootAt != null)
        {
            transform.LookAt(shootAt);
        }
    }
}
