using UnityEngine;
using System.Collections;

public class ZombieSpewerEnemy : EnemyReqComp {

    public enum theStates { APPEAR, IDLE, SHOOT}
    public theStates states;
    public bool hasAppeared;
    public float speed;
    tk2dSpriteAnimator anim;

    GenericShootingScript shoot;

    protected override void Start()
    {
        base.Start();
        shoot = GetComponent<GenericShootingScript>();
        anim = GetComponent<tk2dSpriteAnimator>();
    }

    protected override void Update()
    {
        base.Update();
       
        CheckLoS();
    }

    public void ShootDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if(states == theStates.SHOOT)
        {
            anim.Play("ZombieSpit");
        }
        else
        {
            anim.Play("ZombieIdle");
        }
    }

    void DoZombieStuff()
    {
        transform.localScale = whatSideIsPlayerAt.x > 0 ? Vector3.one : new Vector3(-1, 1, 1);

        if(distanceToPlayer <= startActionDistance * startActionDistance && !hasAppeared)
        {
            states = theStates.APPEAR;
            hasAppeared = true;
            StartCoroutine(WaitWithShooting());
        }

        if (states == theStates.IDLE)
        {
            rb2d.velocity = Vector2.zero;
            if (!anim.IsPlaying("ZombieIdle"))
            {
                anim.Play("ZombieIdle");
                anim.AnimationCompleted = null;
            }
        }
        if (states == theStates.APPEAR)
        {
            rb2d.velocity = Vector2.zero;
            if(!anim.IsPlaying("ZombieAppear"))
            {
                anim.Play("ZombieAppear");
                anim.AnimationCompleted = ShootDelegate;
            }
        }
        if (states == theStates.SHOOT)
        {
            shoot.Shoot();
            if (!anim.IsPlaying("ZombieSpit") && shoot.isShooting)
            {
                anim.Play("ZombieSpit");
            }
           
        }
    }

    protected override void CheckLoS()
    {
        base.CheckLoS();
        if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, WhatIsGround) && targetInView)
        {
            DoZombieStuff();
        }
    }

    IEnumerator WaitWithShooting()
    {
        yield return new WaitForSeconds(0.83f);
        states = theStates.SHOOT;
    }

}
