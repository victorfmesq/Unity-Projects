using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    public float jump;
    private Rigidbody2D rigidbody;
    private bool isJumping;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);

        if(Input.GetMouseButtonDown(0) && isJumping == false){

            rigidbody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isJumping = true;
        }
    }
    void OnCollisionEnter2D(Collision2D colisor){
        
        if(colisor.gameObject.layer == 8){

            isJumping = false;
        }
    }
}
