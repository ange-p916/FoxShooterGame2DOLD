using UnityEngine;
using System.Collections;

public class FlyAndSlamBoss : EnemyReqComp {

    public enum WhatStates { IDLE, FLY, SHOOTGROUND, SHOOTAIR, SLAM}
    public WhatStates states;
    tk2dSpriteAnimator anim;
    GenericShootingScript shoot;
    EnemyHealthBarController ehbc;
    float bounce;

    bool isFlying;

    [Header("Shooting values")]
    public Transform shootingPoint;
    public float timeBeforeShot;

    [Header("Flying and dist values")]
    public float keepAwayDist;
    public float keepAwayFactorX;
    public float keepAwayFactorY;
    public  float radiusndist;
    public float flyingSpeed;
    public float flyAwayValue;

    [Header("CD values")]
    public float timeToSlam;
    public float newTimeToSlam;
    public float timeIsSlamming;
    public float newTimeIsSlamming;

    [Header("Booleans")]
    public bool isSlamming;

    [Header("Slam values")]
    public float slamSpeed;

    protected override void Start()
    {
        base.Start();
        shoot = GetComponent<GenericShootingScript>();
        ehbc = GetComponent<EnemyHealthBarController>();
        anim = GetComponent<tk2dSpriteAnimator>();
    }

    protected override void Update()
    {
        base.Update();
        
        if(distanceToPlayer <= startActionDistance * startActionDistance)
        {
            ehbc.isBossInitiated = true;
            
        }
        else
        {
            ehbc.isBossInitiated = false;
        }

        if(ehbc.isBossInitiated)
        {
            Fly();
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }

        if (CheckpointManager.Instance.isDead && ehbc.isBossInitiated)
        {
            CheckpointManager.Instance.startCinematicStuff = true;
        }
        
    }

    void ShootBullets()
    {
        shoot.shootingPoint = shootingPoint;
        shoot.shootInArc = true;
        if(!anim.IsPlaying("AncientBossShootFlying"))
        {
            anim.Play("AncientBossShootFlying");
            anim.AnimationCompleted = SlamDelegate;
            isFlying = false;
            isSlamming = false;
        }
        shoot.Shoot();
    }

    void Fly()
    {
        if(!anim.IsPlaying("AncientBossFly"))
        {
            anim.Play("AncientBossFly");
            anim.AnimationCompleted = null;
            isFlying = true;
            isSlamming = false;
        }
        transform.localScale = whatSideIsPlayerAt.x > 0 ? Vector3.one : new Vector3(-1, 1, 1);
        var isPlayerUpOrDown = (player.transform.position - transform.position).y > 0 ? Vector2.up : Vector2.down;
        var rayToPlayerHit = Physics2D.CircleCast(transform.position, radiusndist, Vector2.zero, radiusndist, WhatIsPlayer);
        //Linecast(this.transform.position, player.transform.position, WhatIsPlayer);
        //Raycast(transform.position, whatSideIsPlayerAt, startActionDistance, WhatIsPlayer);

        bounce = rayToPlayerHit.distance - keepAwayDist;
        var bounceVec = new Vector2(bounce * keepAwayFactorX, bounce * keepAwayFactorY);

        //Debug.DrawLine(transform.position, player.transform.position, Color.green);

        if (rayToPlayerHit)
        {
            rb2d.velocity = new Vector2((player.transform.position - this.transform.position).x,
                                        (player.transform.position - this.transform.position).y) * flyingSpeed + bounceVec;

            if ((player.transform.position - this.transform.position).x > 0)
            {
                keepAwayFactorX += flyAwayValue * Time.deltaTime;

            }
            else if ((player.transform.position - this.transform.position).x < 0)
            {
                keepAwayFactorX -= flyAwayValue * Time.deltaTime;
            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }

            if(rayToPlayerHit.distance < 5f)
            {
                timeToSlam -= Time.deltaTime;
                if(timeToSlam <= 0)
                {
                    isSlamming = true;
                    timeIsSlamming -= Time.deltaTime;
                    //if we are slamming, then slam and wait abit before shooting
                    if(isSlamming)
                    {
                        Slam();
                    }
                    if(timeIsSlamming <= 0)
                    {
                        isSlamming = false;
                        timeToSlam = newTimeToSlam;
                    }
                    if(!isSlamming && timeToSlam >= 0)
                    {
                        timeIsSlamming = newTimeIsSlamming;
                    }
                }
            }

            if(isSlamming)
            {
                StartCoroutine(ShootYay(timeBeforeShot));
            }

        }
    }

    IEnumerator ShootYay(float mbefshoot)
    {
        yield return new WaitForSeconds(mbefshoot);
        ShootBullets();
    }

    void SlamDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if(isFlying)
        {
            anim.Play("AncientBossFly");
        }
        else
        {
            anim.Play("AncientBossIdle");
        }
    }

    void Slam()
    {
        if(!anim.IsPlaying("AncientBossSlam"))
        {
            anim.Play("AncientBossSlam");
            anim.AnimationCompleted = SlamDelegate;
            isSlamming = true;
            isFlying = false;
        }
        rb2d.velocity = Vector2.zero;
        rb2d.velocity += new Vector2(0, slamSpeed);

        var boxHit = Physics2D.BoxCast(transform.position, transform.localScale, 0, Vector2.zero, 1, WhatIsPlayer);
        if(boxHit)
        {
            boxHit.collider.GetComponent<PlayerHealthController>().PlayerTakeDamage(2f, this.transform);
        }

    }

}
