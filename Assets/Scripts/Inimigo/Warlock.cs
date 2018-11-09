using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : MonoBehaviour {
    //Ataque 1
    public GameObject ataque1;
    public float intervaloAtaque1;
    private float contagemAtaque1;

    //Ataque 2
    public GameObject ataque2;
    public float intervaloAtaque2;
    private float contagemAtaque2;

    //Ataque 3
    public GameObject ataque3;
    public float intervaloAtaque3;
    private float contagemAtaque3;

    public float lookRadius;

    private Transform jogador;       //Quem o inimigo segue

    // Use this for initialization
    void Start () {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        //var direcaoJogador = jogador.position.x - transform.position.x;
        contagemAtaque1 -= Time.deltaTime;
        contagemAtaque2 -= Time.deltaTime;
        contagemAtaque3 -= Time.deltaTime;
        if (Vector2.Distance(jogador.position, transform.position) <= lookRadius)
        {
            Ataque();
        }
    }
    void Ataque()
    {
        if (contagemAtaque1 <= 0)
        {
            var personagem = GameObject.FindGameObjectWithTag("Player").transform;
            Instantiate(ataque1, personagem.position, Quaternion.identity);
            contagemAtaque1 = intervaloAtaque1;
        }else if (contagemAtaque2 <= 0)
        {
            var personagem = GameObject.FindGameObjectWithTag("Player").transform;
            Instantiate(ataque2, personagem.position, Quaternion.identity);
            contagemAtaque2 = intervaloAtaque2;
        }
        else if (contagemAtaque3 <= 0)
        {
            var personagem = GameObject.FindGameObjectWithTag("Player").transform;
            Instantiate(ataque3, personagem.position, Quaternion.identity);
            contagemAtaque3 = intervaloAtaque3;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
