using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public GameObject[] items;
    private Transform player;
    [Space]
    public int startingHealth;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
    //public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    //public AudioClip deathClip;                 // The sound to play when the enemy dies.


    Animator anim;                              // Reference to the animator.
    //AudioSource enemyAudio;                     // Reference to the audio source.
    //public ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
    CapsuleCollider2D capsuleCollider;            // Reference to the capsule collider.
    bool isDead;                                // Whether the enemy is dead.
    bool isSinking;                             // Whether the enemy has started sinking through the floor.
    //public GameObject bloodeEffect;
    public Slider healthSlider;

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        // enemyAudio = GetComponent<AudioSource>();
        //hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;

    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        healthSlider.value = currentHealth;
        // If the enemy should be sinking...
        if (isSinking)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount)
    {
        healthSlider.value = currentHealth;
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

       /* if(bloodeEffect != null)
        {
            Instantiate(bloodeEffect, transform.position, Quaternion.identity);
        }
        */

        // Play the hurt sound effect.
        //enemyAudio.Play();

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // Set the position of the particle system to where the hit was sustained.

        // And play the particles.
        //hitParticles.Play();

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }


    void Death()
    {
        // The enemy is dead.
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it.
        capsuleCollider.isTrigger = true;

        if (items.Length > 0)
        {
            GameObject item = items[Random.Range(0, items.Length)];
            item.GetComponent<SpriteRenderer>().sortingOrder = 12;
            Vector2 playerPos = new Vector2(player.position.x + 2, player.position.y);
            Instantiate(item.transform, playerPos, Quaternion.identity);
        }

        // Tell the animator that the enemy is dead.
        anim.SetTrigger("Dead");
        Destroy(gameObject, 2f);
        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        //enemyAudio.clip = deathClip;
        //enemyAudio.Play();
    }


    public void StartSinking()
    {
        // Find and disable the Nav Mesh Agent.
        //GetComponent<NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody2D>().isKinematic = true;

        // The enemy should no sink.
        isSinking = true;

        // Increase the score by the enemy's score value.
        //ScoreManager.score += scoreValue;

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }
}