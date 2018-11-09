using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    public Animator animator;



    protected abstract void Atacar();
    protected abstract void Mover();
    protected abstract void ReceberDano();
}
