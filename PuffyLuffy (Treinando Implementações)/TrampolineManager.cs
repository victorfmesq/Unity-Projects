using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineManager : MonoBehaviour
{
    private Animator anim;
    public float impulse;
    public string direction;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("Jump");
            if (direction == "up")
            {
                this.ImpulsePlayerUp(collision);
            }
            else if (direction == "down")
            {
                this.ImpulsePlayerDown(collision);
            }
            else if (direction == "left")
            {
                this.ImpulsePlayerLeft(collision);
            }
            else if (direction == "right")
            {
                this.ImpulsePlayerRight(collision);
            }
        }
    }
    private void ImpulsePlayerUp(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, impulse), ForceMode2D.Impulse);
    }

    private void ImpulsePlayerDown(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -impulse), ForceMode2D.Impulse);
    }

    private void ImpulsePlayerLeft(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-impulse, 0f), ForceMode2D.Impulse);
    }

    private void ImpulsePlayerRight(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(impulse, 0f), ForceMode2D.Impulse);
    }
}
