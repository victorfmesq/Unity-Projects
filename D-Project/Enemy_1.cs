using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    private Rigidbody2D _rig;
    private Animator _anim;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float totalHealth;
    [SerializeField]
    private float currentHealth;

    private GameObject _target;

    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;

    private float waitingTime = 1f;

    public void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        currentHealth = totalHealth;
    }

    // Start is called before the first frame update
    void Start()
    {       
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
    private bool canMove = true;
    private IEnumerator Patrol()
    {
        if (canMove == true)
        {
            _anim.SetBool("Walk", true);
            while (Vector2.Distance(transform.position, _target.transform.position) > 0.1f)
            {
                // move
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
    }

    public void GetHit(float damage)
    {
        canMove = false;
        _anim.SetBool("Hit", true);
        currentHealth -= damage;
        StartCoroutine(RecoveryFromTheHit());
    }

    private IEnumerator RecoveryFromTheHit()
    {
        yield return new WaitForSeconds(0.5f);
        _anim.SetBool("Hit", false);
        canMove = true;
    }
}
