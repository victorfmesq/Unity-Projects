using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;

    public Color initialColor = Color.white;
    public Color finalColor;
    public float lifeTime = 3f;

    private float _startingTime;
    private SpriteRenderer _render;

    private void Awake()
    {
        _render = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // save initial time
        _startingTime = Time.time;

        // Destroy the bullet after some time
        Destroy(this.gameObject, lifeTime);
    }

    void Update()
    {
        // Move Object
        Vector2 movement = direction.normalized * speed * Time.deltaTime;
        transform.Translate(movement);

        // Change bullet's color over time

        /**
         * Lerp -> Interpolação linear:
         *      Usa-se quando quer alterar um valor qualquer (Int, Float, Color etc) ao longo de um determinado espaço de tempo
         *      Ex: Component.Lerp(initialValue: , finalValue: , percentagePerTime: )
         *              Neste caso estou usando Lerp para altera a cor do projétil dentro de um determinado espaço de tempo
         *                  calculos: 
         *                           tempoQueFoiCriado = tempoAtual - tempoInicial
         *                           porcentagemCompletada = tempoQueFoiCriado / tempoDeVida 
         */

        float _timeScinceStarted = Time.time - _startingTime;
        float _percentageCompleted = _timeScinceStarted / lifeTime;

        _render.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);
    }
}
