using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    //parallaxSpeed deve ser valor entre 0 e 1
    //0 --> sem parallax
    //1 --> cenário imovel em relação a camera (se move jumto com a camera)

    [Range(0f, 1f)]
    public float parallaxSpeed = 0f;

    private Transform cameraTransform;
    private float xAnt; // x da camera no frame anterior
    


	void Start () {
        cameraTransform = Camera.main.transform;//Pega a posição da camera 
        xAnt = cameraTransform.position.x;//Pega a posicão atual no inicio da camera e coloca no xAnt
		
	}
	
	void Update () {
        float deltaCamera = cameraTransform.position.x - xAnt; // Pega a posição atua da camera e menos a posição anterio 

        if(deltaCamera > 0)
        {
            Vector3 newPos = transform.position;// A posição nova do cenario
            newPos.x += parallaxSpeed * deltaCamera;// cenario vai se mover em % a camera 
            transform.position = newPos; // Nova posição no cenario
        }

        xAnt = cameraTransform.position.x;//Nova posição no quadro anterior

    }
}
