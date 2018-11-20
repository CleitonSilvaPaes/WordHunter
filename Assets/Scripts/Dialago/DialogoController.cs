using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogoController : MonoBehaviour {

    public GameObject painelDeDialogo;

    public Text falaNPC;

    public GameObject resposta;

    private bool falaAtiva = false;

    FalaNPC falas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0) && falaAtiva)
        {
            if(falas.resposta.Length > 0)
            {
                MostrarRespostas();
            }
            else
            {
                falaAtiva = false;
                painelDeDialogo.SetActive(false);
                falaNPC.gameObject.SetActive(false);
                FindObjectOfType<Player>().velocidadePersonagem = 10;
            }
        }
	}

    void MostrarRespostas()
    {
        Time.timeScale = 0;
        falaNPC.gameObject.SetActive(false);
        falaAtiva = false;

        for(int i = 0;i < falas.resposta.Length; i++)
        {
            GameObject tempResposta = Instantiate(resposta, painelDeDialogo.transform) as GameObject;
            tempResposta.GetComponent<Text>().text = falas.resposta[i].resposta;
            tempResposta.GetComponent<AnswerButton>().Setup(falas.resposta[i]);
        }
    }

    public void ProximaFala(FalaNPC fala)
    {
        Time.timeScale = 1;
        falas = fala;

        LimparRespostas();

        falaAtiva = true;
        painelDeDialogo.SetActive(true);
        falaNPC.gameObject.SetActive(true);

        falaNPC.text = falas.fala;
    }
    void LimparRespostas()
    {

        AnswerButton[] buttons = FindObjectsOfType<AnswerButton>();
        foreach(AnswerButton button in buttons)
        {
            Destroy(button.gameObject);
        }

    }
}
