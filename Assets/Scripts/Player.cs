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

    [SerializeField]
    GameObject windowMenu;

    public bool alive = true;
    
    [SerializeField]
    private GameObject wallRockCheck;

    [SerializeField]
    public Animator transition;

    public float originalGravityScale = 5;
    public float horizontal;
    public float vertical;
    [SerializeField]
    private float speed = 0f;
    private float maxSpeed = 0f;
    public float aceleration;
    public float deceleration;
    public float speedGround = 6f;
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
    public bool isAttacking;


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
    private float coyoteTime = 0.1f;
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

    

    public bool inWater = false;
    public bool inRope = false;
    private Vector3 lastRopeRail;
    public HashSet<GameObject> ropesHashSet = new HashSet<GameObject>();
    public bool readyToShootArrow;
    public bool ShootArrowBreak = true;

    private bool groundedToggle = false;
    public bool gamePaused = false;
    private float lastDirection;

    private float fallingTime;
    private float lastFallingTime;
    private bool resetFallingTime;
    public float fallingTimeLimit;

    [SerializeField]
    public Image fallingBar;
    [SerializeField]
    public GameObject fallingBarParent;
    public Image fallingIcon;
    public Transform fallingA;
    public Transform fallingB;

    public float respawnPosX;
    public float respawnPosY;
    private GameObject respawnObject;

    [SerializeField]
    private GameObject rockPrefab;
    public GameObject rock;
    public Image fallingRockIcon;
    public GameObject fallingRockBreakIcon;
    public GameObject fallingPlayerIconStay;
    private float fallingTimeRock;
    private float lastFallingTimeRock;
    private bool resetFallingTimeRock;
    public float fallingTimeLimitRock;
    private bool fallingRockFlag;
    private Vector3 fallingRockBreakPosIcon;
    private bool fallingPlayerFlag;
    private Vector3 fallingPlayerStayPos;


    private void Start()
    {
        respawnPosX = transform.position.x;
        respawnPosY = transform.position.y;
    }

    private void Update()
    {

        fallingBar.fillAmount = lastFallingTime / fallingTimeLimit;
        fallingIcon.transform.position = Vector3.Lerp(fallingA.position, fallingB.position, lastFallingTime / fallingTimeLimit);
        Vector3 fallingRockIconPos = Vector3.Lerp(new Vector3(fallingA.position.x + 0.5f, fallingA.position.y, fallingA.position.z), new Vector3(fallingB.position.x + 0.5f, fallingB.position.y, fallingB.position.z), lastFallingTimeRock / fallingTimeLimitRock);
        fallingRockIcon.transform.position = fallingRockIconPos;
    
        if (lastFallingTime / fallingTimeLimit >= 1) {
            fallingBar.color = Color.red;
        }
        if (lastFallingTime / fallingTimeLimit < 1)
        {
            fallingBar.color = Color.white;
        }
        if (lastFallingTime / fallingTimeLimit > 0.2f)
        {
            fallingPlayerFlag = true;
            fallingPlayerStayPos = fallingIcon.transform.localPosition;
        }
        if (lastFallingTime / fallingTimeLimit < 0.2f)
        {
            if (fallingPlayerFlag)
            {
                GameObject playerIconStay = Instantiate(fallingPlayerIconStay, fallingPlayerStayPos, Quaternion.Euler(0, 0, 90));
                playerIconStay.transform.SetParent(fallingBarParent.transform, false);

                fallingPlayerFlag = false;
            }
            
        }



        //Falling Icon Rock
        if (lastFallingTimeRock / fallingTimeLimitRock > 0.2f) {
            fallingRockIcon.gameObject.SetActive(true);
            fallingRockFlag = true;
            fallingRockBreakPosIcon = fallingRockIcon.transform.localPosition;
        }


        if (lastFallingTimeRock / fallingTimeLimitRock < 0.2f)
        {
            if (fallingRockFlag) {
                GameObject rockFlag = Instantiate(fallingRockBreakIcon, fallingRockBreakPosIcon, Quaternion.Euler(0,0,90));
                rockFlag.transform.SetParent(fallingBarParent.transform, false);
                rockFlag.GetComponent<RockBreakIcon>().setAnimBool("Broken", true);
                fallingRockFlag = false;
            }
            fallingRockIcon.gameObject.SetActive(false);
        }



        //Debug.Log(inRope);
        if (!gamePaused)
        {
            
            //Rock Falling
            if (rock!=null && rock.GetComponent<Rigidbody2D>().velocity.y  < 1) {

                fallingTimeRock += Time.deltaTime;
                lastFallingTimeRock = fallingTimeRock;
            }

            if (rock != null && rock.GetComponent<Rigidbody2D>().velocity.y < -15)
            {
                rock.GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x, -15);
            }

            if (rock == null) {
                lastFallingTimeRock = 0;
                fallingTimeRock = 0;
            }


            //Player Falling

            if (rb.velocity.y < -1)
            {
                fallingTime += Time.deltaTime;
                lastFallingTime = fallingTime ;
            }


            if (!inWater)
            {
                maxSpeed = speedGround;
                jumpingPower = jumpingPowerGround;
            }
            else {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
                maxSpeed = speedWater;
                jumpingPower = jumpingPowerWater;
            }

            if (inWater && stamina.GetCurrentStamina() == 0 && !inRope && alive)
            {
                Death();
            }
            if (inWater) {
                lastFallingTime = 0;
                fallingTime = 0;
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
            if (IsGrounded() && horizontal == 0 && !IsCrouching() && alive && !inRope && !isAttacking && !gamePaused) {
                animation.speed = 1;
                coyoteTimeCounter = coyoteTime;
                animation.SetInteger("State", 0);
            }

            //Sprite Rope Ready
            if (IsGrounded() && (Input.GetAxisRaw("RopeState") > 0.8 || Input.GetButton("RopeStateKeyBoard")) && horizontal == 0 && !IsCrouching() && alive && ropesHashSet.Count == 0 && readyToShootArrow && !isAttacking && !gamePaused)
            {
                animation.speed = 1;
                coyoteTimeCounter = coyoteTime;
                animation.SetInteger("State", 8);
            }

            //Sprite Walking
            if (IsGrounded() && speed != 0 && alive && !inRope && !isAttacking && !gamePaused)
            {
                fallingTime = 0;
                lastFallingTime = 0;
                animation.speed = 1;
                coyoteTimeCounter = coyoteTime;
                animation.SetInteger("State", 1);
            }

            //Sprite Jumping
            if (!IsGrounded() && !IsWalled() && alive && !inRope && !isAttacking && !gamePaused)
            {
                animation.speed = 1;
                coyoteTimeCounter -= Time.deltaTime;
                coyoteTimeCounterWall -= Time.deltaTime;
                animation.SetInteger("State", 2);
            }

            //Sprite Walled
            if (!IsGrounded() && IsWalled() && horizontal != 0 && alive && !inRope && !isAttacking && !gamePaused)
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
            if (alive && inRope && !IsGrounded() && !IsWalled() && !isAttacking)
            {
                coyoteTimeCounter = 0;
                lastFallingTime = 0;
                fallingTime = 0;
                animation.SetInteger("State", 9);

                if (vertical >= 0.2 || vertical <= -0.2)
                {
                    lastFallingTime = 0;
                    fallingTime = 0;
                    animation.speed = 1;
                }
                if(vertical <= 0.2 && vertical >= -0.2) {
                    lastFallingTime = 0;
                    fallingTime = 0;
                    animation.speed = 0;
                }
            }

            //isGrounded Toggle

            if (!IsGrounded() && groundedToggle == false) {
                groundedToggle = true;
            }

            if (IsGrounded() && groundedToggle == true && rb.velocity.y <= 0) {
                //Spawn Smoke
                    SpawnSmokeJump();

                groundedToggle = false;        
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
                ThrowRock();
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
        if (isAttacking == false && alive)
        {
            pauseGame();
        }
    }

    public void SetRespawnPos(float x, float y, GameObject respawnObj)
    {
        if (respawnObject != null) {
            respawnObject.GetComponent<CheckPoint>().turnOffCheckPoint();
        }
        respawnPosX = x;
        respawnPosY = y;
        respawnObject = respawnObj;
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

    //Pause Game
    public void pauseGame(){
        //Toggles state
        if (Input.GetButtonDown("Pause")) {
            if (gamePaused)
            {
                windowMenu.GetComponent<MenuManager>().pauseMenuBack();
                
            }
            else {
                windowMenu.GetComponent<MenuManager>().pauseMenuOut();
                

            }
        }

        // freezes time


    }

    //Climbing Rope Management
    public void Rope()
    {



        if (vertical >= 0.5f && ropesHashSet.Count > 0 && !inRope && !isRopeJumping)
        {
            
            inRope = true;
            rb.gravityScale = 0;
            gameObject.transform.position = new Vector3(lastRopeRail.x, transform.position.y, transform.position.z);
        }

        if (inRope && vertical >= 0.2f && !isRopeJumping)
        {
            animation.speed = 1;
            if (stamina.GetCurrentStamina() > 0)
            {
                rb.velocity = new Vector2(0, vertical * speedClimb);
            }
            else
            {
                rb.velocity = new Vector2(0, vertical * speedClimbNoStamina);
            }
        }

        if ((inRope && vertical < 0.2 && vertical > -0.2 && !isRopeJumping) || (inRope && vertical <= -0.2f && !isRopeJumping && ropesHashSet.Count >= 0))
        {

            rb.velocity = new Vector2(0, 0);
            animation.speed = 0;
        }

        if (inRope && vertical <= -0.2f && !isRopeJumping && ropesHashSet.Count > 0)
        {

            rb.velocity = new Vector2(0, vertical * speedClimb);
            animation.speed = 1;
        }

        if (IsGrounded())
        {
            rb.gravityScale = originalGravityScale;
            inRope = false;
        }
    }


    private void RopeShoot()
    {
        if (Input.GetButtonDown("Fire2") && readyToShootArrow && !gamePaused && ShootArrowBreak) {

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
        animation.SetInteger("State", 0);
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
            //Debug.Log(speed + " = speed | speedGround = " + speedGround + " | horizontal " + horizontal);
            if ((speed<maxSpeed || speed > -maxSpeed) && (horizontal > 0.1f || horizontal < -0.1f)) {
                //Debug.Log("ACELERANDO");
                speed += aceleration * Time.deltaTime;
            }

            if (horizontal < 0.1f && horizontal > -0.1f && speed != 0) {
                //Debug.Log("FRENANDO");
                speed -= deceleration * Time.deltaTime;

                if (speed < 1f && speed > -1) {
                    speed = 0;
                }
            }




            if (speed > maxSpeed ) {
                speed = maxSpeed;
            }
            if (speed < -maxSpeed)
            {
                speed = -maxSpeed;
            }

            if ((horizontal > 0.1f || horizontal < -0.1f)) {
                rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);
                lastDirection = horizontal;
                if (IsWalled() && !IsWallRock())
                {
                    speed = 0;
                }
            }

            if ((horizontal < 0.1f || horizontal > -0.1f)) {
                rb.velocity = new Vector2(speed * lastDirection, rb.velocity.y);
            }


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

            if (speed < maxSpeed/2 && horizontal != 0) {
                speed = maxSpeed/2;
            }

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

        if (Input.GetButtonDown("Fire1") && !gamePaused) {
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
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(0.48f, 0.1f), 0f, groundLayer);
    }

    private void Golpe()
    {

        if (Input.GetButtonDown("Fire2") && stamina.GetCurrentStamina() >= stamina.GetAttackCost() && IsGrounded() && !readyToShootArrow && !gamePaused)
        {
            animation.SetInteger("State", 4);
        }
    }

    public bool checkFallingDeath() {
        
        /*if (lastFallingTime < -30 && !inWater && !IsWalled()) {
            Death();
        }*/
        if (lastFallingTime > fallingTimeLimit) {
            //Debug.Log("Too High");
            return true;
        }
        else {
            return false;
        }

    }

    public void Death() {
        alive = false;
        Invoke("TransitionOn", 1f);
        Invoke("Respawn", 1.8f);
    }

    public void TransitionOn (){
        transition.SetInteger("Transition", 1);
    }
    public void TransitionOff()
    {
        transition.SetInteger("Transition", 2);
    }

    private void Respawn() {
        transform.position = new Vector2(respawnPosX,respawnPosY);
        alive = true;
        lastFallingTime = 0;
        fallingTime = 0;
        inWater = false;
        animation.speed = 1;
        coyoteTimeCounter = coyoteTime;
        animation.SetInteger("State", 0);
        TransitionOff();
        //Application.LoadLevel(Application.loadedLevel);
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
        isAttacking = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawCube(groundCheck.position, new Vector2(0.48f, 0.1f)); 
        
        Gizmos.color = new Color(1, 0, 1, 0.75F);
        Gizmos.DrawSphere(wallCheck.position, 0.2f);

        Gizmos.color = new Color(1, 1, 1, 0.75F);
        Gizmos.DrawSphere(attackPosition.position, radioGolpe);
    }
    

    public bool IsCrouching(){

        
        if (vertical < -0.5 && IsGrounded())
        {
            animation.SetInteger("State", 5);
            speed = 0;
            return true;
        }
        else {
            return false;
        }
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        
    }

    private bool IsWallRock(){
       Collider2D col = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        if (col != null)
        {
            //Debug.Log("ROCK DETECTED = " + col.CompareTag("Rock"));
            return col.CompareTag("Rock");
        }
        else {
            return false;
        }
        
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            fallingTime = 0;
            lastFallingTime = 0;
        }
        else
        {
            isWallSliding = false;
        }
    }
    public void RopeJump()
    {
        if (inRope)
        {
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

            if (horizontal >= 0.3 || horizontal <= -0.3)
            {
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
            //stamina.WallJumpStaminaLoss();
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

        if (Input.GetButtonDown("Fire2") && inRope)
        {
            //Debug.Log("CAE");
            inRope = false;
            rb.gravityScale = originalGravityScale;
            rb.velocity = Vector2.zero;
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
            if (horizontal != 0) {
                speed = maxSpeed;
            }

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
            speed = maxSpeed * 0.1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.layer == 8 || collision.gameObject.tag == "Spikes" ) {
            Death();
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.layer == 3 || collision.gameObject.tag == "OneWayPlatform")
        {
            //Debug.Log(lastFallingTime);
            if (checkFallingDeath())
            {
                Death();
            }
            else {
                fallingTime = 0;
                lastFallingTime = 0;
            }
            
            //SpawnSmokeJump();
        }

        
    }

    public void ThrowRock() {
        if (Input.GetButtonDown("Fire3") && rock == null) {
             rock = Instantiate(rockPrefab, new Vector3(groundCheck.transform.position.x + (0.5f * gameObject.transform.localScale.x), groundCheck.transform.position.y , groundCheck.transform.position.z), Quaternion.identity);
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

