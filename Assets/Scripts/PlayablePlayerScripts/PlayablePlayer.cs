using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired;

[RequireComponent(typeof(Controller2D))]
public class PlayablePlayer : MonoBehaviour
{
    public enum Direction
    {
        Stationary, Left, Right, Up
    }
    public enum CharacterState
    {
        Idle, WalkLeft, WalkRight, Running, Jumping, Shooting, Falling, WallSlide, WallJump
    }

    public CharacterState state;
    public CharacterState lastState;
    public Direction moveDirection;

    public float runSpeed = 1.82f;
    public bool canWallJump = false;
    public bool lookRight = true;
    public List<GameObject> listOfStuff = new List<GameObject>();

    public float moveSpeed = 6f;
    public Vector2 wallJumpClimb, wallJumpOff, wallJumpLeap;
    private float targetVelX;

    public float wallSlidingSpeedMax = 3f;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;

    public float maxJumpVelocity;
    public float minJumpVelocity;
    public float gravity;

    [Range(0.1f,10f)]
    public float gravityFactor = 1.1f;

    public Vector3 velocity;

    [HideInInspector]
    public Vector2 input;
    float velocityXSmoothing;
    Controller2D controller;

    tk2dSpriteAnimator anim;
    //[HideInInspector]
    //public Animator anim;

    Player InputManager;


    void Start()
    {
        InputManager = ReInput.players.GetPlayer(0);
        anim = GetComponent<tk2dSpriteAnimator>();
        Screen.SetResolution(1024, 768, false);
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        //anim = GetComponent<Animator>();
        controller = GetComponent<Controller2D>();
        gravity = -(gravityFactor * maxJumpHeight) / Mathf.Pow(timeToJumpApex, gravityFactor);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        //print("Gravity: " + gravity + " Jump Velocity " + maxJumpVelocity);

        state = CharacterState.Idle;
    }

    public void RespawnAt(Transform spawnPos)
    {
        transform.position = spawnPos.position;
    }
    int wallDirX;

    void Update()
    {
        //determine direction
        //handle input
        //update motor
        
        //determine state

        DetermineMoveDirection();

        input = new Vector2(InputManager.GetAxisRaw("Move Horizontal"), InputManager.GetAxisRaw("Look"));
        
        targetVelX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            //state = CharacterState.Idle;
            lastState = state;
        }

        //if (controller.collisions.below == false)
        //{
        //    state = CharacterState.Jumping;
        //}
        //else
        //    state = CharacterState.Idle;

