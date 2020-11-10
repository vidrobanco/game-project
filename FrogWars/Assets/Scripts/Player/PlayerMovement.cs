using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Essential Components

    // Multiplier for the speed of the frog
    public float speed = 50f;

    float jumpForce = 0f;
    readonly float maxJumpForce = 1000000f;

    Rigidbody2D rb2D;

    Collider2D cd2D;

    #endregion

    #region Functions
    
    void PointToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDir = mousePos - transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    /// <summary>
    /// A loop running in parallel to Update, 
    /// FixedUpdate etc which has the sole
    /// purpose of taking in the jump input
    /// </summary>
    /// <returns></returns>
    IEnumerator GetJumpInput()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("Jumping!!!"); // FOR DEBUG

                // Reset jump force
                jumpForce = 0f;

                // User holds it until they would like
                // to make the frog jump
                while (Input.GetKey(KeyCode.Space))
                {
                    if (jumpForce < maxJumpForce)
                        jumpForce += 200f;
                    
                    yield return new WaitForSeconds(.02f);
                }

                rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Force);

                print("Jumped successfully!"); // FOR DEBUG
            }
            
            yield return null;
        }
    }

    #endregion

    #region Unity Processes

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(GetJumpInput());
    }

    // Update is called once per frame
    void Update()
    {
        PointToMouse();
    }

    // Frames can vary so FixedUpdate makes it at a fixed rate per second
    private void FixedUpdate()
    {
        rb2D.velocity = new Vector3(Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime,
                                    Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime);
    }

    // TO DO: Collide with lily pads

    #endregion
}
