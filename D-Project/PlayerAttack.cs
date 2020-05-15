using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player _player;
    List<Transform> EnemyList = new List<Transform>();

    private void Start()
    {
        _player = FindObjectOfType<Player>().GetComponent<Player>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyList.Clear();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyList.Add(collision.transform);

            foreach (Transform enemies in EnemyList)
            {
                Enemy_1 enemy = enemies.GetComponent<Enemy_1>();

                if (enemy != null)
                {
                    enemy.GetHit(_player.damage);
                }
            }
        }
    }
}
