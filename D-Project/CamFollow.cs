using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    private Transform _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>().GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        transform.position = _player.position + new Vector3(0f, 1.31f, -10f);
    }
}
