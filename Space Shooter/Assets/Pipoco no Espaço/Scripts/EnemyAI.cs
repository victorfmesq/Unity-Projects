using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    private GameManager _gameManager;
    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _clip; // Classe que segura um clip de audio

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        EnemyMovement();
        if (_gameManager.gameOver == true)
        {
            Explosion();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy colidiu com: " + other.name);

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Explosion();
        }
        else if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject);
            CountScore();
            Explosion();
        }
    }

    private void CountScore()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.UpdateScore();
        }
    }

    private void Explosion()
    {
        // gerando som quando destrou um inimigo
        // Camera.main é apra o som sair na camera que está mais proximo do jogador, se nao o som sai em 3d
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position); 
        Destroy(this.gameObject);
        Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity); // animação de explosao
    }

    private void EnemyMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            float randomX = Random.Range(-7.7f, 7.7f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
}
