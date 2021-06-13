using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fox : MonoBehaviour
{
    
    

    //private fields
    Rigidbody2D rb;
    Animator animator;
    public Collider2D standingCollider,crouchingCollider;
    public Transform groundCheckCollider;
    public Transform overheadCheckCollider;
    public LayerMask groundLayer;


    const float groundCheckRadius = 0.2f;
    const float overheadCheckRadius = 0.2f;
    [SerializeField] float speed = 2;
    [SerializeField] float jumpPower = 100;
    [SerializeField] int totalJumps;
    int availableJumps;
    float horizontalValue;
    float runSpeedModifier = 2f;
    float crouchSpeedModifier = 0.5f;
    bool isGrounded;
    bool multipleJump;
    bool crouchPressed ;
    bool facingRight = true;
    bool isRunning;
    bool coyoteJump;
    bool isDead = false;

    void Awake()
    {
        availableJumps = totalJumps;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
    
    // Update is called once per frame
    void Update()
    {
        if(CanMoveOrInteract()==false)
        return;
        //set the yVelocity in the animator
        animator.SetFloat("yVelocity", rb.velocity.y);

        //store the horizontal value
        horizontalValue = Input.GetAxisRaw("Horizontal");
        // if leftshift is clicked isRunning enable
        if(Input.GetKeyDown(KeyCode.LeftShift))
        
            isRunning = true;
        
        //if leftshift is released isRunning disable
        if(Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        // if we press space bar enbale jump
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        
            

        // if we pess s enbale crouch
        if(Input.GetButtonDown("Crouch"))
            crouchPressed = true;
        //if not disable crouch
        else if(Input.GetButtonUp("Crouch"))
            crouchPressed = false;

        
        
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue, crouchPressed);

    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;


        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                availableJumps = totalJumps;
                multipleJump = false;

                AudioManager.instance.PlaySFX("landing");                
            }


            

        }
        else
        {
            if(wasGrounded)
            {
                StartCoroutine(CoyoteJumpDelay());
            }
        }
           

        
          

         animator.SetBool("Jump", !isGrounded);
        
    }

    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
    }
    void Jump()
    {
        if (isGrounded)
        {
            multipleJump = true;
            availableJumps--;
        
            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
        }
        else
        {
            if(coyoteJump)
            {
                multipleJump = true;
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
                Debug.Log("COYOTE JUMP");
            }
            if(multipleJump && availableJumps >0)
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
    }

    void Move(float dir, bool crouchFlag)
    {
        #region Crouch
        if(!crouchFlag)
        {
            if(Physics2D.OverlapCircle(overheadCheckCollider.position,overheadCheckRadius,groundLayer))
                 crouchFlag = true;
        }
        

        animator.SetBool("Crouch", crouchFlag);
        standingCollider.enabled = !crouchFlag;
        crouchingCollider.enabled = crouchFlag;
    #endregion


    #region Move & Run
    float xVal = dir * speed * 100 * Time.fixedDeltaTime; 
        if(isRunning)
        {
             xVal *= runSpeedModifier;
        }
        if(crouchFlag)
        {
             xVal *= crouchSpeedModifier;
        }
        
        Vector2 targetVelocity = new Vector2(xVal,rb.velocity.y);
        rb.velocity = targetVelocity;

        

        // right - flip right
        if(facingRight && dir<0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if(!facingRight && dir>0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckCollider.position, groundCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(overheadCheckCollider.position, overheadCheckRadius);

    }
    
    bool CanMoveOrInteract()
    {
        bool can = true;

        if (FindObjectOfType<InteractionSystem>().isExamining)
            can = false;
        if (FindObjectOfType<InventorySystem>().isOpen)
            can = false;
        if (isDead)
            can = false; 

        return can;
    }


    public void Die()
    {
        isDead = true;
        SceneManager.LoadScene("LooseScene");
        //FindObjectOfType<LevelManager>().Restart();
    }
}


