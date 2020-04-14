using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int Score;
    public Text scoreText;
    public Player player;

    public void Start()
    {
        //if (player.inGame == true)
        //{
        //    Time.timeScale = 1;
        //}
        Time.timeScale = 0;
    }

    public void Update()
    {
        if (player.inGame == true && player.game_over == false)
        {
            Time.timeScale = 1;
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        player.inGame = true;
    }

}
