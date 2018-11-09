using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    public int z;
	// Use this for initialization
	void Start () {
        transform.Rotate(0, 0, z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
