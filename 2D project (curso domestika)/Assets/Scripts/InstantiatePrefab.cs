using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject _prefab; // prefab que instanciaremos
    public Transform _point; // onde instanciaremos, por isso o transform
    public float livingTime;

    public void Instantiate()
    {
        // Instancie um prefab em uma determinada posição com uma rotação normal como um GameObject
        GameObject instantiatedObject = Instantiate(_prefab, _point.position, Quaternion.identity) as GameObject;

        if (livingTime > 0f)
        {
            Destroy(instantiatedObject, livingTime);
        }
    }
}
