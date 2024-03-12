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

    [SerializeField]
    GameObject ropeDisplayer;

    [SerializeField]
    GameObject ropeFly;
    
    [SerializeField]
    GameObject sweat; 
    
    [SerializeField]
    GameObject smokeJump;

    [SerializeField]
    GameObject smokeLand;

    public bool alive = true;
    

    [SerializeField]
    public Animator transition;

    public float originalGravityScale = 5;
    public float horizontal;
    public float vertical;
    public float speed = 8f;
    public float speedGround = 8f;
    public float speedWater = 5f;
    public float speedClimb = 3f;
    public float speedClimbNoStamina = 1f;
    //public float runSpeed = 16f;
    public float jumpingPower;
    public float jumpingPowerGround;
    public float jumpingPowerWater;
    public float highJumpingPower;
    private bool isFacingRight = true;
    //private bool isRuning;
    private bool isAttacking;


    private bool isWallSliding;
    public float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private bool isRopeJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.2f;
    private Vector2 wallJumpingPower = new Vector2(12f, 12f);
    private Vector2 ropeJumpingPower = new Vector2(12f, 12f);
    private Vector2 ropeJumpingPowerNoStamina = new Vector2(0.2f, 0.2f);

    //private float jumpStaminaMaxCounter = 0.6f;
    //private float jumpStaminaCounter;

    /*private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;*/
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float coyoteTimeWall = 0.2f;
    private float coyoteTimeCounterWall;

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

    private double lastYVelocity;

    public bool inWater = false;
    public bool inRope = false;
    private Vector3 lastRopeRail;
    public HashSet<GameObject> ropesHashSet = new HashSet<GameObject>();
    public bool readyToShootArrow;

    private void Update()
    {
        //Debug.Log(horizontal + " , " + vertical);

        if (!IsGrounded())
        {
            lastYVelocity = rb.velocity.y;
        }


        if (!inWater)
        {
            speed = speedGround;
            jumpingPower = jumpingPowerGround;
        }
        else {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            speed = speedWater;
            jumpingPower = jumpingPowerWater;
        }

        if (inWater && stamina.GetCurrentStamina() == 0 && !inRope)
        {
            Death();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            Application.LoadLevel(Application.loadedLevel);

        }

        /*if (isDashing)
        {
            return;
        }*/

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //Sprite Iddle
        if (IsGrounded() && horizontal == 0 && !IsCrouching() && alive && !inRope) {
            animation.speed = 1;
            coyoteTimeCounter = coyoteTime;
            animation.SetInteger("State", 0);
        }

        //Sprite Rope Ready
        if (IsGrounded() && vertical >= 0.5 && horizontal == 0 && !IsCrouching() && alive && ropesHashSet.Count == 0 && readyToShootArrow)
        {
            animation.speed = 1;
            coyoteTimeCounter = coyoteTime;
            animation.SetInteger("State", 8);
        }

        //Sprite Walking
        if (IsGrounded() && horizontal != 0 && alive && !inRope)
        {
            animation.speed = 1;
            coyoteTimeCounter = coyoteTime;
            animation.SetInteger("State", 1);
        }

        //Sprite Jumping
        if (!IsGrounded() && !IsWalled() && alive && !inRope)
        {
            animation.speed = 1;
            coyoteTimeCounter -= Time.deltaTime;
            coyoteTimeCounterWall -= Time.deltaTime;
            animation.SetInteger("State", 2);
        }

        //Sprite Walled
        if (!IsGrounded() && IsWalled() && horizontal != 0 && alive && !inRope)
        {
            animation.speed = 1;
            coyoteTimeCounter -= Time.deltaTime;
            coyoteTimeCounterWall = coyoteTimeWall;
            animation.SetInteger("State", 3);
        }

        //Sprites Muerte
        if (!alive && IsGrounded()) {
            animation.speed = 1;
            animation.SetInteger("State", 6);
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
        if (!alive && !IsGrounded())
        {
            animation.speed = 1;
            animation.SetInteger("State", 7);
            rb.velocity = new Vector2(0, rb.velocity.y);

        }

        //Sprite Climbing
        if (alive && inRope && !IsGrounded() && !IsWalled())
        {
            coyoteTimeCounter = 0;
            animation.SetInteger("State", 9);

            if (vertical >= 0.2 || vertical <= -0.2)
            {
                animation.speed = 1;
            }
            if(vertical <= 0.2 && vertical >= -0.2) {
                animation.speed = 0;
            }
        }



        if (isAttacking == false && alive) {
            Jump();
            //HighJump();
            WallSlide();
            WallJump();
            RopeJump();
            ConsumeFood();
            //DebugFillStamina();
            Golpe();
            Rope();
            RopeShoot();
            Sweat();
        }

        /* if (Input.GetButtonDown("Fire3") && stamina.GetCurrentStamina() >= stamina.GetDashStaminaCost()) {
             stamina.DashStaminaLos();
             StartCoroutine(Dash());
         }*/

        if (!isWallJumping && isAttacking == false && alive && !inRope)
        {
            Flip();
        }
    }


    private void Sweat()
    {
        if (stamina.GetCurrentStamina() == 0 && alive) {
            sweat.SetActive(true);
        }
        if (stamina.GetCurrentStamina() > 0 && alive) {
            sweat.SetActive(false);
        }
    }

    //Climbing Rope Management
    public void Rope() {
        if (vertical >= 0.5f && ropesHashSet.Count > 0 && !inRope && !isRopeJumping) {
            inRope = true;
            rb.gravityScale = 0;
            gameObject.transform.position= new Vector3(lastRopeRail.x, transform.position.y, transform.position.z);
        }

        if (inRope && vertical >= 0.2f && !isRopeJumping) {

            if (stamina.GetCurrentStamina() > 0)
            {
                rb.velocity = new Vector2(0, vertical * speedClimb);
            }
            else {
                rb.velocity = new Vector2(0, vertical * speedClimbNoStamina);
            }

        }

        if (inRope && vertical < 0.2 && vertical > -0.2 && !isRopeJumping) {
            rb.velocity = new Vector2(0,0);
        }

        if (inRope && vertical <= -0.2f && !isRopeJumping)
        {
            rb.velocity = new Vector2(0, vertical * speedClimb);
        }

        if (ropesHashSet.Count == 0 || IsGrounded()) {
            rb.gravityScale = originalGravityScale;
            inRope = false;
        }
    }


    private void RopeShoot()
    {
        if (Input.GetButtonDown("Fire2") && readyToShootArrow) {

            GameObject ropeFlyInstance = Instantiate(ropeFly, gameObject.transform.position, Quaternion.identity);
            ropeFlyInstance.GetComponent<Rope_Fly>().StartMovement(ropeDisplayer.GetComponent<RopeDisplayer>().hitPosition);
        }
    }
    public void isOnWater(bool w) {
        inWater = w;
    }
    public void attackingTrue() {
        isAttacking = true;
    }
    public void attackingFalse() {
        isAttacking = false;
    }

    private void FixedUpdate()
    {
       /* if (isDashing)
        {
            return;
        }*/
        if (alive) {
            Walk();
        }

        //Run();
    }

    /*private IEnumerator Dash() {
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

    }*/

    private void Walk() {
        if (!isWallJumping && !IsCrouching() && isAttacking == false && !inRope /*&& !IsRuning()*/)
        {
            //animation.SetInteger("State", 1);
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        }
        if (IsCrouching() || isAttacking == true)
        {

            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void SpawnSmokeJump() {
       GameObject smk = Instantiate(smokeJump, gameObject.transform.position, Quaternion.identity);
        Vector3 smkLocalScale = smk.transform.localScale ;
        if (  gameObject.transform.localScale.x > 0) {
            smkLocalScale.x = smkLocalScale.x * -1;
            smk.transform.localScale = smkLocalScale;
        }
    }

    public void SpawnSmokeLand()
    {
        GameObject smk = Instantiate(smokeLand, gameObject.transform.position, Quaternion.identity);
        Vector3 smkLocalScale = smk.transform.localScale;
        if (gameObject.transform.localScale.x > 0)
        {
            smkLocalScale.x = smkLocalScale.x * -1;
            smk.transform.localScale = smkLocalScale;
        }
    }

    private void Jump() {

        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0 && !IsCrouching())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            SpawnSmokeJump();
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            coyoteTimeCounter = 0;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (rb.velocity.y < -15) {
            rb.velocity = new Vector2(rb.velocity.x , -15);
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

    /*private void HighJump() {
        if (Input.GetButtonDown("Jump") && IsCrouching() && stamina.GetCurrentStamina() >= stamina.GetHighJumpStaminaCost())
        {

            stamina.HighJumpStaminaLoss();
            rb.velocity = new Vector2(rb.velocity.x, highJumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }*/

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(0.35f, 0.1f), 0f, groundLayer);
    }

    private void Golpe()
    {

        if (Input.GetButtonDown("Fire2") && stamina.GetCurrentStamina() >= stamina.GetAttackCost() && IsGrounded() && !readyToShootArrow)
        {
            animation.SetInteger("State", 4);
        }
    }

    public void checkFallingDeath() {
        //Debug.Log(lastYVelocity);
        if (lastYVelocity < -30 && !inWater && !IsWalled()) {
            Death();
        }
    }

    public void Death() {
        alive = false;
        Invoke("TransitionOn", 1f);
        Invoke("ReloadLevel", 1.8f);
    }

    public void TransitionOn (){
        transition.SetInteger("Transition", 1);

    }

    private void ReloadLevel() {
        Application.LoadLevel(Application.loadedLevel);
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
    

    public bool IsCrouching(){

        
        if (vertical < -0.5 && IsGrounded())
        {
            animation.SetInteger("State", 5);

            return true;
        }
        else {
            if (vertical >= -0.5) { 
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
    public void RopeJump()
    {
        if (inRope) {
            isRopeJumping = false;
            wallJumpingCounter = wallJumpingTime;
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f && stamina.GetCurrentStamina() >= stamina.GetWallJumpStaminaCost() && inRope)
        {
            isRopeJumping = true;
            inRope = false;
            rb.gravityScale = originalGravityScale;

            if(horizontal >= 0.3 || horizontal <= -0.3) {
                stamina.WallJumpStaminaLoss();
                rb.velocity = new Vector2(Mathf.Sign(horizontal) * ropeJumpingPower.x, ropeJumpingPower.y);
            }

            wallJumpingCounter = 0f;

            if (transform.localScale.x != Mathf.Sign(horizontal))
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopRopeJumping), wallJumpingDuration);
        }
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f && stamina.GetCurrentStamina() < stamina.GetWallJumpStaminaCost() && inRope)
        {
            isRopeJumping = true;
            inRope = false;
            rb.gravityScale = originalGravityScale;
            stamina.WallJumpStaminaLoss();
            rb.velocity = new Vector2(Mathf.Sign(horizontal) * ropeJumpingPowerNoStamina.x, ropeJumpingPowerNoStamina.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != Mathf.Sign(horizontal))
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopRopeJumping), wallJumpingDuration);
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

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f && stamina.GetCurrentStamina() >= stamina.GetWallJumpStaminaCost() && coyoteTimeCounterWall > 0)
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
    private void StopRopeJumping()
    {
        isRopeJumping = false;
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
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.layer == 8 || collision.gameObject.tag == "Spikes" ) {
            Death();
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.layer == 3) {
            checkFallingDeath();
            lastYVelocity = 0;
            SpawnSmokeJump();
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rope" || collision.gameObject.layer == 10)
        {
            ropesHashSet.Add(collision.gameObject);
            lastRopeRail = collision.transform.GetChild(0).transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rope" || collision.gameObject.layer == 10)
        {
            ropesHashSet.Remove(collision.gameObject);
            
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

