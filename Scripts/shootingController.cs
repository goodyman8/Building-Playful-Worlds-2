using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingController : MonoBehaviour
{
    public float bulletForce; // Force of the bullet
    public float bulletRate; // Rate of fire
    private float bulletRateTimeStamp; // Used when resetting the shooting timer

    // Turret rotation moved to turret model in order to make it turn properly.
    //public bool Turret; // Enabled if the gun is a turret and auto locks on players
    public bool enemyGun; // Enabled if the gun is used by an enemy & not the player
    public float attackDistance; // Gun will be active when the distance is low enough (used for enemies)

    public GameObject bullet; // Which object to spawn
    public GameObject player; // Used to calculate distance between player & shooting controller. Used for enemies

    public Transform PlayerPos; // Get player rotation, used to aim the gun at the player
    public Transform gun; // Takes the position of the gun, public so it can be called from other scripts (used in the old enemy state machine)
    public GunStates states; // State enum, used in the switch cases

    // Update is called once per frame. WAAR WAS IK MEE BEZIG? GUN CONTROLLER STATE MACHINE: WANNEER KAN IK SCHIETEN. WANNEER SCHIET ENEMY AUTOMATISCH. WANNEER IS GUN ACTIVE OF NIET!
    void Update()
    {
        switch (states)
        {
            case GunStates.Fire: // Fires
                    if (enemyGun == false) // If the gun isnt from an enemy (player gun)
                    { 
                        if (Input.GetKey("space") && Time.time > bulletRateTimeStamp) // If you press space & shooting is available
                        {
                            Fire(gun);
                        }
                    }
                    else //If its an enemy gun, shoot automatically over time.
                    {
                    if (Time.time > bulletRateTimeStamp && Vector3.Distance(this.transform.position, player.transform.position) < attackDistance) 
                        {
                        /*if (Turret == true) // Turret test, moved to turret model to make it rotate
                        {
                            transform.LookAt(PlayerPos);
                            Fire(gun);
                        }
                        */
                            Fire(gun);
                        }
                    }
            break;
        }
    }
    

    public void Fire(Transform gun) 
    {
        GameObject shot = Instantiate(bullet,gun.position,gun.rotation); // Instantiate bullet game object
        shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * bulletForce); // Shoot it forward

        bulletRateTimeStamp = Time.time + bulletRate; //Reset timer
    }



    public enum GunStates
    {
        Fire,
        //FireEnemy, Supposed to be for the enemy firing, but is unused.
    }
}
