using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired;

[RequireComponent(typeof(Controller2D))]
public class PlayablePlayer : MonoBehaviour
{
    public float runSpeed = 5f;
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

    float maxJumpVelocity;
    float minJumpVelocity;
    float gravity;

    [Range(0.1f,10f)]
    public float gravityFactor = 1.1f;

    public Vector3 velocity;

    [HideInInspector]
    public Vector2 input;
    float velocityXSmoothing;
    Controller2D controller;

    //[HideInInspector]
    //public Animator anim;

    Player player;

    void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    void Start()
    {
        //anim = GetComponent<Animator>();
        controller = GetComponent<Controller2D>();
        gravity = -(gravityFactor * maxJumpHeight) / Mathf.Pow(timeToJumpApex, gravityFactor);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        //print("Gravity: " + gravity + " Jump Velocity " + maxJumpVelocity);
    }

    public void RespawnAt(Transform spawnPos)
    {
        transform.position = spawnPos.position;
    }

    void Update()
    {
        input = new Vector2(player.GetAxisRaw("Move Horizontal"), 0);
        int wallDirX = (controller.collisions.left) ? -1 : 1;

        targetVelX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        bool wallSliding = false;

        if (canWallJump)
        {
            if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
            {
                wallSliding = true;

                if (velocity.y < -wallSlidingSpeedMax)
                {
                    velocity.y = -wallSlidingSpeedMax;
                }

                if (timeToWallUnstick > 0)
                {
                    velocityXSmoothing = 0;
                    velocity.x = 0;
                    if (input.x != wallDirX && input.x != 0)
                    {
                        timeToWallUnstick -= Time.deltaTime;
                    }
                    else
                    {
                        timeToWallUnstick = wallStickTime;
                    }
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
        }

        

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        /*
        *Jump button
        *Shot button
        *Input Hor- and Vertical
        */


        //input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        

        if (player.GetButtonDown("Jump"))
        {
            if (wallSliding)
            {
                if(wallDirX == input.x)
                {
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                }
                else if(input.x == 0)
                {
                    velocity.x = -wallDirX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                }
                else
                {
                    velocity.x = -wallDirX * wallJumpLeap.x;
                    velocity.y = wallJumpLeap.y;
                }
            }
            if(controller.collisions.below)
            {
                velocity.y = maxJumpVelocity;
            }
            
        }
        if (player.GetButtonUp("Jump"))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }

        }






        if (input.x != 0)
        {

            lookRight = (input.x > 0 ? true : false);
            transform.localScale = new Vector3(input.x > 0 ? 1 : -1, 1, 1);
            //transform.localScale = new Vector3(wallSliding && wallDirX > 0 ? -1 : 1, 1, 1);

            //play walk animation
            //if (controller.collisions.below && !player.GetButton("Run"))
            //{
            //    anim.SetInteger("AnimState", 1);
            //} //play run animation
            //else if (controller.collisions.below && player.GetButton("Run"))
            //{
            //    anim.SetInteger("AnimState", 4);
            //}
            ////running shoot
            //if (player.GetButtonDown("Shoot") && player.GetButton("Run") && controller.canShootStraight)
            //{
            //    anim.SetInteger("AnimState", 5);
            //}

            //if (player.GetButtonDown("Shoot") && !player.GetButton("Run") && controller.canShootStraight)
            //{
            //    anim.SetInteger("AnimState", 2);
            //}
            //if (player.GetButtonDown("Shoot") && !player.GetButton("Run") && controller.canShootUp)
            //{
            //    anim.SetInteger("AnimState", 3);
            //}

        }
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

        velocity.y += gravity * Time.deltaTime;
        if(!player.GetButton("Run"))
        {
            controller.Move(velocity * Time.deltaTime, input);
        }
        else if(player.GetButton("Run"))
        {
            controller.Move(velocity * Time.deltaTime * runSpeed, input);
        }
    }
}