        DetermineCurrentState();
        //process state
        ProcessCurrentState();
        print(state);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, input);
        //print(velocity.y);
        #region bigblock
        ///Settings value dont touch
        //gravity = -(gravityFactor * maxJumpHeight) / Mathf.Pow(timeToJumpApex, gravityFactor);
        //maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        //minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        ///


        //int wallDirX = (controller.collisions.left) ? -1 : 1;

        //targetVelX = input.x * moveSpeed;
        //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        //bool wallSliding = false;

        //if (canWallJump)
        //{
        //    
        //}

        //if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        //    {
        //        wallSliding = true;

        //        if (velocity.y < -wallSlidingSpeedMax)
        //        {
        //            velocity.y = -wallSlidingSpeedMax;
        //        }

        //        if (timeToWallUnstick > 0)
        //        {
        //            velocityXSmoothing = 0;
        //            velocity.x = 0;
        //            if (input.x != wallDirX && input.x != 0)
        //            {
        //                timeToWallUnstick -= Time.deltaTime;
        //            }
        //            else
        //            {
        //                timeToWallUnstick = wallStickTime;
        //            }
        //        }
        //        else
        //        {
        //            timeToWallUnstick = wallStickTime;
        //        }
        //    }


        //if (player.GetButtonDown("Jump"))
        //{
        //    if (wallSliding)
        //    {
        //        if(wallDirX == input.x)
        //        {
        //            velocity.x = -wallDirX * wallJumpClimb.x;
        //            velocity.y = wallJumpClimb.y;
        //        }
        //        else if(input.x == 0)
        //        {
        //            velocity.x = -wallDirX * wallJumpOff.x;
        //            velocity.y = wallJumpOff.y;
        //        }
        //        else
        //        {
        //            velocity.x = -wallDirX * wallJumpLeap.x;
        //            velocity.y = wallJumpLeap.y;
        //        }
        //    }
        //}



        //}

        //if (input.x != 0)
        //{

        //    lookRight = (input.x > 0 ? true : false);
        //    transform.localScale = new Vector3(input.x > 0 ? 1 : -1, 1, 1);
        //    //transform.localScale = new Vector3(wallSliding && wallDirX > 0 ? -1 : 1, 1, 1);

        //    //play walk animation
        //    //if (controller.collisions.below && !player.GetButton("Run"))
        //    //{
        //    //    anim.SetInteger("AnimState", 1);
        //    //} //play run animation
        //    //else if (controller.collisions.below && player.GetButton("Run"))
        //    //{
        //    //    anim.SetInteger("AnimState", 4);
        //    //}
        //    ////running shoot
        //    //if (player.GetButtonDown("Shoot") && player.GetButton("Run") && controller.canShootStraight)
        //    //{
        //    //    anim.SetInteger("AnimState", 5);
        //    //}

        //    //if (player.GetButtonDown("Shoot") && !player.GetButton("Run") && controller.canShootStraight)
        //    //{
        //    //    anim.SetInteger("AnimState", 2);
        //    //}
        //    //if (player.GetButtonDown("Shoot") && !player.GetButton("Run") && controller.canShootUp)
        //    //{
        //    //    anim.SetInteger("AnimState", 3);
        //    //}

        //}


        #region commentedOutCode
        //else
        //{
        //    //play idle animation
        //    anim.SetInteger("AnimState", 0);
        //}
        //idle shoot
        //if (player.GetButtonDown("Shoot"))
        //{
        //    anim.SetInteger("AnimState", 11);
        //}

        //walking shoot


        //anim.SetBool("IsJumping", !controller.collisions.below);
        //if (!controller.collisions.below)
        //{
        //    anim.SetInteger("AnimState", 7);
        //    if (player.GetButtonDown("Shoot") && controller.canShootStraight)
        //    {
        //        anim.SetInteger("AnimState", 8);
        //    }
        //    if(player.GetButtonDown("Shoot") && controller.canShootUp)
        //    {
        //        anim.SetInteger("AnimState", 9);
        //    }

        //    anim.SetBool("IsWallHolding", wallSliding);
        //}

        /*if(Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

        if(input.x != 0 )
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                lookRight = (input.x > 0 ? true : false);
                transform.localScale = new Vector3(input.x > 0 ? 1 : -1, 1, 1);
            }
            if(controller.collisions.below)
            {
                anim.SetInteger("AnimState", 1);
            }
            else if (!controller.collisions.below && (velocity.y > 0 || velocity.y < 0))
            {
                anim.SetInteger("AnimState", 2);
            }
        }

        else
        {
            anim.SetInteger("AnimState", 0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetInteger("AnimState", 3);
        }*/
        #endregion

        //float targetVelocityX = input.x * moveSpeed;


        //if(!player.GetButton("Run"))
        //{

        //}
        //else if(player.GetButton("Run"))
        //{
        //    controller.Move(velocity * Time.deltaTime * runSpeed, input);
        //}
        #endregion
    }

    void UpdateMotor()
    {

        
    }
    
    void ApplyGravity()
    {

    }

    private void JumpShootRight()
    {
        if (anim.Sprite.FlipX == true)
            return;
        if (state != CharacterState.Falling &&
            state != CharacterState.Running &&
            state != CharacterState.WallJump &&
            state != CharacterState.WallSlide)
        {
            if ((lastState == CharacterState.Jumping &&
                    InputManager.GetButtonDown("Shoot") &&
                moveDirection == Direction.Right) ||
                ((lastState == CharacterState.Jumping &&
                    InputManager.GetButtonDown("Shoot") &&
                moveDirection == Direction.Stationary &&
                anim.Sprite.FlipX == false)))
            {
                anim.Play("jump_shoot_forward");
                ProjectilePool.Instance.ShootRight();
            }
        }
    }
    void ShootCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        
            state = CharacterState.Idle;
    }
    private void WalkShootRight()
    {
        
        if (state != CharacterState.Falling &&
            state != CharacterState.Jumping &&
            state != CharacterState.Running &&
            state != CharacterState.WallJump &&
            state != CharacterState.WallSlide )
        {
            if ((InputManager.GetButtonDown("Shoot") &&
                moveDirection == Direction.Right &&
                anim.Sprite.FlipX == false) ||
                (
                InputManager.GetButtonDown("Shoot") &&
                moveDirection == Direction.Stationary &&
                anim.Sprite.FlipX == false))
            {
                ProjectilePool.Instance.ShootRight();
                state = CharacterState.Shooting;
            }
        }
    }

    private void WalkShootLeft()
    {
        if (state != CharacterState.Falling &&
            state != CharacterState.Jumping &&
            state != CharacterState.Running &&
            state != CharacterState.WallJump &&
            state != CharacterState.WallSlide)
        {
            if ((
                InputManager.GetButtonDown("Shoot") &&
                moveDirection == Direction.Left &&
                anim.Sprite.FlipX == true) ||
                (
                InputManager.GetButtonDown("Shoot") &&
                moveDirection == Direction.Stationary &&
                anim.Sprite.FlipX == true))
            {
                state = CharacterState.Shooting;
                
                ProjectilePool.Instance.ShootLeft();
                state = CharacterState.Shooting;
            }
        }
    }

    private void JumpShootLeft()
    {
        if (anim.Sprite.FlipX == false)
            return;

        if (state != CharacterState.Falling &&
            state != CharacterState.Running &&
            state != CharacterState.WallJump &&
            state != CharacterState.WallSlide)
        {
            if ((lastState == CharacterState.Jumping &&
                    InputManager.GetButtonDown("Shoot") &&
                moveDirection == Direction.Left) ||
                ((lastState == CharacterState.Jumping &&
                    InputManager.GetButtonDown("Shoot") &&
                moveDirection == Direction.Stationary &&
                anim.Sprite.FlipX == true)))
            {
                ProjectilePool.Instance.ShootLeft();
            }
        }
    }

    private void JumpShootUp()
    {
        if (state != CharacterState.Falling &&
            state != CharacterState.Running &&
            state != CharacterState.WallJump &&
            state != CharacterState.WallSlide)
        {
            if (lastState == CharacterState.Jumping &&
                InputManager.GetButtonDown("Shoot") &&
                moveDirection == Direction.Up)
            {
                anim.Play("jump_shoot_up");
                ProjectilePool.Instance.ShootStuffUp();
            }
        }
    }

    void DetermineCurrentState()
    {

        //if (state != CharacterState.Falling &&
        //    state != CharacterState.Jumping &&
        //    state != CharacterState.Shooting &&
        //    state != CharacterState.WallJump &&
        //    state != CharacterState.WallSlide)
        //{
        //    switch (moveDirection)
        //    {
        //        case Direction.Stationary:
        //            break;
        //        case Direction.Left:
        //            break;
        //        case Direction.Right:
        //            break;
        //        default:
        //            break;
        //    }
        //}
        
        if(state != CharacterState.Shooting &&
            state != CharacterState.WalkLeft)
        {

        }

        WalkShootRight();
        WalkShootLeft();

        JumpShootRight();
        JumpShootLeft();
        JumpShootUp();

        

        if (state != CharacterState.Falling &&
            state != CharacterState.Running &&
            state != CharacterState.Shooting &&
            state != CharacterState.WallJump &&
            state != CharacterState.WallSlide &&
            state != CharacterState.Jumping)
        {
            switch (moveDirection)
            {
                case Direction.Stationary:
                    state = CharacterState.Idle;
                    break;
                case Direction.Left:
                    state = CharacterState.WalkLeft;
                    break;
                case Direction.Right:
                    state = CharacterState.WalkRight;
                    break;
                default:
                    break;
            }
        }

        if (state != CharacterState.Falling &&
            state != CharacterState.Running &&
            state != CharacterState.Shooting &&
            state != CharacterState.WallJump &&
            state != CharacterState.WallSlide )
        {
            if (InputManager.GetButtonDown("Jump"))
            {
                if (controller.collisions.below)
                {
                    state = CharacterState.Jumping;
                    lastState = state;
                }
            }
            if (InputManager.GetButtonUp("Jump"))
            {
                if (velocity.y > minJumpVelocity)
                {
                    velocity.y = minJumpVelocity;
                }
            }
        }
    }

    void Jumping()
    {
        //animtion ntot playing or we're grounded
        //have to be on the ground
        //jumping state and grounded still landed
        //still in the air
        if( (!anim.IsPlaying("Jumping") && controller.collisions.below) ||
            controller.collisions.below)
        {

        }
    }

    void ProcessCurrentState()
    {
        switch (state)
        {
            case CharacterState.Idle:
                if(!anim.IsPlaying("idle") && lastState == CharacterState.Jumping == false)
                    anim.Play("idle");
                break;
            case CharacterState.WalkLeft:
                if (
                    !anim.IsPlaying("walk") &&
                    lastState == CharacterState.Jumping == false)
                {
                    anim.Play("walk");
                    anim.Sprite.FlipX = true;
                }                 
                break;
            case CharacterState.WalkRight:
                if (
                    !anim.IsPlaying("walk") &&
                    lastState == CharacterState.Jumping == false)
                {
                    anim.Play("walk");
                    anim.Sprite.FlipX = false;
                }
                break;
            case CharacterState.Running:
                break;
            case CharacterState.Jumping:
                anim.Play("jump");
                if (input.x > 0)
                    anim.Sprite.FlipX = false;
                else if (input.x < 0)
                    anim.Sprite.FlipX = true;

                velocity.y = maxJumpVelocity;
                state = CharacterState.Idle;
                //state = CharacterState.Idle;
                break;
            case CharacterState.Shooting:
                if (!anim.IsPlaying("walk_shoot"))
                {
                    anim.Play("walk_shoot");
                    anim.AnimationCompleted = ShootCompleteDelegate;
                }
                //if (!anim.IsPlaying("walk_shoot") && state != CharacterState.WalkRight)
                //    anim.Play("walk_shoot");
                break;
            case CharacterState.Falling:
                break;
            case CharacterState.WallSlide:
                break;
            case CharacterState.WallJump:
                break;
            default:
                break;
        }
    }

    void DetermineMoveDirection()
    {
        bool left = false;
        bool right = false;
        bool up = false;

        if (input.x > 0)
            right = true;
        if (input.x < 0)
            left = true;
        if (input.y > 0)
        {
            up = true;
            print(up);
        }

        if (up)
            moveDirection = Direction.Up;
        else if (left)
            moveDirection = Direction.Left;
        else if (right)
            moveDirection = Direction.Right;
        else
            moveDirection = Direction.Stationary;
    }
}
