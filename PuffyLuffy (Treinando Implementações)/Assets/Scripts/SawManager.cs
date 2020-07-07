using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawManager : MonoBehaviour
{
    public static SawManager instance;
    private Animator anim;

    public bool isOn; // está ligada (mata o player) // desligada (não mata)

    public bool isHorizontal; // (true) -> indica o sentido horizontal / (false) -> indica o sentido vertial
   

    public bool dirRight; // (true) -> direção para direita / (false) -> direção para esquerda
    public bool dirUp;    // (true) -> direção para cima / (false) -> direção para baixo

    public float speed; // velocidade com que a serra se locomove
    public float moveTime; // tempo até a serra mudar de direção

    private float timer; // contador de tempo

    void Start()
    {
        instance = GetComponent<SawManager>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (anim.GetBool("On") == true) // animação "On" está com valor (true) por default -> implementar mecanismo para liggar e desligar mais para frente
        {
            this.isOn = true;

            if (this.isHorizontal)
            {

                HorizontalMove();
            }
            else
            {
                VerticalMove();
            }
        }
        else
        {
            this.isOn = false;
        }
    }

    private void TurnOn()
    {
        if (this.isOn == true)
        {
            anim.SetBool("On",true);
        }
    }

    private void TurnOf()
    {
        if (this.isOn == false)
        {
            anim.SetBool("On", false);
        }
    }

    private void HorizontalMove()
    {
        if (dirRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime); // move a serra para direita
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime); // move a serra para esquerda
        }

        timer += Time.deltaTime; // incrementa contador até chegar no valor de moveTIme

        if (timer >= moveTime)
        {
            dirRight = !dirRight; // inverte o booleano e com isso muda a direção da serra
            timer = 0f; // zera o contador;
        }
    }

    private void VerticalMove()
    {
        if (dirUp)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime); // move a serra para cima
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime); // move a serra para baixo
        }

        timer += Time.deltaTime;

        if (timer >= moveTime)
        {
            dirUp = !dirUp;
            timer = 0f;
        }
    }
}
