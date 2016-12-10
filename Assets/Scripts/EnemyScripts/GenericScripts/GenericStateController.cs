using UnityEngine;
using System.Collections;

public class GenericStateController : MonoBehaviour {

    public enum WhatStateIsThis
    {
        IDLE,
        PATROLLING,
        CHARGING,
        FLYING, SHOOTING,
        JUMPING,
        EXPLODER,
        TELEPORTER,
        SLUG
    };
    public WhatStateIsThis state;

    #region getstuff
    GenericChargeScript charge;
    GenericFlyingScript flying;
    GenericJumpingScript jumping;
    GenericShootingScript shooting;
    GenericExploderEnemyScript exploder;
    GenericTeleporterScript teleporter;
    SlugEnemyBehaviour slug;
    #endregion

    Rigidbody2D rb2d;

    void GetDiffComps()
    {
        charge = GetComponent<GenericChargeScript>();
        flying = GetComponent<GenericFlyingScript>();
        jumping = GetComponent<GenericJumpingScript>();
        shooting = GetComponent<GenericShootingScript>();
        exploder = GetComponent<GenericExploderEnemyScript>();
        teleporter = GetComponent<GenericTeleporterScript>();
        slug = GetComponent<SlugEnemyBehaviour>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        GetDiffComps();
    }

    void Update()
    {
        if (state == WhatStateIsThis.IDLE)
        {
            rb2d.velocity = Vector2.zero;
        }
        else if (state == WhatStateIsThis.PATROLLING)
        {
            //TODO:
            //patrolling.Patrol();
        }
        else if (state == WhatStateIsThis.CHARGING)
        {
            //TODO:
            charge.Charge();
        }
        else if (state == WhatStateIsThis.FLYING)
        {
            //TODO:
            //flying.Fly();
        }
        else if (state == WhatStateIsThis.SHOOTING)
        {
            //TODO:
            //shooting.Shoot();
        }
        else if (state == WhatStateIsThis.JUMPING)
        {
            //TODO:
            //jumping.Jump();
        }
        else if(state == WhatStateIsThis.EXPLODER)
        {
            //exploder.RunAndExplode();
        }
        else if(state == WhatStateIsThis.TELEPORTER)
        {
            //TODO:
            teleporter.ShootArrowAndTeleportAndShootFireBalls();
        }
        else if(state == WhatStateIsThis.SLUG)
        {
            slug.DoSlugStuff();
        }
    }

}
