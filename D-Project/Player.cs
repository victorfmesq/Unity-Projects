using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rig;
    private Animator _anim;

    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float jumpForce = 5f;
    private bool isJumping;



    void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        StartCoroutine(Attack());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            _anim.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    private bool canMove = true;
    private void Move()
    {
        if (canMove == true)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * moveSpeed * Time.deltaTime;

            if (Input.GetAxis("Horizontal") > 0f) // anda pra direita
            {
                _anim.SetBool("Run", true);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (Input.GetAxis("Horizontal") < 0f) // anda para esquerda
            {
                _anim.SetBool("Run", true);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else
            {
                _anim.SetBool("Run", false);
            }
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isJumping == false)
            {
                _rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                _anim.SetBool("Jump", true);
            }
        }
    }

    private bool canAttack = true;
    private IEnumerator Attack()
    {
        if (canAttack == true)
        {
            if (isJumping == false && Input.GetKeyDown("z"))
            {
                _anim.SetBool("Run", false);
                _anim.SetBool("Attack1", true);
                canMove = false;
                canAttack = false;

                yield return new WaitForSeconds(0.6f);

                _anim.SetBool("Attack1", false);

                yield return new WaitForSeconds(0.5f);

                canMove = true;
                canAttack = true;
            }
        }
    }
}
