using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Essential Components

    // Multiplier for the speed of the frog
    public float speed = 50f;

    // Speed of the jump of the frog
    // NEEDS TO BE BIGGER THAN MOVEMENT
    public float jumpSpeed = 80f;

    float jumpForce = 0f;

    readonly float maxJumpForce = 100f;

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
                        jumpForce += 2f;
                    
                    yield return new WaitForSeconds(.02f);
                }

                StartCoroutine(FrogJump());

                print("Jumped successfully!"); // FOR DEBUG
            }
            
            yield return null;
        }
    }

    IEnumerator FrogJump()
    {
        List<float> scales = new List<float>();
        int scIndex = 0;
        bool listIndexCounted = false;

        Vector3 dest = transform.position + (transform.up * jumpForce);
        float initDis = Vector3.Distance(transform.position, dest);
        float midDis = Vector3.Distance(transform.position, dest - transform.position);

        float initScale = transform.localScale.x;

        bool midReached = false;

        // How big it will change each time
        float scaleChange;

        while (Vector3.Distance(transform.position, dest) >= 1f)
        {
            // The position it has to move to
            // (a step for each frame)
            Vector3 moveTo = Vector3.MoveTowards(transform.position, dest, jumpSpeed * Time.fixedDeltaTime);

            if (midReached)
            {
                if (!listIndexCounted)
                {
                    scIndex = scales.Count - 1;
                    listIndexCounted = true;
                }
                if (scIndex >= 0 && scIndex <= scales.Count)
                {
                    transform.localScale -= new Vector3(scales[scIndex], scales[scIndex], 0f);
                    scIndex--;
                }
            }
            else
            {
                scaleChange = Vector3.Distance(transform.position, moveTo) / midDis;
                scales.Add(scaleChange);
                transform.localScale += new Vector3(scaleChange, scaleChange, 0f);
            }

            rb2D.MovePosition(moveTo);

            if (Vector3.Distance(transform.position, dest) <= initDis / 2)
            {
                midReached = true;
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
