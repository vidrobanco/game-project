using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lilypad : MonoBehaviour
{
    float moveSpeed = 0.1f;

    Rigidbody2D rb2D;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        dir = transform.position - Vector3.zero;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        dir = transform.up;

        transform.rotation = Quaternion.identity;
    }

    public void StartMoving()
    {
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        while (true)
        {
            rb2D.MovePosition(transform.position + (dir * moveSpeed));

            yield return null;
        }
    }
}
