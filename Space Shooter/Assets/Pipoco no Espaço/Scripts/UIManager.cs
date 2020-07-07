using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject _titleScreen;

    public Sprite[] Lives;
    public Image LivesImageDisplay;
    public Text scoreText;
    public int score;

    public void Start()
    {

    }

    public void UpdateLives(int currentLives)
    {
        Debug.Log("O player tem: " + currentLives);
        LivesImageDisplay.sprite = Lives[currentLives]; // atribui a imagem na posição currentLives ao Display
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score; // atribui um texto ao display
    }

    public void ShowTitleScreen()
    {
        _titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        _titleScreen.SetActive(false);
        scoreText.text = "Score: ";
    }

}
