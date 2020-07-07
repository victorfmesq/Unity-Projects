﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint : MonoBehaviour
{
    public string levelName;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameController.instance.ShowNextLvl();
        }
    }

    public void SkipScene()
    {
        SceneManager.LoadScene(levelName);
    }
}
