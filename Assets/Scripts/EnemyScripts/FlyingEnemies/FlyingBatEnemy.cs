using UnityEngine;
using System.Collections;

public class FlyingBatEnemy : EnemyReqComp {
    
    public Transform restingSpot;

    public float flySpeed = 5f;

    public float chasingTimer = 5f;
    public float newCDChasingTimer;

    public bool startToChase;

    bool isBiting, isDiving, isFlying, isIdling;

    tk2dSpriteAnimator anim;

    protected override void Start()
    {
        base.Start();
        //anim = GetComponent<Animator>();
        anim = GetComponent<tk2dSpriteAnimator>();
    }

    void OnEnable()
    {
        startToChase = false;
    }

    protected override void Update()
    {
        base.Update();
        StartToFly();
        
    }

    //public override void TakeDamage(float damage)
    //{
    //    base.TakeDamage(damage);
    //    startToChase = true;
    //    chasingTimer = newCDChasingTimer;

    //}


    protected override void CheckLoS()
    {
        base.CheckLoS();
        if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, WhatIsGround) && targetInView)
        {
            startToChase = true;
        }
        else
        {
            startToChase = false;
        }
    }

    void BiteCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if(isFlying)
        {
            anim.Play("BatFly");
        }
        else
        {
            anim.Play("BatIdle");
        }
    }

    void StartToFly()
    {
        //if player is within distance, start chasing
        if (distanceToPlayer <= startActionDistance * startActionDistance)
        {
            CheckLoS();
            chasingTimer -= Time.deltaTime;
            if (chasingTimer <= 0 || CheckpointManager.Instance.isDead)
            {
                startToChase = false;
            }
        }

        //do some fancy code so they spread out

        var atkHit = Physics2D.CircleCast(transform.position, 0.29f, Vector2.zero, 0.29f, WhatIsPlayer);
        if (atkHit)
        {
            StartCoroutine(HoldUp());
            if(!anim.IsPlaying("BatBite"))
            {
                anim.Play("BatBite");
                anim.AnimationCompleted = BiteCompleteDelegate;
                isFlying = false;
                isBiting = true;
            }
            atkHit.collider.GetComponent<PlayerHealthController>().PlayerTakeDamage(1f, this.transform);
            startToChase = true;
            //chasingTimer = 0f;
            chasingTimer = newCDChasingTimer;

        }

        if (startToChase)
        {
            ChaseOrRetreat();
        }

        if (!startToChase)
        {
            ChaseOrRetreat();
        }
        if (this.transform.position == restingSpot.position)
        {
            chasingTimer = newCDChasingTimer;
            //anim.SetInteger("AnimState", 0);
            if(!anim.IsPlaying("BatIdle"))
            {
                anim.Play("BatIdle");
                anim.AnimationCompleted = null;
                isFlying = false;
                isIdling = true;
            }
            
        }
    }

    IEnumerator HoldUp()
    {
        flySpeed = 0f;
        yield return new WaitForSeconds(1f);
        flySpeed = 3f;
    }

    bool ChaseOrRetreat()
    {
        if (startToChase)
        {
            //var push = (Mathf.Sin((Time.time + timingOffset) * timeWaveVar)) * height;
            transform.localScale = new Vector3(whatSideIsPlayerAt.x, 1, 1);
            //if (anim != null)
            //{
            //    anim.SetInteger("AnimState", 1);
            //}
            if(!anim.IsPlaying("BatFly"))
            {
                anim.Play("BatFly");
                isIdling = false;
                isFlying = true;
            }
            rb2d.velocity = new Vector2((player.transform.position - this.transform.position).x,
                (player.transform.position - this.transform.position).y).normalized * flySpeed;
        }
        if (!startToChase)
        {
            //if (anim != null)
            //{
            //    anim.SetInteger("AnimState", 1);
            //}
            transform.localScale = new Vector3(whatSideIsPlayerAt.x, 1, 1);
            rb2d.velocity = Vector2.zero;
            transform.position = Vector3.MoveTowards(transform.position, restingSpot.position,
                5 * Time.deltaTime);
        }
        return startToChase;
    }

}
