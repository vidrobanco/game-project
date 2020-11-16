using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Essential Components

    // Multiplier for the speed of the frog
    public float speed = 50f;

    readonly float maxJumpForce = 100f;

    Rigidbody2D rb2D;


    ////// FROG JUMPING COMPONENTS ///////

    // Speed of the jump of the frog
    // NEEDS TO BE BIGGER THAN MOVEMENT
    public float jumpSpeed = 80f;

    float jumpForce = 0f;

    bool movingDone = true;

    //////////////////////////////////////


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
                // Reset jump force
                jumpForce = 0f;

                // User holds it until they would like
                // to make the frog jump
                while (Input.GetKey(KeyCode.Space))
                {
                    if (jumpForce < maxJumpForce)
                        jumpForce += 2f;
                    
                    yield return new WaitForSeconds(.02f);
                }

                if (movingDone)
                {
                    movingDone = false;

                    StartCoroutine(FrogJump());
                }
            }
            
            yield return null;
        }
    }

    IEnumerator FrogJump()
    {
        Vector3 dest = transform.position + (transform.up * jumpForce);

        StartCoroutine(Scaling(dest, Vector3.Distance(transform.position, dest), 0.2f));

        // The last stats;
        Vector3 lastMoveTo = transform.position;
        float lastDis = Vector3.Distance(transform.position, dest);
        int sameDisCounter = 0;

        while (Vector3.Distance(transform.position, dest) != 0f)
        {
            // The position it has to move to
            // (a small step for each frame for smooth movement)
            Vector3 moveTo = Vector3.MoveTowards(transform.position, dest, jumpSpeed * Time.fixedDeltaTime);

            if(lastMoveTo != moveTo)
            {
                // Moves a step forward to the destination
                rb2D.MovePosition(moveTo);
            }

            if (lastDis == Vector3.Distance(transform.position, dest))
            {
                sameDisCounter++;
                lastDis = Vector3.Distance(transform.position, dest);
                if (sameDisCounter > 4)
                    break;
            }
            else
            {
                sameDisCounter = 0;
                lastDis = Vector3.Distance(transform.position, dest);
            }

            lastMoveTo = moveTo;

            yield return null; // Waits until the frame ends to continue the loop
        }

        movingDone = true;
    }

    IEnumerator Scaling(Vector3 dest, float dis, float scaleChange)
    { 
        while (movingDone == false)
        {
            if (Vector3.Distance(transform.position, dest) <= dis / 2)
            {
                if(transform.localScale.x > 1f)
                    transform.localScale -= new Vector3(scaleChange, scaleChange);
            }
            else
            {
                transform.localScale += new Vector3(scaleChange, scaleChange);
            }

            yield return null;
        }

        transform.localScale = new Vector3(1f, 1f, 1f);
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
