using UnityEngine;

public class WeaponSwitching : MonoBehaviour // Tutorial from Brackeys
{
    public int selectedWeapon = 0; // What weapon do we currently have selected
    public int upgrade = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (upgrade == 1) // Changes weapon if you press 1, repeated for other functions
        {
            selectedWeapon = 1;
        }
        if (upgrade == 2 ) // Changes weapon if you press 1, repeated for other functions
        {
            selectedWeapon = 2;
        }
        if (upgrade >= 3)
        {
            selectedWeapon = 3;
        }

        if (previousSelectedWeapon != selectedWeapon) // If previous weapon index isn't equal to the current weapon
        {
            SelectWeapon(); // Run select weapon script, changing the current weapon to match index.
        }
    }


    void SelectWeapon()
    {
        int i = 0; // index numbers
        foreach (Transform weapon in transform) // for each transform in transform (all childs of current transform weaponholder) referred to current one as weapon.
        {
            if (i == selectedWeapon) // If indexes match
                weapon.gameObject.SetActive(true); // Activate game object
            else // Otherwise
                weapon.gameObject.SetActive(false); // Disable game object
            i++; // Adds index numbers to each child
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Upgrade")
        {
            Destroy(collision.gameObject);
            upgrade++;
        }
    }
}
