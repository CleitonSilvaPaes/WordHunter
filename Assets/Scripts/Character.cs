using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour {
    public Animator animator;
    protected Rigidbody2D rBody;
    [Header("Vida")]
    public int startingHealth;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public bool isDead;
    [Header("Ataque")]
    protected float timeBtwAttack;
    public float startTimeBtwAttack;
    public int dano;
    protected bool damaged;
    [Header("Velocidade")]
    protected Vector2 velocidade;
    [Header("Limite de movimento")]
    public float limiteMinimoY;
    public float limiteMaximoY;
    protected bool direita = true;

    protected abstract void Atacar();
    protected abstract void Mover();
    public abstract void ReceberDano(int ReceberDano);
    public abstract void AdicionarDano(int AdicionarDano);
    protected abstract void Morte();
}
