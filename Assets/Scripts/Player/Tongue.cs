using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    public Inventory inv;

    public float tongueMaxSpeed = 30f;

    bool alreadyExtending = false;

    #region Functions

    IEnumerator TongueExtending()
    {
        alreadyExtending = true;
        transform.localScale = new Vector3(transform.localScale.x, tongueMaxSpeed, 1f);

        for (float speed = tongueMaxSpeed - 1f; transform.localScale.y != 0f; speed--)
        {
            transform.localScale = new Vector3(transform.localScale.x,
                                               transform.localScale.y + speed,
                                               transform.localScale.z);

            yield return new WaitForSeconds(0.015f);
        }
        alreadyExtending = false;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (alreadyExtending == false)
                StartCoroutine(TongueExtending());
        }
    }
}
