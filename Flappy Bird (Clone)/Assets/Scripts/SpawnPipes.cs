using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPipes : MonoBehaviour
{
    public GameObject pipe; // para referenciar o objeto que está com os canos
    public float height; // para definir autura minima e máxima dos canos
    public float maxTime = 1f; // intervalo de tempo de spawn de canos

    private float timer = 0f; // contador de tempo

    void Start()
    {
        GameObject newPipe = Instantiate(pipe);
        newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime)
        {
            GameObject newPipe = Instantiate(pipe);
            newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
            Destroy(newPipe, 10f);
            timer = 0f;
        }
        timer += Time.deltaTime;
    }
}
