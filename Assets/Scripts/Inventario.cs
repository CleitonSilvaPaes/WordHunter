using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour {
    public GameObject inventario;
    public bool[] ehCheio; // VERIFICA SE ESTÁ CHEIO O INVENTARIO
    public GameObject[] slots; // OS IMAGENS QUE VAI FICAR OS ITEMS
    int i;

    private void Start()
    {
        inventario.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventario.SetActive(true);
            i++;
        }
        if (i > 1) {
            inventario.SetActive(false);
            i = 0;
        }
            
    }
}
