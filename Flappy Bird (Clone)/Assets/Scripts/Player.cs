using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D rig;

    public GameObject gameOver;
    public GameObject startGameButton;

    public bool inGame = false;
    public bool game_over = false;

    void Start()
    {
        if (inGame == false)
        {
            startGameButton.SetActive(true);
        }
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(inGame == true)
        {
            startGameButton.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            rig.velocity = Vector2.up * speed;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        gameOver.SetActive(true);
        game_over = true;
        Time.timeScale = 0;
    }
}
