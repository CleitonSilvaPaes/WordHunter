using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public GameObject[] obj;
   

	// Use this for initialization
	void Start () {
        spawn();
	}
	
	// Update is called once per frame
	void spawn () {
        Instantiate(obj[Random.Range(0,obj.Length)],transform.position,Quaternion.identity);
	}
}
