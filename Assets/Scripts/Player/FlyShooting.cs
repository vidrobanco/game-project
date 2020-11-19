using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyShooting : MonoBehaviour
{
    public Inventory inv;

    public float shootForce = 20f;
    public Transform firePoint;

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
        var bullet = Instantiate(inv.flyPrefabs[inv.selectFlyNo], firePoint.position, firePoint.rotation);

        bullet.GetComponent<Rigidbody2D>().AddForce(shootForce * firePoint.up, ForceMode2D.Impulse);
    }
}
