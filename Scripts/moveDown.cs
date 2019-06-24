using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDown : MonoBehaviour
{
    // Moves down like the regular shipController, but shipcontroller isnt used because most of its code isnt necessary

    // Update is called once per frame
    void Update()
    {
        var UpgradeRb = gameObject.GetComponent<Rigidbody>();
        UpgradeRb.MovePosition(transform.localPosition += Vector3.back * 5 * Time.deltaTime);
    }
}
