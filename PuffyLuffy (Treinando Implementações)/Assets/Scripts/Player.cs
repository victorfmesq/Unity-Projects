using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private bool isJumping;
    private bool doubleJump;

    private Animator anim;
    private Rigidbody2D rig; // possui metodos como para adicionar uma força q arremesa o objeto para alguma direção

    private bool isBlowing;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) 
        {
            isJumping = false;
            doubleJump = false;
            anim.SetBool("Jump", false);
        }
        else if (collision.gameObject.tag == "Spike")
        {
            Die();
        }
        else if (collision.gameObject.tag == "Saw")
        {
            if (SawManager.instance.isOn == true)
            {
                Die();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isBlowing == false) // para alterar os botões default de Input, vá na engine e: Edit -> Project Settings -> Input -> Axes -> Jump
        {
            if (isJumping == false)
            {
                rig.AddForce(new Vector2(0f, (jumpForce)), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("Jump", true);
            }
            else if (doubleJump == true)
            {
                rig.AddForce(new Vector2(0f, (jumpForce / 2)), ForceMode2D.Impulse);
                doubleJump = false;
            }
        }

    }

    private void Move() // para alterar os botões default de Input, vá na engine e: Edit -> Project Settings -> Input -> Axes -> Horizontal
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;

        if (Input.GetAxis("Horizontal") > 0f) // andando para direita
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f,0f,0f); // rotaciona para direita
        }

        else if (Input.GetAxis("Horizontal") < 0f) // andando para esquerda
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        else // ta parado
        {
            anim.SetBool("Walk", false);
        }
    }

    private void Die()
    {
        GameController.instance.ShowGameOver();
        Destroy(this.gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision) // é chamado enquanto um objeto estiver em constante colisão com outro
    {
        if (collision.gameObject.layer == 12) // layer do ventilador
        {
            isBlowing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // é chamado enquanto um objeto estiver em constante colisão com outro
    {
        if (collision.gameObject.layer == 12) // layer do ventilador
        {
            isBlowing = false;
        }
    }
}

// Vector
/**
 * Vector2 => é uma CLASSE que contém um vetor bidimensional
 * Vector3 => é uma CLASSE que contém um vetor tridimensional
 *  O uso de cada uma vai depender dos métodos que serão aplicados a elas
 *  no caso da movimentação de um personagem 2D é necessario usar um Vector3 ao invés de Vector2
 *  pois o 'trasnform.position' necessita ser usado um vetor 3 pois ele tem os 3 eixos (x,y,z)
*/

// movimentação horizontal
/**
 * Input => é uma estrrutura da propria engine que contem diversos metodos que recebem entradas do teclado
 * GetAxis => pega um nome de um parâmetro especifico.
 * Horizontal => é um Parâmetro que modifica a localização do eixo X do objeto e contem valores de -1 a 1 (esqueda / direita)
 */
