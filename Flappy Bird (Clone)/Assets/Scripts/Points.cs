using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public GameController controller;

    private void Start()
    {
        // como esse objeto eh um Prefab, nao se pode passar como referencia um objeto que esteja na cena principal
        // entao usa-se esse método que faz com que o objeto procure por outro objeto do tipo GameController para passar as informações para ele
        controller = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller.Score++; // incrementa contador de scores
        controller.scoreText.text = controller.Score.ToString();// transforma scores numa string para aplicar no texto mostrado na tela
    }
}
