using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{


    private Inventario inventory;
    public int index;

    private void Start()
    {

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.ehCheio[index] = false;
        }
    }

    public void Cross()
    {

        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnItem();
            GameObject.Destroy(child.gameObject);
        }
    }

}