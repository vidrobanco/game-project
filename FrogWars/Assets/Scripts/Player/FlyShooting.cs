﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyShooting : MonoBehaviour
{
    public Inventory inv;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            inv.ChangeSelFly(true);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            inv.ChangeSelFly(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (inv.FliesAvailable())
            {
                Shoot();
                inv.ChangeFlyCountVal(-1);
            }
        }
    }

    void Shoot()
    {
        print("Somehow I got here...?");
    }
}
