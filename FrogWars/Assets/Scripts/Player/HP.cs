using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float hp = 100f;

    // DO NOT CHANGE UNLESS
    // A POWERUP IS ADDED TO IT
    public float MAXHP = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
