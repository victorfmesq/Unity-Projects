using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab; // guarda o prefab da munição
    public GameObject shooter; // guarda quem está com a arma

    private Transform _firePoint; // local onde a munição deve ser disparada

    // Awake is called when the gameObject is instanciated
    private void Awake()
    {
        _firePoint = transform.Find("FirePoint");
    }
    // Start is called before the first frame update
    void Start()
    {
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        if (bulletPrefab != null && _firePoint != null && shooter != null){
                                  // instantiate a bulletPrefab on _firePoint position with default rotation (1,1,1);
            GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject; // Cria um GameObject que recebe um prefab e é isntanciado como um Gameobject

            Bullet bulletComponent = myBullet.GetComponent<Bullet>(); // instancia um objeto da classe Bullet atraves de myBullet

            // verifica se o atirador esta olhando para direita ou esquerda
            if (shooter.transform.localScale.x > 0f){ 
                //Right
                bulletComponent.direction = Vector2.right; // new Vector2(-1f, 0f)
            }
            else{
                //Left
                bulletComponent.direction = Vector2.left; // new Vector2(1f, 0f);
            }
        }
    }
}
