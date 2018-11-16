using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Character
{
    public AudioSource _AudioAtaque;
    //Raio de alcance do Personagem
    public float attackRange;
    //Obejeto que se esta no raio e o Personagem atacar vai tirar dano
    public Transform attackPos;
    //O que vai atacar em vez de tag e pela Layer
    public LayerMask whatIsEnemies;
    public LayerMask whatIsBoss;

    float tempoAudioMovimento;
    public float velocidadePersonagem;

    private void Start()
    {
        currentHealth = startingHealth;
        rBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
       var horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0 && !direita)
            Vire();
        if (horizontal < 0 && direita)
            Vire();

        velocidade = new Vector2(horizontal * velocidadePersonagem, GetComponent<Rigidbody2D>().velocity.y);

        GetComponent<Rigidbody2D>().velocity = velocidade;
    }

    private void Update()
    {
        tempoAudioMovimento -= Time.deltaTime;      
        if (currentHealth > 0)
        {
            Mover();
            if (timeBtwAttack <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    Atacar();
            }
            else
                timeBtwAttack -= Time.deltaTime;

            healthSlider.value = currentHealth;
            if (damaged)
                damageImage.color = flashColour;
            else
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            damaged = false;
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    protected override void Atacar()
    {
        //AUDIO DE ATAQUE
        _AudioAtaque.Play();
          
        _animacaoPersonagens.SetTrigger("ataquePersonagem");
        //VAI PEGAR TODOS OS INIMIGOS QUE ENTRAR NO RAIO DE ATAQUE DO PLAYER
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        Collider2D[] bossToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsBoss);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            //SE ELE TIVER NO RAIO VAI ATIRAR VIDA DO MOSTRO
            enemiesToDamage[i].GetComponent<Enemy>().ReceberDano(dano);
        }
        for (int i = 0; i < bossToDamage.Length; i++)
        {
            bossToDamage[i].GetComponent<BossHealth>().TakeDamage(dano);
        }
        //TEMPO DE ATAQUE
        timeBtwAttack = startTimeBtwAttack;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    protected override void Mover()
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
        _animacaoPersonagens.SetBool("noChao", true
);
        if (horizontal != 0 || vertical != 0)
        {
            _animacaoPersonagens.SetBool("seMovendo", true);
            
            if(tempoAudioMovimento <= 0)
            {
                audio.Play();
                tempoAudioMovimento = 0.25f;
            }
        }
        else
        {
            _animacaoPersonagens.SetBool("seMovendo", false);

        }
    }

    void Vire()
    {
        direita = !direita;

        Vector2 novoScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

        transform.localScale = novoScale;
    }

    public override void AdicionarDano(int AdicionarDano)
    {
        dano += AdicionarDano;
    }

    public override void ReceberDano(int ReceberDano)
    {
        damaged = true;
        currentHealth -= ReceberDano;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0 && !isDead)
        {
            Morte();
        }
    }

    protected override void Morte()
    {
        isDead = true;
        return;
    }
}
