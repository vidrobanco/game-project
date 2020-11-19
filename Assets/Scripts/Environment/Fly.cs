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

    IEnumerator ChangeFlySpeedTimer()
    {
        while (true)
        {
            SetFlySpeed();
            yield return new WaitForSeconds(durOfDir);
        }
    }

    void SetFlySpeed()
    {
        // Moves the fly to one direction for 10 seconds
        
        float xSpeed = Random.Range(-flySpeed, flySpeed);
        float ySpeed = Random.Range(-flySpeed, flySpeed);

        rb.velocity = new Vector2(xSpeed, ySpeed);

        if (xSpeed > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    #endregion

    #region Unity Processes

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeFlySpeedTimer());
    }

    public void IncreaseSpeed()
    {
        print("Fly: AAAAAAAAAAAAA");
        flySpeed *= 8f;
        durOfDir /= 2f;
        SetFlySpeed();
    }

    public void DecreaseSpeed()
    {
        print("Fly: Phew!!!");
        flySpeed /= 8f;
        durOfDir *= 2f;
        SetFlySpeed();
    }

    #endregion
}
