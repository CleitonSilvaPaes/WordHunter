using UnityEngine;

public class Enemy : Character
{
    [Header("Items que vai Jogar para Personagen")]
    public GameObject[] items;
    [Header("Velocidade")]
    public float speed = 3.5f;
    [Header("Distancia que Fica parado perto do Player")]
    public float pararDePersigir;
    [Header("Postos do Mapa que o Inimigo vai ficar")]
    public GameObject[] pontos;
    [Header("RAIO DE ALCANCE DO INIMIGO")]
    public float lookRadius = 6f;
    bool seMovendo = true;
    int i = 0;

    GameObject _personagen; 
    bool playerInRange;
    CapsuleCollider2D capsuleCollider;
    Player player; // Referencia a vida do Personagem
    float timer;
    bool itemEntreque = false;

    void Awake()
    {
        _personagen = GameObject.FindGameObjectWithTag("Player");
        player = _personagen.GetComponent<Player>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        currentHealth = startingHealth;
    }

    private void Start()
    {
        direita = false;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            speed = 0;
            seMovendo = false;
        }
        else if(currentHealth > 0)
        {
            timer += Time.deltaTime;
            Mover();
            if (timer >= timeBtwAttack && playerInRange)
            {
                Atacar();
                player.ReceberDano(Random.Range(0, dano));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _personagen)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == _personagen)
        {
            // ... the player is in range.
            playerInRange = false;
        }
    }

    public override void ReceberDano(int ReceberDano) // Quanto de dano que vai levar dos ATAQUES DO PERSONAGEM
    {
        damaged = true;
        currentHealth -= ReceberDano;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            Morte();
        }
    }

    protected override void Atacar()
    {
        timer = -2f;

        if (player.currentHealth > 0)
        {
            audio.Play();
            _animacaoPersonagens.SetTrigger("ataque");
        }
    }

    protected override void Morte()
    {
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it.
        capsuleCollider.isTrigger = true;

        if (items.Length > 0)
        {
           if(itemEntreque == false)
            {
                GameObject item = items[UnityEngine.Random.Range(0, items.Length)];
                item.GetComponent<SpriteRenderer>().sortingOrder = 12;
                Vector2 playerPos = new Vector2(_personagen.transform.position.x + 2, _personagen.transform.position.y);
                Instantiate(item.transform, playerPos, Quaternion.identity);
                itemEntreque = true;
            }
        }

        // Tell the animator that the enemy is dead.
        _animacaoPersonagens.SetTrigger("Dead");
        Destroy(gameObject, 2f);
    }

    protected override void Mover()
    {
        if (Vector2.Distance(_personagen.transform.position, transform.position) <= lookRadius &&
                     Vector2.Distance(_personagen.transform.position, transform.position) > pararDePersigir)
        {
            var distancia = _personagen.transform.position.x - transform.position.x;

            if (Mathf.Abs(distancia) <= 0.9)
            {
                _animacaoPersonagens.SetBool("isWalk", false);
                _animacaoPersonagens.SetBool("isIdle", true);
                _animacaoPersonagens.SetBool("isRun", false);
            }
            else
            {
                _animacaoPersonagens.SetBool("isWalk", false);
                _animacaoPersonagens.SetBool("isIdle", false);
                _animacaoPersonagens.SetBool("isRun", true);
                speed = 6.9f;
                transform.position = Vector2.MoveTowards(transform.position, _personagen.transform.position, speed * Time.deltaTime);
                seMovendo = true;
                if (_personagen.transform.position.x < transform.position.x)
                {
                    if (direita == true)
                    {
                        direita = false;
                        Flip();
                    }
                }
                else
                {
                    if (direita == false)
                    {
                        direita = true;
                        Flip();
                    }
                }
            }

        }
        else if (Vector2.Distance(_personagen.transform.position, transform.position) > lookRadius)
        {
            if (Time.time >= 0)
            {
                if (transform.position.x <= pontos[0].transform.position.x && direita == false)
                {
                    direita = true;
                    Flip();
                }
                else if (transform.position.x >= pontos[1].transform.position.x && direita == true)
                {
                    direita = false;
                    Flip();
                }
                seMovendo = true;
                if ((pontos.Length != 0) && (seMovendo))
                {
                    speed = 3.5f;
                    transform.position = Vector2.MoveTowards(transform.position, pontos[i].transform.position,
                        speed * Time.deltaTime);
                    _animacaoPersonagens.SetBool("isWalk", true);
                    _animacaoPersonagens.SetBool("isIdle", false);
                    if (Mathf.Abs(pontos[i].transform.position.x - transform.position.x) <= 0.001f)
                    {
                        i++;
                        _animacaoPersonagens.SetBool("isIdle", true);
                        _animacaoPersonagens.SetBool("isRun", false);
                        _animacaoPersonagens.SetBool("isWalk", false);
                        seMovendo = false;
                    }

                    if (i >= pontos.Length)
                    {
                        if (true)
                            i = 0;
                    }
                }
            }

        }
    }

    void Flip()
    {
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public override void AdicionarDano(int AdicionarDano) // Não Implementa
    {
    }

}
