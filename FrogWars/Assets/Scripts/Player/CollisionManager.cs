using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public Inventory inv;
    Collider2D cd2D;

    // Start is called before the first frame update
    void Start()
    {
        cd2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fly")
        {
            collision.GetComponent<Fly>().IncreaseSpeed();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fly")
        {
            collision.GetComponent<Fly>().DecreaseSpeed();
        }
    }
}
