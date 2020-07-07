using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private SpriteRenderer sprite;
    private CircleCollider2D circle;

    public GameObject collected;
    public int score;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            circle.enabled = false;
            sprite.enabled = false;
            collected.SetActive(true);

            GameController.instance.totalScore += score; // adiciona o valor atual de score ao scoreTotal de GameController (script e gameObject que controla o score total do jogo)
            GameController.instance.UpdateScoreText();

            Destroy(this.gameObject, 0.3f);

        }
    }
}
