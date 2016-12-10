using UnityEngine;
using System.Collections;

public class GenericExploderEnemyScript : EnemyReqComp {

    public enum theStates { IDLE, RUN, EXPLODE }
    public theStates states;

    tk2dSpriteAnimator anim;

    bool hasExploded;

    [Header("Run Timers")]
    public float newCDRun;
    public float countdownTimeToRun;
    public float timeIsRunning;
    public float newCDRunTimer;

    [Header("Explosion Timers")]
    public float countdownTimeToExplode;
    public float timeIsExploding;
    public float newCDToExplode;
    public float newCDTimeIsExploding;
    public float cooldowntoexplode = 2f;

    [Header("Booleans")]
    public bool countDownStart = false;
    public bool isRunning = false;
    public bool isExploding = false;

    [Header("Distances")]
    public float dstToPlayerBeforeExplosion;

    [Header("Speeds")]
    public float runSpeed;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<tk2dSpriteAnimator>();
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
            //RunAndExplode();
            RunAndExplodeNew();
        }
    }

    void ExplodeDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if(isRunning)
        {
            anim.Play("ExploderRun");
        }
        else
        {
            anim.Play("ExploderIdle");
        }
    }

    public void RunAndExplodeNew()
    {
        var circlecast = Physics2D.CircleCast(transform.position, 3f, Vector2.zero, 3f, WhatIsPlayer);

        if (distanceToPlayer <= startActionDistance * startActionDistance)
        {
            states = theStates.RUN;
        }
        else
        {
            states = theStates.IDLE;
        }
        if (circlecast)
        {
            states = theStates.EXPLODE;
        }
        


        if(states == theStates.IDLE)
        {
            rb2d.velocity = Vector2.zero;
            if (!anim.IsPlaying("ExploderIdle"))
            {
                anim.Play("ExploderIdle");
                anim.AnimationCompleted = null;
            }
        }
        if(states == theStates.RUN)
        {
            rb2d.velocity = new Vector2(whatSideIsPlayerAt.x, rb2d.velocity.y) * runSpeed;
            if(!anim.IsPlaying("ExploderRun"))
            {
                anim.Play("ExploderRun");
                anim.AnimationCompleted = null;
            }
        }

        if(states == theStates.EXPLODE)
        {
            rb2d.velocity = Vector2.zero;

            if (!anim.IsPlaying("ExploderExplode"))
            {
                anim.Play("ExploderExplode");
                anim.AnimationCompleted = ExplodeDelegate;
            }

            var boxhit = Physics2D.BoxCast(transform.position, transform.localScale, 0, Vector2.zero, transform.localScale.x, WhatIsPlayer);
            if(boxhit)
            {
                boxhit.collider.gameObject.GetComponent<PlayerHealthController>().PlayerTakeDamage(1f, this.transform);
            }
            StartCoroutine(Explodeffs());
        }
    }

    //public void RunAndExplode()
    //{
    //    var rayLength = Mathf.Abs(rb2d.velocity.x);

    //    var hit = Physics2D.Raycast(transform.position, whatSideIsPlayerAt, rayLength, WhatIsPlayer);
    //    countdownTimeToRun -= Time.deltaTime;
    //    if (countdownTimeToRun <= 0)
    //    {
    //        isRunning = true;
    //        timeIsRunning -= Time.deltaTime;
    //        if (isRunning)
    //        {
    //            if(!anim.IsPlaying("ExploderRun"))
    //            {
    //                anim.Play("ExploderRun");
    //                anim.AnimationCompleted = null;
    //                isRunning = true;
    //            }
    //            transform.localScale = new Vector3(whatSideIsPlayerAt.x * 1f, 1f, 1f);
    //            rb2d.velocity = whatSideIsPlayerAt * runSpeed;
    //        }
    //        if (timeIsRunning <= 0)
    //        {
    //            isRunning = false;
    //            countdownTimeToRun = newCDRun;
    //        }
    //        if (!isRunning && countdownTimeToRun >= 0)
    //        {
    //            timeIsRunning = newCDRunTimer;
    //        }
    //    }

    //    if(hit)
    //    {
    //        isRunning = false;
    //        rb2d.velocity = new Vector2(hit.distance - dstToPlayerBeforeExplosion, 0);
    //        rayLength = hit.distance;
    //        countdownTimeToExplode -= Time.deltaTime;
    //        var circlecasthit = Physics2D.CircleCast(transform.position, 1f, Vector2.zero, 1f, WhatIsPlayer);
    //        if (countdownTimeToExplode <= 0)
    //        {
    //            isExploding = true;
    //            timeIsExploding -= Time.deltaTime;

    //            if (isExploding)
    //            {
    //                if (circlecasthit)
    //                {
    //                    if (!anim.IsPlaying("ExploderExplode"))
    //                    {
    //                        anim.Play("ExploderExplode");
    //                        anim.AnimationCompleted = ExplodeDelegate;
    //                        isRunning = false;
    //                    }
    //                    //ExplosionPool.Instance.impactPoint = circlecasthit.point;
    //                    //ExplosionPool.Instance.ExplodeHere();
    //                    circlecasthit.collider.GetComponent<PlayerHealthController>().PlayerTakeDamage(1f);
    //                }
    //            }
    //            if (timeIsExploding <= 0)
    //            {
    //                isExploding = false;
    //                countdownTimeToExplode = newCDToExplode;
    //            }
    //            if (!isExploding && countdownTimeToExplode >= 0)
    //            {
    //                timeIsExploding = newCDTimeIsExploding;
    //            }
    //        }

    //    }
    //    else if(!hit && !isRunning)
    //    {
    //        rb2d.velocity = Vector2.zero;
    //    }
    //}

    IEnumerator Explodeffs()
    {
        yield return new WaitForSeconds(cooldowntoexplode);
        gameObject.SetActive(false);
    }
}
