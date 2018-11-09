using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed = 3.5f;
    public float pararDePersigir;
    public GameObject[] pontos;
    public float espera = 10f;
    bool loop = true;
    bool seMovendo = true;
    float proxTempo = 0;
    bool direita = false;
    int i = 0;

    Transform target;
    Animator animatorAi;
    public float lookRadius = 6f;
    EnemyHealth enemyHealth;

	// Use this for initialization
	void Start () {
        //Pega a posicao do player
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animatorAi = GetComponent<Animator>();
        enemyHealth = FindObjectOfType(typeof(EnemyHealth)) as EnemyHealth;
        direita = false;
    }

    // Update is called once per frame
    void Update () {
        if(enemyHealth.currentHealth <= 0)
        {
            speed = 0;
            seMovendo = false;
        }
        else
            if(enemyHealth.currentHealth > 0)
            {

                if (Vector2.Distance(target.position, transform.position) <= lookRadius && 
                    Vector2.Distance(target.position, transform.position) > pararDePersigir)
                {
                    if(Vector2.Distance(target.position, transform.position) <= 0.8)
                    {
                        animatorAi.SetBool("isWalk", false);
                        animatorAi.SetBool("isIdle", true);
                        animatorAi.SetBool("isRun", false);
                    }
                    else {
                        animatorAi.SetBool("isWalk", false);
                        animatorAi.SetBool("isIdle", false);
                        animatorAi.SetBool("isRun", true);
                        speed = 6.9f;
                        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                        seMovendo = true;
                        if (target.position.x < transform.position.x)
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
                else if (Vector2.Distance(target.position, transform.position) > lookRadius)
                {
                    if (Time.time >= proxTempo)
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
                        movimenta();
                    }

                }
            }
	}

    private void movimenta()
    {
        if ((pontos.Length != 0) && (seMovendo))
        {
            speed = 3.5f;
            transform.position = Vector2.MoveTowards(transform.position, pontos[i].transform.position,
                speed * Time.deltaTime);
            animatorAi.SetBool("isWalk", true);
            animatorAi.SetBool("isIdle", false);
            if (Mathf.Abs(pontos[i].transform.position.x - transform.position.x) <= 0.001f)
            {
                i++;
                animatorAi.SetBool("isIdle", true);
                animatorAi.SetBool("isRun", false);
                animatorAi.SetBool("isWalk", false);
                proxTempo = Time.time + espera;
                seMovendo = false;
            }

            if (i >= pontos.Length)
            {
                if (loop)
                    i = 0;
                else
                    seMovendo = false;
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
}
