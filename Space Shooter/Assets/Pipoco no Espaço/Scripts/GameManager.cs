using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;

    public GameObject _playerSpaceShip;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Instantiate(_playerSpaceShip, new Vector3(0, 0, 0), Quaternion.identity);
                gameOver = false;
                _uiManager.HideTitleScreen();
                _uiManager.score = 0;
                _spawnManager.StartSpawn();
            }
        }
    }
}
