using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlePlayer : MonoBehaviour
{
    private Rigidbody2D rBody;

    private bool noChao = true;
   
    public bool seMovendo;
    bool bossInRange;

    float horizontal;
    float vertical;

    public AudioSource andandoAudio;

    public float velocidadePersonagem;
    [Header("Limite de movimento")]
    public float limiteMinimoY;
    public float limiteMaximoY;

    public bool atacando;

    Vector2 velocidade;

    private bool direita = true;

    private Animator animator;

    PlayerHealth playerHealth;

    public float TempoDoAudio;


    // Use this for initialization
    void Start()
    {
       
        animator = gameObject.GetComponent<Animator>();

        rBody = GetComponent<Rigidbody2D>();

        playerHealth = FindObjectOfType(typeof(PlayerHealth)) as PlayerHealth;
        // enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.currentHealth > 0)
        {
            moveJogador();
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }


    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        if(horizontal !=0 && TempoDoAudio <= 0)
        {
            TempoDoAudio = 0.4f;
            andandoAudio.Play();
        }
        if (horizontal > 0 && !direita)
            Vire();
        if (horizontal < 0 && direita)
            Vire();

        velocidade = new Vector2(horizontal * velocidadePersonagem, GetComponent<Rigidbody2D>().velocity.y);

        GetComponent<Rigidbody2D>().velocity = velocidade;

        TempoDoAudio -= Time.deltaTime;
    }

    /*void LateUpdate()
    {
        virarJogador();
        
    }*/

    void moveJogador() //Movimentacao do sprit do presonagem 
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float velocidade = velocidadePersonagem;

        rBody.velocity = new Vector2(horizontal * velocidade, vertical * velocidade);

        if (transform.position.y > limiteMaximoY)
        {
            transform.position = new Vector3(transform.position.x, limiteMaximoY, 0);
        }
        else if (transform.position.y < limiteMinimoY)
        {
            transform.position = new Vector3(transform.position.x, limiteMinimoY, 0);

        }
        animator.SetBool("noChao", noChao);
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("seMovendo", true);
        }
        else
        {
            animator.SetBool("seMovendo", false);

        }

       /* if ((Input.GetButton("Fire1") && !atacando))
        {
            if (tempoDeAtaque <= 0)
            {
                Ataque();
                if (bossInRange)
                    bossHealth.TakeDamage(Random.Range(5, dano));
                tempoDeAtaque = 0.5f;
            }
        }*/

    }

    void Vire()
    {
        direita = !direita;

        Vector2 novoScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

        transform.localScale = novoScale;
    }

    /*void Ataque()
    {
        animator.SetTrigger("ataquePersonagem");
    }*/

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            {
                if (Input.GetButton("Fire1") && !atacando) { }
                if (tempoDeAtaque <= 0)
                {
                    Ataque();
                    var inimigo = other.gameObject.GetComponent<EnemyHealth>();
                    inimigo.TakeDamage(Random.Range(5, dano));
                }


            }
        }
        else if (other.tag.Equals("Boss"))
        {
            bossInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Boss"))
        {
            bossInRange = false;
        }
    }*/
}
