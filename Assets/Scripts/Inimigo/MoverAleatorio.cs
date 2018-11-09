using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverAleatorio : MonoBehaviour {
    public float speed = 2;
    private float tempo = 0f;
    public Vector3 direcao;
    private bool direita = false;
    Warlock warlock;
    Transform player;

    public float yMinimo;      //Altura minima que a nave pode ir
    public float yMaximo;      //Altura máxima que a nave pode ir
    public float xMinimo;      //Altura minima que a nave pode ir
    public float xMaximo;      //Altura máxima que a nave pode ir

    public int pararDePersigir;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        warlock = GetComponent<Warlock>();
    }


    void Update () {
        
        if (Vector2.Distance(player.position, transform.position) <= warlock.lookRadius && 
            Vector2.Distance(player.position, transform.position) > pararDePersigir)
        {
            var personagem = GameObject.FindGameObjectWithTag("Player");
            var direcaoPlayer = personagem.transform.position.x - transform.position.x;

            if (direcaoPlayer > 0 && !direita)
            {
                direita = true;
                Flip();
            }
            else if (direcaoPlayer < 0 && direita)
            {
                direita = false;
                Flip();
            }
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (tempo <= 0)
        {
            tempo = 2f;
            direcao.x = Random.Range(-1f, 1f);
            direcao.y = Random.Range(-1f, 1f);
            if (direcao.x > 0 && direita == false)
            {
                direita = true;
                Flip();
            }
            else if(direcao.x < 0 && direita == true)
            {
                direita = false;
                Flip();
            }
        }
        tempo -= Time.deltaTime;

        transform.Translate(direcao.normalized * speed * Time.deltaTime);
        var posicao = transform.position;
        //Maxima e minima altura em Y.
        posicao.y = Mathf.Clamp(posicao.y, yMinimo, yMaximo);

        //Maxima e minima posicao em X.
        posicao.x = Mathf.Clamp(posicao.x, xMinimo, xMaximo);

        transform.position = posicao; //Define a posição com a altura ajustada
    }

    void Flip()
    {
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
