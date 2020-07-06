using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Attack : MonoBehaviour
{
    private Enemy_1 _enemy1;
    private float waitingTime = 1f;
    private bool canAttack;

    private void Start()
    {
        _enemy1 = FindObjectOfType<Enemy_1>().GetComponent<Enemy_1>();
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canAttack == true)
        {
            canAttack = false;
            Player player = FindObjectOfType<Player>().GetComponent<Player>();
            if (player != null)
            {
                player.GetHit(_enemy1);
                Debug.Log("Acertou o player");
                StartCoroutine(RecoveryFromAttack());
            }
        }
    }

    IEnumerator RecoveryFromAttack()
    {
        yield return new WaitForSeconds(waitingTime);
        canAttack = true;
    }
}
