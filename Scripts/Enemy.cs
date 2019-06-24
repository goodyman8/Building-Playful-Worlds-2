using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    public Rigidbody enemyRb;
    public shootingController bullet;
    public Transform gun;
    public EnemyStates states;
    public float speed;
    public GameObject player;
    public float attackDistance;

    // Start is called before the first frame update
    void Start()
    {
        states = EnemyStates.Moving;
    }
    
    // Update is called once per frame
    void Update()
    {
        //states
        switch (states)
        {
            case EnemyStates.Moving:
                enemyRb.MovePosition(transform.position + 
                    player.gameObject.transform.position.normalized * speed * Time.deltaTime);
                if (Vector3.Distance(this.transform.position, player.transform.position) < attackDistance)
                {
                    states = EnemyStates.Shooting;
                }
                break;
           case EnemyStates.Shooting:
                bullet.Fire(gun);
                break;
            case EnemyStates.Death:
                Destroy(gameObject);
                //deathcode
                break;
        }
    }

    //fire the bullet
    IEnumerator Shoot()
    {
        bullet.Fire(gun);
        yield return new WaitForSeconds(100);
    }

    //got collision of the bullet
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bullet")
        {
            states = EnemyStates.Death;
        }
    }
}
public enum EnemyStates
{
    Moving,
    Shooting,
    Death
}