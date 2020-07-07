using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool podePipocoTriplo = false; // primeiro power-up triple shoot
    public bool podeCorrerRapido = false; // segundo power-up speed boost
    public bool podeAtivarEscudo = false; // terceiro power-up shields

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _pipocoTriploPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField] // serve para visualizar no INSPECTOR as variaveis do tipo PRIVATE
    private float _fireRate = 0.25f;
    
    private float _canFire = 0.0f;
    [SerializeField]
    private float _speed = 5.0f; // f no final especifica que estou usando um valor decimal
    [SerializeField]
    public int _lives = 3;

    private UIManager _uiManager; // instancio UImanager aqui
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    // Executo no começo do game
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>(); // encontre o objeto "Canvas" e pegue o componente UIManager

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(_lives);
        }

    }
    
    private void Update()
    {
        Mover();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Pipocar();
        }
    }

    private void Pipocar() {
        if (Time.time > _canFire) // se o tempo atual for maior que _canFire
        {
            _audioSource.Play();
            if (podePipocoTriplo == true)
            {
                Instantiate(_pipocoTriploPrefab, transform.position, Quaternion.identity);
            }
            // instacie o objeto laser na posição atual da nave + 0.88 na posição y (p ficar a frnete da nave), com a rotação padrao
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            _canFire = Time.time + _fireRate; // _canFire recebe tempo atual + tempo de Cool Down 
        }
    }

    private void Mover()

    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (podeCorrerRapido == true)
        {
            _speed = 7.5f; // aumenta a velocidade por 1.5
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        else
        {
            _speed = 5.0f;
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime); // movimenta na horizontal
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime); // movimenta na vertical
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
        if (transform.position.x > 8.0f)
        {
            transform.position = new Vector3(8.0f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.0f)
        {
            transform.position = new Vector3(-8.0f, transform.position.y, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (podeAtivarEscudo == true)
        {
            podeAtivarEscudo = false;
            _shieldGameObject.SetActive(false); // desativa um gameobject
            return;
        }
        else
        {
            _lives--;
            _uiManager.UpdateLives(_lives);

            if (_lives < 1)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _gameManager.gameOver = true;
                _uiManager.ShowTitleScreen();
                Destroy(this.gameObject);
                _spawnManager.StopAllCoroutines();
            }
        }
    }

    public void EneableShield()
    {
        podeAtivarEscudo = true;
        _shieldGameObject.SetActive(true); // Ativa um GameObject
    }

    public void SpeedBoostPowerUpOn()
    {
        podeCorrerRapido = true;
        StartCoroutine(this.SpeedBoostDownRoutine());
    }

    public IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        podeCorrerRapido = false;
    }

    public void TripleSHotPowerUpOn() // pipoco triplo ta ativado
    {
        podePipocoTriplo = true; 
        StartCoroutine(this.TripleShotPowerDownRoutine()); // chama COROUTINE do power down
    }

    public IEnumerator TripleShotPowerDownRoutine() // funcção que executa ou para uma ação em x segundos
    {
        yield return new WaitForSeconds(5.0f); // espera 5 segundos e transforma pipoco triplo em falso
        podePipocoTriplo = false; // volta a atirar normalmente
    }
}
