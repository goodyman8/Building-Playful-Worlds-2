using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject muzzlePrefab; // Muzzle particles, wanneer je schiet
    public GameObject hitPrefab; // Hit particles, wanneer je iets raakt.
    public Transform muzzle; // muzzle transform, used for the rotation of the muzzle.

    public float bulletTimer;

    void Start()
    {
        if (muzzlePrefab != null) //Als muzzleprefab bestaat
        {
            // Var zijn variables die alleen locaal gebruikt kunnen worden ?
            GameObject muzzleVFX = Instantiate(muzzlePrefab,transform.position, muzzle.rotation); // Spawn muzzle prefab
           // muzzleVFX.transform.forward = gameObject.transform.forward; // Muzzleprefab spawnt forward

            var ProjectileMuzzle = muzzleVFX.GetComponent<ParticleSystem>();

            if (ProjectileMuzzle != null)
            {
                Destroy(muzzleVFX, ProjectileMuzzle.main.duration); // Verwijdert object na de duration van de particle system main settings
            }
            else
            {
                var ProjectileChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>(); //Pakt de particle system van de child als het geen projectilemuzzle kan vinden.
                Destroy(muzzleVFX, ProjectileChild.main.duration);
            }
        }
        Destroy(gameObject, bulletTimer); // Destroy itself
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0]; //Waar is de contact tussen projectile en collider?
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal); //Rotate hit prefab to normal of the collider
        Vector3 position = contact.point; //Position of the contact point.

        if (hitPrefab != null)
        {
            GameObject hitVFX = Instantiate(hitPrefab, position, rotation);
            var ProjectileHit = hitVFX.GetComponent<ParticleSystem>();

            if (ProjectileHit != null)
            {
                Destroy(hitVFX, ProjectileHit.main.duration); // Verwijdert object na de duration van de particle system main settings
            }
            else
            {
                var ProjectileChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>(); //Pakt de particle system van de child als het geen projectilemuzzle kan vinden.
                Destroy(hitVFX, ProjectileHit.main.duration); // Verwijdert object na de duration van de particle system main settings

            }
        }
    }

}
