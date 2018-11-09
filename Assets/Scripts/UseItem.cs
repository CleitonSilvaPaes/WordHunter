using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour {

    PlayerHealth playerHealth;
    private Transform playerPos;
    PlayerAttack attackPlayer;
    public GameObject arma;
    public int vida;
    public int danoArma;
	// Use this for initialization
	void Start () {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        attackPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

    }
     
    public void Use()
    {
        if (gameObject.CompareTag("Item"))
        {
            if(playerHealth.currentHealth < 100)
            {
                playerHealth.currentHealth += vida;
                if(playerHealth.currentHealth > 100)
                {
                    playerHealth.currentHealth = 100;
                }
                Destroy(gameObject);
            }
        }else if (gameObject.CompareTag("Arma"))
        {
            attackPlayer.AddDamage(danoArma);
            Instantiate(arma, playerPos.position, arma.transform.rotation, playerPos.transform);
            Destroy(gameObject);
            //Destroy(arma);
        }
    }
}
