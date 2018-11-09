using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtaque : MonoBehaviour {
    public bool direita;            //Direção que o inimigo anda
    private Transform jogador;       //Quem o inimigo segue
    public float velocidade;        //A velocidade com a qual o ataque segue o inimigo
    public float posicaoY;          //Altura da animação
    public int dano;                //Dano cuasado

    private void Awake()
    {
        var animator = GetComponent<Animator>();
        var duracaoAnimacao = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(this.gameObject, duracaoAnimacao);

        //Definindo a altura
        var posicao = transform.position;
        transform.position = new Vector3(posicao.x, posicaoY, posicao.z);
    }

    // Use this for initialization
    void Start () {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (velocidade > 0)
        {
            //Direção do jogador em relação ao objeto
            var direcaoJogador = jogador.position.x - transform.position.x;

            //Inverte a direção se necessário
            if (direcaoJogador > 0 && !direita)
            {
                direita = true;
                inverter();
            }
            if (direcaoJogador < 0 && direita)
            {
                direita = false;
                inverter();
            }

            //Move objeto
            var direcao = (direita ? Vector3.right : -Vector3.right);
            transform.Translate(direcao * velocidade * Time.deltaTime);
        }
    }

    void inverter()
    {
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerStay2D(Collider2D colisor)
    {
        if (colisor.gameObject.tag.Equals("Player"))
        {
            var personagem = colisor.gameObject.GetComponent<Player>();
            personagem.ReceberDano(Random.Range(0,dano));
        }
    }
}
