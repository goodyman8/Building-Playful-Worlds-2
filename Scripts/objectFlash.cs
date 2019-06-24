using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectFlash : MonoBehaviour
{
    //Red flashing effect

    public bool isFlashing; // Turn on if object is supposed to flash
    public bool flashingIn; // True = flashing in, otherwise flash out

    public int red; //Amount of red
    public int green; //Amount of green
    public int blue; //Amount of blue

    Renderer flashRenderer;

    private void Start()
    {
        flashRenderer = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        flashRenderer.material.color = new Color32((byte)red, (byte)green, (byte)blue, 255);
        if (flashingIn == true)
        {
            if (green <= 30)
            {
                flashingIn = false;
            }
            else
            {
                green -= 10;
                blue -= 10;
            }
        }
        if (flashingIn == false)
        {
            if (green >= 250)
            {
                flashingIn = true;
            }
            else
            {
                green += 10;
                blue += 10;
            }
        }
    }
}
