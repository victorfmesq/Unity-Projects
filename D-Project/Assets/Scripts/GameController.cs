using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalScore;
    public Text scoreText;

    public GameObject _nextLvl;
    public GameObject _gameOver;

    public static GameController instance;

    void Start()
    {
        instance = this;
    }

    public void UpdateLifeText() // implementar depois uma barra
    {
        
    }

    public void UpdateStaminText() // implementar depois uma barra
    {
        
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        _gameOver.SetActive(true);
    }

    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
    public void ShowNextLvl()
    {
        _nextLvl.SetActive(true);
    }

}


    
