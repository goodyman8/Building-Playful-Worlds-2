using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ShipController : MonoBehaviour
{   
    // Originally only used for the enemies, which is why some names still have Enemy in them.
    // Finite state machine, used as a base for enemies (also for the player now). Guns are attached as objects instead of handled by the state machine.
    public bool isEnemy = true; // Used if the ship is an enemy ship
    public bool follow; // Used on certain enemies, true if it follows player

    public float speed; // How fast is the ship
    public float health; // Hitpoints of the ship

    Renderer enemyRenderer; // Renderer, used for the flash

    public GameObject gameManagerHolder; //  Load the game object with the scenemanager in it, then take that component.
    public GameManager scenes; // Load the scenemanager, used only for the player game over

    public GameObject player; // Player game object, used when chasing
    public GameObject DeathMuzzleShot; // Death muzzle for enemies shot to death

    public Rigidbody enemyRb; // Enemy rigidbody

    public EnemyStates enState; // Originally = enemy states (Also used for the player)

    public float timer; // Timer, used for both the flash & immunity
    private float timerStamp; // Resets timer

    // Start is called before the first frame update
    void Start()
    {
        // Get the scene manager
        if (isEnemy == false)
        {
            gameManagerHolder = GameObject.Find("GameManagerObject"); // Find the game manager object if the ship spawns in the level, game manager is a singleton so there will always be one
            scenes = gameManagerHolder.GetComponent<GameManager>(); // Get the Game Manager script from it (bad naming I know)
            GameManager.Score = 0; // Reset score on ship spawn, used when restarting the level.
        }

        enemyRenderer = GetComponent<Renderer>(); // Take the renderer
        timerStamp = timer; // Remember starting timer

        if (isEnemy == true)
        {
            if (follow == true)
            {
                enState = EnemyStates.Follow; // Start following if the bool is true
            }
            else
            {
                enState = EnemyStates.Move; // Otherwise use move case
            }
        }
        else
        {
            enState = EnemyStates.PlayerMove; // Player movement
        }
     }

    // Update is called once per frame
    void Update()
    {
        timer -= 1 * Time.deltaTime; // Remove time from the Immunity timer

        // States
        switch (enState)
        {
            case EnemyStates.PlayerMove: // Playermovement
                float horizontal = Input.GetAxis("Horizontal"); // Gets the horizontal axis
                float vertical = Input.GetAxis("Vertical"); // Gets the vertical axis

                enemyRb.MovePosition(transform.localPosition + (transform.forward * vertical + transform.right * horizontal).normalized * speed * Time.fixedDeltaTime);
                break;

            case EnemyStates.Move: // Move down
                enemyRb.MovePosition(transform.localPosition += Vector3.back * speed *Time.deltaTime);
                break;

            case EnemyStates.Follow: //Follow player
                enemyRb.MovePosition(transform.localPosition += player.gameObject.transform.position.normalized * speed * Time.deltaTime);
                break;

            case EnemyStates.Damage: // Checks if health < 0 & flashes the enemy
                health -= 1; // Removes target health
                StartCoroutine(enemyFlash());
                if (health <= 0 && isEnemy == true) // Checks for health, if below 0 enter death state
                {
                    enState = EnemyStates.Death; // Death for the enemy ships
                }
                else if (health <= 0 && isEnemy == false)
                {
                    enState = EnemyStates.GameOver; // Death for the player
                }
                else if (follow == true) //If not below 0 and follow true = return to following
                {
                    enState = EnemyStates.Follow;
                }
                else if (follow != true && isEnemy == true) //If not dead & not following: move.
                {
                    enState = EnemyStates.Move;
                }
                else 
                {
                    enState = EnemyStates.PlayerMove; // Move the player
                }
                break;

            case EnemyStates.Death: // DIE :(
                Destroy(gameObject);
                break;

            case EnemyStates.GameOver: // Game ogre
                scenes.LoseScreen(); // Back to start menu on death
                break;

            case EnemyStates.Win: //Win game, move to endscreen
                scenes.EndScreen();
                break;
        }
    }


    public void OnCollisionEnter(Collision collision) //Collision, used to check for hits against bullets
    {
        if (collision.gameObject.tag == "PlayerBullet" && isEnemy == true) // Hit by bullet && timer below 0?
        {
            ContactPoint contact = collision.contacts[0]; //
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal); //Rotate hit prefab to normal of the collider
            Vector3 position = contact.point; //Position of the contact point.

            Destroy(collision.gameObject); // Destroy bullet
            if (timer <= 0)
            {
                GameManager.Score += 10; // Add 10 score on every hit
                enState = EnemyStates.Damage; // Enter damage state
                timer = timerStamp; // Groundhog day the timer

                if (health == 1 && DeathMuzzleShot != null) // Same particle code as in the projectile controller :(
                {
                    GameObject DeathExplosion = Instantiate(DeathMuzzleShot, position, rotation);
                    var DeathParticles = DeathExplosion.GetComponent<ParticleSystem>();

                    if (DeathMuzzleShot != null)
                    {
                        Destroy(DeathExplosion, DeathParticles.main.duration); // Destroy object after the particle lifetime (just destroying the particle seems to keep the object alive)
                    }
                    else
                    {
                        var ExplosionChild = DeathExplosion.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(DeathExplosion, DeathParticles.main.duration);
                    }

                }
            }
        }
        else if (collision.gameObject.tag == "EnemyBullet" && isEnemy == false) // Hit by bullet && timer below 0?
        {
            Destroy(collision.gameObject); // Destroy bullet
            if (timer <= 0)
            {
                enState = EnemyStates.Damage; // Enter damage state
                timer = timerStamp; // Groundhog day the timer
            }
        }
        if (collision.gameObject.tag == "Enemy" && isEnemy == false)
        {
            if (timer <= 0)
            {
                enState = EnemyStates.Damage;
                timer = timerStamp;
            }
        }
        if (collision.gameObject.name == "Win" && isEnemy == false)
        {
            enState = EnemyStates.Win; // Win if the player collides with the Win object
        }
        if (collision.gameObject.name == "EnemyDestroyer" && isEnemy == true)
        {
            Destroy(gameObject); //Destroy enemy on collision, used for enemies who go offscreen.
       }
    }

    IEnumerator enemyFlash() // Flashes the enemy
    {
        Material enemyMat = GetComponent<Renderer>().material; // Old material
        Color32 enemyColor = GetComponent<Renderer>().material.color; // Old color

        enemyRenderer.material = null;
        enemyRenderer.material.color = Color.white;

        yield return new WaitForSeconds(0.1f);

        enemyRenderer.material = enemyMat; 
        enemyRenderer.material.color = enemyColor;
    }

    public enum EnemyStates
    {
        PlayerMove,
        Follow,
        Move,
        Damage,
        Death,
        GameOver,
        Win,
    }
}
