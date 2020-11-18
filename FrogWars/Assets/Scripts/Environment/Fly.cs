using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    #region Components

    public bool flyAlive = true;

    public float flySpeed = 4f;
    public float durOfDir = 3f;

    Rigidbody2D rb;

    #endregion

    #region Functions

    /// <summary>
    /// Makes the fly simply go in a random 
    /// direction for 3 seconds at a time.
    /// </summary>
    /// <returns></returns>
    IEnumerator FlyMovement()
    {
        while (flyAlive)
        {
            // Moves the fly to one direction for 10 seconds
            
            float xSpeed = Random.Range(-flySpeed, flySpeed);
            float ySpeed = Random.Range(-flySpeed, flySpeed);

            rb.velocity = new Vector2(xSpeed, ySpeed);

            if (xSpeed > 0)
                transform.localScale = new Vector3(1f, 1f, 1f);
            else
                transform.localScale = new Vector3(-1f, 1f, 1f);

            yield return new WaitForSeconds(durOfDir);
        }
    }

    #endregion

    #region Unity Processes

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FlyMovement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        flySpeed *= 2f;
        durOfDir /= 2f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        flySpeed /= 2f;
        durOfDir *= 2f;
    }

    #endregion
}
