using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player _player;
    List<Transform> EnemyList = new List<Transform>();
    private float waitingTime = 1f;
    private bool canAttack;

    private void Start()
    {
        _player = FindObjectOfType<Player>().GetComponent<Player>();
        canAttack = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyList.Clear();
        if (collision.gameObject.CompareTag("Enemy") && canAttack == true)
        {
            canAttack = false;
            EnemyList.Add(collision.transform);

            foreach (Transform enemies in EnemyList)
            {
                Enemy_1 enemy = enemies.GetComponent<Enemy_1>();

                if (enemy != null)
                {
                    enemy.GetHit(_player.damage);
                    Debug.Log("Acertou o inimigo");
                    StartCoroutine(RecoveryFromAttack());
                }
            }
        }
    }

    IEnumerator RecoveryFromAttack()
    {
        yield return new WaitForSeconds(waitingTime);
        canAttack = true;
    }
}
