using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Player : MonoBehaviour
{
    [SerializeField]
    StaminaManagement stamina;

    private float horizontal;
    private float vertical;
    public float speed = 8f;
    //public float runSpeed = 16f;
    public float jumpingPower = 16f;
    public float highJumpingPower = 24f;
    private bool isFacingRight = true;
    //private bool isRuning;
    private bool isAttacking;
 

    private bool isWallSliding;
    public float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.2f;
    private Vector2 wallJumpingPower = new Vector2(12f, 12f);

    //private float jumpStaminaMaxCounter = 0.6f;
    //private float jumpStaminaCounter;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [SerializeField]
    private float radioGolpe;
    [SerializeField]
    private int dañoGolpe;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform attackPosition;

    [SerializeField] GameObject circulo;

    [SerializeField] Animator animation;


    private double fallMultiplier = 2.5f;
    private double lowJumpMultiplier = 2f;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R)) {
            Application.LoadLevel(Application.loadedLevel);
            
        }

        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (IsGrounded() && horizontal == 0 && !IsCrouching()) {
            coyoteTimeCounter = coyoteTime;
            animation.SetInteger("State", 0);
        }
        if (IsGrounded() && horizontal != 0)
        {
            coyoteTimeCounter = coyoteTime;
            //animation.SetInteger("State", 1);
        }
        if (!IsGrounded() && !IsWalled())
        {
            coyoteTimeCounter -= Time.deltaTime;
            animation.SetInteger("State", 2);
        }
        if (!IsGrounded() && IsWalled() && horizontal != 0)
        {
            coyoteTimeCounter -= Time.deltaTime;
            animation.SetInteger("State", 3);
        }

        if (isAttacking == false) { 
            Jump();
            //HighJump();
            WallSlide();
            WallJump();
            ConsumeFood();
            //DebugFillStamina();
            Golpe();
        }

       /* if (Input.GetButtonDown("Fire3") && stamina.GetCurrentStamina() >= stamina.GetDashStaminaCost()) {
            stamina.DashStaminaLos();
            StartCoroutine(Dash());
        }*/

        if (!isWallJumping && isAttacking == false)
        {
            Flip();
        }
    }

    public void attackingTrue() {
        isAttacking = true;
    }
    public void attackingFalse() {
        isAttacking = false;
    }



    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        Walk();
        //Run();
    }

    private IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }

    private void Walk(){
        
        if (!isWallJumping && !IsCrouching() && isAttacking == false /*&& !IsRuning()*/)
        {
            animation.SetInteger("State", 1);
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        if (IsCrouching() || isAttacking == true)
        {
            
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void Jump() {

        
        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0 && !IsCrouching() )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            coyoteTimeCounter = 0;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        /*
        //Gravity Jump
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) *Time.deltaTime;
        }
        */
    }

    private void ConsumeFood() {

        if (Input.GetButtonDown("Fire1")) {
            stamina.ConsumeFood();
        }

    }
    
    private void DebugFillStamina() {

        if (Input.GetButtonDown("Fire1")) {
            stamina.DebugFullStamina();
        }

    }

    private void HighJump() {
        if (Input.GetButtonDown("Jump")  && IsCrouching() && stamina.GetCurrentStamina() >= stamina.GetHighJumpStaminaCost())
        {
            
            stamina.HighJumpStaminaLoss();
            rb.velocity = new Vector2(rb.velocity.x, highJumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(0.35f, 0.1f) ,0f, groundLayer);
    }

    private void Golpe()
    {

        if (Input.GetButtonDown("Fire2") && stamina.GetCurrentStamina() >= stamina.GetAttackCost() && IsGrounded())
        {
            animation.SetInteger("State", 4);

        }

        
    }

    public void GolpeImpacto() {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(attackPosition.position, radioGolpe);
        stamina.AttackStaminaLoss();
        foreach (Collider2D colisionador in objetos)
        {

            if (colisionador.CompareTag("Enemy")|| colisionador.CompareTag("Rock"))
            {
                colisionador.transform.GetComponent<Enemigo>().takeDamage(dañoGolpe);
            }
            if (colisionador.CompareTag("Explosive"))
            {
                //colisionador.transform.GetComponent<Explosive>().Explosion();
            }
            /*
            if (Input.GetKey(KeyCode.C))
            {

                circulo.SetActive(true);
            }
            if(Input.GetKeyUp(KeyCode.C))
            {

                circulo.SetActive(false);
            }
            */
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawCube(groundCheck.position, new Vector2(0.35f, 0.1f)); 
        
        Gizmos.color = new Color(1, 0, 1, 0.75F);
        Gizmos.DrawSphere(wallCheck.position, 0.2f);

        Gizmos.color = new Color(1, 1, 1, 0.75F);
        Gizmos.DrawSphere(attackPosition.position, radioGolpe);
    }
    

    private bool IsCrouching(){

        Vector3 localScale = transform.localScale;
        if (vertical < -0.5 && IsGrounded())
        {
            animation.SetInteger("State", 5);
            //localScale.y = 0.6f;
            //transform.localScale = localScale;
            return true;
        }
        else {
            if (vertical >= -0.5) { 
                //localScale.y = 1f;
                //transform.localScale = localScale;
            }
            return false;
        }
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f && stamina.GetCurrentStamina() >= stamina.GetWallJumpStaminaCost())
        {
            stamina.WallJumpStaminaLoss();
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    /* private void Run() {
         if (IsRuning() && !isWallJumping && !IsCrouching() && stamina.GetCurrentStamina() != 0) {
             rb.velocity = new Vector2(horizontal * runSpeed, rb.velocity.y);

             if (IsGrounded()) {
                 jumpStaminaCounter = jumpStaminaMaxCounter;
                 stamina.RunningStaminaLoss();
             }
             if (!IsGrounded() && jumpStaminaCounter > 0) {
                 jumpStaminaCounter -= Time.deltaTime;
                 stamina.RunningStaminaLoss();
             }
         }

         if (IsCrouching())
         {
             rb.velocity = new Vector2(0, rb.velocity.y);
         }
     }
     */

    /*
    private bool IsRuning()
    {
        
        if (Input.GetButton("Fire3") && IsGrounded() && stamina.GetCurrentStamina() > 0 && horizontal != 0) {
            isRuning = true;
        }
        if (!Input.GetButton("Fire3") && IsGrounded() || horizontal == 0 || stamina.GetCurrentStamina() == 0)
        {
            isRuning = false;
        }

        return isRuning;
        
    }
     */
}

