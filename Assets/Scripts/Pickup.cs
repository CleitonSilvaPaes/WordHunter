using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private Inventario inventario;
    public GameObject itemBotao;

	// Use this for initialization
	void Start () {
        inventario = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
	}

    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            for(int i = 0; i < inventario.slots.Length; i++)
            {
                if(inventario.ehCheio[i] == false)
                {
                    inventario.ehCheio[i] = true;
                    Instantiate(itemBotao, inventario.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }   
    }
}
