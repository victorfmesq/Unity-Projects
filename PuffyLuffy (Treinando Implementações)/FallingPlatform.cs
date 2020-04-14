using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallingTime;

    private TargetJoint2D target;
    private BoxCollider2D boxcoll;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxcoll = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("Falling", fallingTime); // chama um metodo do script passado por uma string, depois de um determinado tempo -> Invoke(stringMethod: , time: );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
    }
    void Falling()
    {
        target.enabled = false;
        boxcoll.isTrigger = true;
    }
}
