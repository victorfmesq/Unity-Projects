using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject _followObject;
    public Vector2 followOffset;
    public float speed = 3f;
    private Vector2 threshold;
    private Rigidbody2D _rig;

    private void Start()
    {
        threshold = calculateThreshold();
        _rig = _followObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 follow = _followObject.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * this.transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * this.transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = this.transform.position;

        if (Mathf.Abs(xDifference) >= threshold.x) // Mathf.abs retorna um valor absoluto sempre positivo
        {
            newPosition.x = follow.x;
        }
        if (Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }
        float moveSpeed = _rig.velocity.magnitude > speed ? _rig.velocity.magnitude : speed; // caso o objeto tenha um rigidbody
        this.transform.position = Vector3.MoveTowards(this.transform.position, newPosition, moveSpeed * Time.deltaTime);
    }
    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
