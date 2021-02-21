using UnityEngine;
using System.Collections.Generic;

public class FlyingBomberEnemy : EnemyReqComp {

    tk2dSpriteAnimator anim;
    //Animator anim;
    public float keepAwayDist;
    public float radiusndist;
    public float bounce;
    public float flyingSpeed;
    public float keepAwayFactorX, keepAwayFactorY;
    public float flyAwayValue;
    public Transform shootPoint;
    GenericShootingScript shoot;
    public float radiusndistanceForDistGround;

    bool isIdling, isFlying, isBombing;

    protected override void Start()
    {
        base.Start();
        //anim = GetComponent<Animator>();
        shoot = GetComponent<GenericShootingScript>();
        anim = GetComponent<tk2dSpriteAnimator>();
    }

    protected override void Update()
    {
        base.Update();
        if (distanceToPlayer <= startActionDistance * startActionDistance)
        {
            CheckLoS();
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    protected override void CheckLoS()
    {
        base.CheckLoS();
        if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, WhatIsGround) && targetInView)
        {
            FlyAndBomb();
        }
    }

    void ShootCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if(isFlying)
        {
            anim.Play("FlyingBomberFly");
        }
        else
        {
            anim.Play("FlyingBomberIdle");
        }
    }

    public void FlyAndBomb()
    {
        var isPlayerUpOrDown = (player.transform.position - transform.position).y > 0 ? Vector2.up : Vector2.down;
        var rayToPlayerHit = Physics2D.CircleCast(transform.position, radiusndist, Vector2.zero, radiusndist, WhatIsPlayer);
        //Linecast(this.transform.position, player.transform.position, WhatIsPlayer);
        //Raycast(transform.position, whatSideIsPlayerAt, startActionDistance, WhatIsPlayer);
        
        bounce = rayToPlayerHit.distance - keepAwayDist;
        var bounceVec = new Vector2(bounce * keepAwayFactorX, bounce * keepAwayFactorY);

        //Debug.DrawLine(transform.position, player.transform.position, Color.green);

        if(rayToPlayerHit)
        {
            rb2d.velocity = new Vector2((player.transform.position - this.transform.position).x,
                                        (player.transform.position - this.transform.position).y) * flyingSpeed + bounceVec;

            if((player.transform.position - this.transform.position).x > 0)
            {
                rb2d.velocity = Vector2.right * flyingSpeed;
                keepAwayFactorX += flyAwayValue * Time.deltaTime;
                //transform.eulerAngles += new Vector3(0, 0, 10f);
                
            }
            else if((player.transform.position - this.transform.position).x < 0)
            {
                rb2d.velocity = Vector2.left * flyingSpeed;
                keepAwayFactorX -= flyAwayValue * Time.deltaTime;
                //transform.eulerAngles -= new Vector3(0, 0, 10f);
            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -flyingSpeed, flyingSpeed), Mathf.Clamp(rb2d.velocity.y, -flyingSpeed, flyingSpeed));
            //transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(transform.eulerAngles.z, 45f, 315f));

            //play shootin animations
            if (shoot.CDToShoot <= 1f)
            {
                //anim.SetInteger("AnimState", 1);
                if (!anim.IsPlaying("FlyingBomberShoot"))
                {
                    anim.Play("FlyingBomberShoot");
                    anim.AnimationCompleted = ShootCompleteDelegate;
                    isFlying = false;
                    isBombing = true;
                }
            }
            else
            {
                //anim.SetInteger("AnimState", 0);
                if(!anim.IsPlaying("FlyingBomberIdle"))
                {
                    anim.Play("FlyingBomberIdle");
                    isIdling = true;
                }
            }
            shoot.shootingPoint = shootPoint;
            shoot.Shoot();
        }
    }
}
