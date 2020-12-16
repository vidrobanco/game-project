using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Essential Components

    public HP hpComponent;

    /////// SWIMMING COMPONENTS ///////

    bool isSwim = false;

    public bool onLilyPad = true;

    public float maxSwimTime = 4f;

    public float swimTime;
    
    ///////////////////////////////////


    // Multiplier for the speed of the frog
    public float speed = 50f;

    readonly float maxJumpForce = 100f;

    Rigidbody2D rb2D;


    ////// FROG JUMPING COMPONENTS ///////

    Vector3 initScale;

    // Speed of the jump of the frog
    // NEEDS TO BE BIGGER THAN MOVEMENT
    public float jumpSpeed = 80f;

    float jumpForce = 0f;

    bool jumpCanceled = false;
    bool movingDone = true;

    //////////////////////////////////////

    #endregion

    #region Functions
    
    void FrogDeath()
    {
        GameObject.Find("Game Manager").transform.GetChild(0).gameObject.SetActive(true);
        hpComponent.hp = 0f;
    }

    void PointToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDir = mousePos - transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    IEnumerator Swim()
    {
        if (!isSwim)
        {
            isSwim = true;
            speed /= 2f;

            while (!onLilyPad)
            {
                swimTime -= 0.1f;

                if(swimTime <= 0f)
                {
                    FrogDeath();
                    break;
                }

                yield return new WaitForSeconds(0.1f);
            }

            speed *= 2f;

            swimTime = maxSwimTime;
            isSwim = false;
        }
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

                if (movingDone && onLilyPad)
                {
                    movingDone = false;
                    GetComponent<Collider2D>().isTrigger = true;
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
            // CHECKING FOR INPUT TO CHECK IF
            // THEY WANT TO CANCEL THE JUMP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpCanceled = true;
            }

            // The position it has to move to
            // (a small step for each frame for smooth movement)
            Vector3 moveTo = Vector3.MoveTowards(transform.position, dest, jumpSpeed * Time.fixedDeltaTime);

            if (!jumpCanceled)
            {
                if (lastMoveTo != moveTo)
                {
                    // Moves a step forward to the destination
                    rb2D.MovePosition(moveTo);
                }
            }

            if (lastDis == Vector3.Distance(transform.position, dest))
            {
                sameDisCounter++;
                lastDis = Vector3.Distance(transform.position, dest);
                if (sameDisCounter > 20)
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
        jumpCanceled = false;
        GetComponent<Collider2D>().isTrigger = false;
    }

    IEnumerator Scaling(Vector3 dest, float dis, float scaleChange)
    { 
        while (movingDone == false)
        {
            if (Vector3.Distance(transform.position, dest) <= dis / 2)
            {
                if(transform.localScale.x > initScale.x)
                    transform.localScale -= new Vector3(scaleChange, scaleChange);
            }
            else
            {
                transform.localScale += new Vector3(scaleChange, scaleChange);
            }

            if (jumpCanceled)
                break;

            yield return null;
        }
        if (jumpCanceled)
        {
            while(transform.localScale != initScale)
            {
                transform.localScale -= new Vector3(scaleChange, scaleChange);

                yield return null;
            }
        }

        transform.localScale = initScale;
    }

    #endregion

    #region Unity Processes

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        initScale = transform.localScale;
        swimTime = maxSwimTime;
        StartCoroutine(GetJumpInput());
    }

    // Update is called once per frame
    void Update()
    {
        PointToMouse();
    }

    // FPS can vary so FixedUpdate makes it at a fixed rate per second
    private void FixedUpdate()
    {
        rb2D.velocity = new Vector3(Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime,
                                    Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime);

        if (onLilyPad != true && !isSwim)
            StartCoroutine(Swim());
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Lilypad")
        {
            onLilyPad = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lilypad")
        {
            onLilyPad = false;
        }
    }

    #endregion
}
