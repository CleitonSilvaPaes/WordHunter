using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    //Tempo de cada Ataque do Personagem
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    //Quantidade de dano o Personagem vai infligir nos Inimigos
    public int damage;
    //Raio de alcance do Personagem
    public float attackRange;
    //Obejeto que se esta no raio e o Personagem atacar vai tirar dano
    public Transform attackPos;
    //O que vai atacar em vez de tag e pela Layer
    public LayerMask whatIsEnemies;
    public LayerMask whatIsBoss;
    //Animação do ataque do personagem
    public Animator playerAnim;

    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            //SE O PLAYER APERTAR O BOTAO DIREITO DO MOUSE VAI ATACAR
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //ANIMACAO DO PLAYER
                playerAnim.SetTrigger("ataquePersonagem");
                //VAI PEGAR TODOS OS INIMIGOS QUE ENTRAR NO RAIO DE ATAQUE DO PLAYER
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                Collider2D[] bossToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsBoss);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    //SE ELE TIVER NO RAIO VAI ATIRAR VIDA DO MOSTRO
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                }
                for (int i = 0; i < bossToDamage.Length; i++)
                {
                    bossToDamage[i].GetComponent<BossHealth>().TakeDamage(damage);
                }
                //TEMPO DE ATAQUE
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
            timeBtwAttack -= Time.deltaTime;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void AddDamage(int addDamage)
    {
        damage += addDamage;
    }
}
