using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ThrowLogBoss : EnemyReqComp {

    public enum TheStates { IDLE, THROW, TELEPORT, SUMMON }
    public TheStates states;

    private GenericShootingScript shoot;
    BossHealthBarController bhbc;

    float timeToFadeOut = 1f;
    float changeColor;

    BoxCollider2D boxcol;

    tk2dSpriteAnimator anim;
    bool firstTele, secondTele;
    bool startToFade = false;
    [Header("Cooldowns")]
    public float stateTimer;
    public float state1timer, state2timer, state3timer, state4timer;
    public float newStateTimer;

    public bool throwBossDED = false;
    public Transform shootingPoint;

    List<TeleportPoint> telepoints = new List<TeleportPoint>();
    EnemyHealthBarController ehbc;
    protected override void Start()
    {
        base.Start();
        telepoints = FindObjectsOfType<TeleportPoint>().OrderBy(t => t.gameObject.name).ToList();
        shoot = GetComponent<GenericShootingScript>();
        ehbc = GetComponent<EnemyHealthBarController>();
        bhbc = GetComponent<BossHealthBarController>();
        anim = GetComponent<tk2dSpriteAnimator>();
        boxcol = GetComponent<BoxCollider2D>();
    }

    protected override void Update()
    {
        base.Update();
        DoTrollStuff();
    }

    IEnumerator WaitWithDoingStuff(float timer, TheStates state)
    {
        yield return new WaitForSeconds(timer); //first wait abit
        states = state; //then change state
    }

    void DoTrollStuff()
    {
        transform.localScale = whatSideIsPlayerAt.x > 0 ? Vector3.one : new Vector3(-1, 1, 1);

        if(distanceToPlayer <= startActionDistance * startActionDistance)
        {
            if(bhbc.health > 0)
            {
                ehbc.isBossInitiated = true;
            }
            else
            {
                ehbc.isBossInitiated = false;
            }
        }
        else
        {
            ehbc.isBossInitiated = false;
            states = TheStates.IDLE;
        }

        if (ehbc.isBossInitiated)
        {
            stateTimer -= Time.deltaTime;
            if(stateTimer <= state1timer && stateTimer >= state2timer)
            {
                firstTele = true;
                secondTele = false;
                states = TheStates.TELEPORT;
            }
            if(stateTimer <= state2timer)
            {
                states = TheStates.THROW;
            }
            if(stateTimer <= state3timer)
            {
                firstTele = false;
                secondTele = true;
                states = TheStates.TELEPORT;
                
            }
            if(stateTimer <= state4timer)
            {
                states = TheStates.THROW;
            }
            if(stateTimer <= 0)
            {
                stateTimer = newStateTimer;
            }
        }
        else
        {
            states = TheStates.IDLE;
        }

        //-----------STATES----------------

        if (states == TheStates.IDLE)
        {
            if(!anim.IsPlaying("ThrowBossIdle"))
            {
                anim.Play("ThrowBossIdle");
                anim.AnimationCompleted = null;
            }
        }
        
        if(states == TheStates.TELEPORT)
        {
            if(firstTele)
            {
                StartCoroutine(TeleportAndStuff(telepoints, 0, 1, 1.33f));
            }
            if(secondTele)
            {
                StartCoroutine(TeleportAndStuff(telepoints, 1, 0, 1.33f));
            }
        }

        if (states == TheStates.THROW)
        {
            ThrowLogs();
            if (!anim.IsPlaying("ThrowBossThrow"))
            {
                anim.Play("ThrowBossThrow");
                anim.AnimationCompleted = ShootDelegate;
            }
        }

        if (states == TheStates.SUMMON)
        {
            if (!anim.IsPlaying("ThrowBossTP"))
            {
                anim.Play("ThrowBossTP");
                anim.AnimationCompleted = null;
            }
        }
    }

    IEnumerator TeleportAndStuff(List<TeleportPoint> otherTele, int firstTelepoint, int secondTelepoint, float time)
    {
        boxcol.enabled = false;
        if (!anim.IsPlaying("ThrowBossTP"))
        {
            anim.Play("ThrowBossTP");
            anim.AnimationCompleted = TeleportDelegate;
        }
        yield return new WaitForSeconds(time);
        boxcol.enabled = true;
        if (transform.position == otherTele[firstTelepoint].transform.position)
        {
            transform.position = otherTele[secondTelepoint].transform.position;
        }
    }

    void TeleportDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if (states == TheStates.TELEPORT)
        {
            anim.Play("ThrowBossTP");
        }
        else
        {
            anim.Play("ThrowBossIdle");
        }
    }


    void ShootDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if(states == TheStates.THROW && shoot.isShooting)
        {
            anim.Play("ThrowBossThrow");
        }
        else
        {
            anim.Play("ThrowBossTP");
        }
    }

    void ThrowLogs()
    {
        shoot.shootingPoint = shootingPoint;
        shoot.shootInArc = true;
        shoot.Shoot();
    }

    void FadeOut()
    {
        if (startToFade)
        {
            changeColor = GetComponent<SpriteRenderer>().color.a;
            if (changeColor >= 0)
            {
                changeColor -= Time.deltaTime / timeToFadeOut;
                this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255f, changeColor);
            }
            if (changeColor <= 0)
            {
                changeColor = 1f;
                this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255f, changeColor);
                startToFade = false;
            }
        }
    }
}
