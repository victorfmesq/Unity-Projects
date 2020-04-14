using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups; // array de gameobjects dos powerups
    /*
        powerups[0] -> triple shot
        powerups[1] -> Speed Boost
        powerups[2] -> Shield
    */
    public bool stopper;

    private UIManager _uiManager;
    private GameManager _gameManager;

    // Start is called before the first frame updat
    private void Start()
    {
    
    }

    private void Update()
    {
       
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            float randomSpawn = Random.Range(-7.7f, 7.7f);
            transform.position = new Vector3(randomSpawn, 7, 0);
            Instantiate(enemyShipPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }
   
    IEnumerator SpawnPowerupsRoutine()
    {
        while (true)
        {
            int randomPowerup = Random.Range(0, 3);
            float randomSpawn = Random.Range(-7.7f, 7.7f);
            transform.position = new Vector3(randomSpawn, 7, 0);
            Instantiate(powerups[randomPowerup], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }

    public void StartSpawn()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupsRoutine());
    }
}
