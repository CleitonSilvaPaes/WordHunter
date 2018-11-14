using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour {

    //private Transform playerPos;
    Player player;
    public GameObject arma;
    public int vida;
    public int danoArma;
	// Use this for initialization
	void Start () {
        //playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }
     
    public void Use()
    {
        if (gameObject.CompareTag("Item"))
        {
            if(player.currentHealth < 100)
            {
                player.currentHealth += vida;
                if(player.currentHealth > 100)
                {
                    player.currentHealth = 100;
                }
                Destroy(gameObject);
            }
        }else if (gameObject.CompareTag("Arma"))
        {
            player.AdicionarDano(danoArma);
            //Instantiate(arma, playerPos.position, arma.transform.rotation, playerPos.transform);
            Destroy(gameObject);
            //Destroy(arma);
        }
    }
}
