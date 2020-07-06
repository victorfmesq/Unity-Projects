using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    private Rigidbody2D _rig;
    private BoxCollider2D _boxCollider2D;
    private Animator _anim;

    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float totalHealth = 1f;
    [SerializeField]
    private float currentHealth;
  
    public float damage = 10f;

    private GameObject _target;

    [SerializeField]
    private float minX = 0f;
    [SerializeField]
    private float maxX = 3f;

    private float waitingTime = 1f;

    private bool canMove;

    private Player _player;

    public void Awake()
    {
        _player = FindObjectOfType<Player>().GetComponent<Player>();
        _rig = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
        currentHealth = totalHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        UpdateTarget();
        StartCoroutine(Patrol());
    }

    private void UpdateTarget()
    {
        if (_target == null)
        {
            _target = new GameObject("Target");
            _target.transform.position = new Vector2(minX, transform.position.y); // seta o alvo na esquerda
            this.transform.localScale = new Vector3(-1f, 1f, 1f); // vira o boneco para esquerda
            return;
        }

        if (_target.transform.position.x == minX)
        {
            _target.transform.position = new Vector2(maxX, transform.position.y); // seta o alvo na direita
            this.transform.localScale = new Vector3(1f, 1f, 1f); // vira o boneco para direita
            return;
        }
        else if (_target.transform.position.x == maxX)
        {
            _target.transform.position = new Vector2(minX, transform.position.y); // seta o alvo na esquerda
            this.transform.localScale = new Vector3(-1f, 1f, 1f); // vira o boneco para esquerda
        }

    }
    
    private IEnumerator Patrol()
    {
        _anim.SetBool("Walk", true);
        while (Vector2.Distance(transform.position, _target.transform.position) > 0.1f)
        {
            // move
            if (canMove == false)
            {
                yield return new WaitForSeconds(waitingTime);
            }

            Vector2 direction = _target.transform.position - transform.position;

            this.transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);

            yield return null;
        }

        //this.transform.position = new Vector2(_target.transform.position.x, this.transform.position.y);

        UpdateTarget();
        _anim.SetBool("Walk", false);

        yield return new WaitForSeconds(waitingTime);

        StartCoroutine(Patrol());
    }

    public void GetHit(float damage)
    {
        canMove = false;
        _anim.SetBool("Hit", true);
        currentHealth -= damage;
        if (currentHealth < 1)
        {
            this.Die();
        }
        else
        {
            StartCoroutine(RecoveryFromTheHit());
        }
    }

    private IEnumerator RecoveryFromTheHit()
    {
        yield return new WaitForSeconds(0.8f);
        _anim.SetBool("Hit", false);
        canMove = true;
    }
    private void Die()
    {
        StopAllCoroutines();
        _anim.SetBool("Hit", false);
        _anim.SetBool("Dead", true);
        _anim.SetBool("Walk", false);
        Destroy(this._rig);
        Destroy(this._boxCollider2D);
    }

    private bool canAttack = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canAttack)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                canMove = false;
                canAttack = false;
                _anim.SetBool("Attack", true); // animação que vai controlar o dano... 
                StartCoroutine(RecoveryFromTheAttack());
            }
        }
    }

    private IEnumerator RecoveryFromTheAttack()
    {       
        yield return new WaitForSeconds(0.8f);
        _anim.SetBool("Attack", false);
        canMove = true;
        canAttack = true;
    }

}
