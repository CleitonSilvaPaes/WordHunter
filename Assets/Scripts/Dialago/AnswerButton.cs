using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButton : MonoBehaviour {

    Respostas respostaData;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ProximaFala()
    {
        FindObjectOfType<DialogoController>().ProximaFala(respostaData.proximaFala);
    }
    public void Setup(Respostas resposta)
    {
        respostaData = resposta;
    }
}
