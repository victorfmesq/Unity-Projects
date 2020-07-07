using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private BoxCollider2D boxCollider2D;
    private CircleCollider2D circleCollider2D;

    private bool colliding; // variável que checa toda vez que o inimigo encostar numa parede;

    public float speed;

    public Transform uperCol; // recebe um objeto vazio que será usado como primeira extremidade do colisor
    public Transform bottonCol; // recebe um objeto vazio que será usado como segunda extremidade do colisor

    public Transform headPoint; // Recebe um objeto vazio que fica dentro do colisor na cabeça do inimigo (serve como valor base para calcular se o player bateu o nao na cabeça)

    public LayerMask layer; // Guarda uma ou mais camadas;


    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movimentation();
    }

    bool playerDestoyed; // controla se player ta morto ou não (apenas para corrigir um bug pois o if e o else estava sendo chamados pois as vezes ocorria mais de uma colisao)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float height = collision.contacts[0].point.y - headPoint.position.y; // checa se o player bate na cabeça do inimigo 
            // -> pega o contato do player com o inimogo (captado pelo boxCollider)            -> collision.contacts[0].point.y
            // -> subtrai pelo valor da posição do headPoint                                   -> - headPoint.position.y

            if (height > 0 && playerDestoyed == false) // se o valor da subtração do ponto de contato pela altura altura do headPoint e o player ta vivo então o inimigo MORRE!
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse); // faz o personagem pular quando atingir a cabeça
                Die();
            }
            else // player morre
            {
                playerDestoyed = true;
                GameController.instance.ShowGameOver();
                Destroy(collision.gameObject); // destroi o player
            }
        }
    }

    private void Die()
    {
        speed = 0f; // para o inimigo
        anim.SetTrigger("Die");
        boxCollider2D.enabled = false;
        circleCollider2D.enabled = false;
        rig.bodyType = RigidbodyType2D.Kinematic;
        Destroy(this.gameObject, 0.33f);
    }

    private void Movimentation()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y); // velocity eh uma CLASSE do RIGIDBODY que adiciona uma velocidade. Caso o objeto nao tenha um, usa-se o Tranform.Translate

        colliding = Physics2D.Linecast(uperCol.position, bottonCol.position, (layer));

        // -> Physic2D.Linecast é um método que desenha um colisor invisivel em formato de linha entre 2 objetos na cena e retorna um Bool.
        // -> Detalhe: ele só vai alterar de FALSE p/ TRUE se o colisor criado entre os 2 pontos (uperCol e bottonCol) colidir com uma das camadas passadas para variável LAYER

        if (colliding) // verifica se o objeto esta colidindo;
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y); // muda a direção do eixo x do inimigo quando ele colidir
            speed *= -1f; // faz o inimigo correr para direção oposta
        }
    }
}
