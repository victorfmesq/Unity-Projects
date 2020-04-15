using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;

    public float lifeTime = 3f;

    void Start()
    {
        // Destroy the bullet after some time
        Destroy(this.gameObject, lifeTime);
    }

    void Update()
    {
        Vector2 movement = direction.normalized * speed * Time.deltaTime;

        transform.Translate(movement);
    }
}
