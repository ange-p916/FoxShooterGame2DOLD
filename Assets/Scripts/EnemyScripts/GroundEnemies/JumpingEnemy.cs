using UnityEngine;
using System.Collections;

public class JumpingEnemy : EnemyReqComp {

    public enum TheStates { IDLE, JUMP, LAND, SPIT }
    public enum JumpMachine { LEFT, RIGHT };
    public JumpMachine jumps;
    public TheStates states;

    public float jumpLeftTimer;
    public float jumpRightTimer;

    GenericShootingScript shoot;
    tk2dSpriteAnimator anim;

    [Header("jump variables")]
    public float countDownTimeToJump;
    public float newCDJump;
    public float timeIsJumping;
    public bool isJumping = false;

    public float jumpSpeedX;
    public float jumpSpeedY;
    
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<tk2dSpriteAnimator>();
        shoot = GetComponent<GenericShootingScript>();
    }


    protected override void Update()
    {
        base.Update();
        CheckLoS();
    }

    protected override void CheckLoS()
    {
        base.CheckLoS();
        if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, WhatIsGround) && targetInView)
        {
            StartJumpingInPlace();
        }

    }

    public void ShootDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if (states == TheStates.SPIT)
        {
            anim.Play("SpiderSpit");
        }
        else
        {
            anim.Play("SpiderIdle");
        }
    }

    public void JumpDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if (states == TheStates.JUMP)
        {
            anim.Play("SpiderJump");
        }
        else
        {
            anim.Play("SpiderIdle");
        }
    }

    public void LandDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if (states == TheStates.LAND)
        {
            anim.Play("SpiderLand");
        }
        else
        {
            anim.Play("SpiderIdle");
        }
    }

    //first we wait, then change state, so this is the state that it should change to.
    IEnumerator WaitWithDoingStuff(float timer, TheStates state)
    {
        yield return new WaitForSeconds(timer);
        states = state;
    }

    void StartJumpingInPlace()
    {
        if (distanceToPlayer <= startActionDistance * startActionDistance)
        {
            StartCoroutine(WaitWithDoingStuff(0.05f, TheStates.JUMP));
            StartCoroutine(WaitWithDoingStuff(countDownTimeToJump + .33f, TheStates.LAND));
            StartCoroutine(WaitWithDoingStuff(.33f, TheStates.SPIT));
            StartCoroutine(WaitWithDoingStuff(shoot.CDToShoot + 0.5f, TheStates.JUMP));
        }
        else
        {
            states = TheStates.IDLE;
        }

        if(states == TheStates.IDLE)
        {
            rb2d.velocity = Vector2.zero;
            if(!anim.IsPlaying("SpiderIdle"))
            {
                anim.Play("SpiderIdle");
                anim.AnimationCompleted = null;
            }
        }

        if (states == TheStates.JUMP)
        {
            if (jumpLeftTimer <= 0)
            {
                jumps = JumpMachine.RIGHT;
                jumpLeftTimer = 2f;
            }
            if (jumpRightTimer <= 0)
            {
                jumps = JumpMachine.LEFT;
                jumpRightTimer = 2f;
            }


            if (jumps == JumpMachine.LEFT)
            {
                JumpLeftOrRight(Vector2.left);
                jumpLeftTimer -= Time.deltaTime;
            }

            if (jumps == JumpMachine.RIGHT)
            {
                JumpLeftOrRight(Vector2.right);
                jumpRightTimer -= Time.deltaTime;
            }
        }

        if (states == TheStates.LAND)
        {
            if (!anim.IsPlaying("SpiderLand"))
            {
                anim.Play("SpiderLand");
                anim.AnimationCompleted = LandDelegate;
            }
        }

        if (states == TheStates.SPIT)
        {
            shoot.Shoot();
            if (!anim.IsPlaying("SpiderSpit") && shoot.isShooting)
            {
                anim.Play("SpiderSpit");
                anim.AnimationCompleted = ShootDelegate;
            }
        }

        var boxcasthit = Physics2D.BoxCast(transform.position, transform.localScale, 0, Vector2.zero, 1f, WhatIsPlayer);
        if(boxcasthit)
        {
            boxcasthit.collider.GetComponent<PlayerHealthController>().PlayerTakeDamage(1f, this.transform);
        }
    }

    void JumpLeftOrRight(Vector2 direction)
    {

        transform.localScale = (direction.x > 0) ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f);

        var grounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, WhatIsGround);
        if (grounded)
        {
            countDownTimeToJump -= Time.deltaTime;
        }
        if (countDownTimeToJump <= 0)
        {

            isJumping = true;
            timeIsJumping -= Time.deltaTime;

            if (isJumping)
            {
                rb2d.velocity = new Vector2(direction.x * jumpSpeedX, jumpSpeedY);

                if (!anim.IsPlaying("SpiderJump"))
                {
                    anim.Play("SpiderJump");
                    anim.AnimationCompleted = JumpDelegate;
                }
            }
            if (timeIsJumping <= 0)
            {
                isJumping = false;
                countDownTimeToJump = newCDJump;
            }

            if (!isJumping && countDownTimeToJump >= 0)
            {
                timeIsJumping = 0.1f;
            }
        }
    }
}
