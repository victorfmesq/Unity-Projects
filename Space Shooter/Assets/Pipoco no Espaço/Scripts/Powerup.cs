using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; // 0 -> TripleShot / 1 -> Speed Bost / 2-> Shields
    [SerializeField]
    private AudioClip _clip;
  
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Power-up colidiu com: " + other.name);

        if (other.tag == "Player") // verifica se o objeto colidido eh o player (apenas o PLAYER tem a tag player)
        {
            Player player = other.GetComponent<Player>(); // instancio player para pegar os componentes publicos

            if (player != null) // testa se o GetComponent eh != de null, par aevitar erros
            {
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
                if (this.powerupID == 0)
                {
                    // Enable triple shot
                    player.TripleSHotPowerUpOn();
                }
                else if (this.powerupID == 1)
                {
                    // Enable Speed Bost
                    player.SpeedBoostPowerUpOn();
                }
                else if (this.powerupID == 2)
                {
                    // Enable Shields
                    player.EneableShield();
                }
            }

            Destroy(this.gameObject); // destruo o objeto de power-up       
        }
    }
}
