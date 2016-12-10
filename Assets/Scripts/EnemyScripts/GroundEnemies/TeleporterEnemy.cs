using UnityEngine;
using System.Collections;

public class TeleporterEnemy : EnemyReqComp {

    public LayerMask WhatIsWall;

    public float distToKeepFromWall;
    public float distToKeepFromPlayer;

    public float rayDistToStuff = 5f;

    float bounce;

    [Header("Timers")]
    public float CDToTeleport;
    public float timeIsTeleporting;
    public float newCDToTeleport;
    public bool isTeleporting;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (distanceToPlayer < startActionDistance * startActionDistance)
        {
            TeleportAndShoot();
        }
        else
        {

        }
    }

    void TeleportAndShoot()
    {
        var hitWall = Physics2D.Raycast(transform.position, whatSideIsPlayerAt, rayDistToStuff, WhatIsWall);
        var hitPlayer = Physics2D.Raycast(transform.position, whatSideIsPlayerAt, rayDistToStuff, WhatIsPlayer);

        Debug.DrawRay(transform.position, whatSideIsPlayerAt * rayDistToStuff);

        bounce = hitWall.distance - distToKeepFromWall;
        var bounce2 = whatSideIsPlayerAt.x > 0 ? hitPlayer.distance + distToKeepFromPlayer : hitPlayer.distance - distToKeepFromPlayer;
        rb2d.velocity = whatSideIsPlayerAt * 2f;
        CDToTeleport -= Time.deltaTime;

        if (CDToTeleport <= 0)
        {
            isTeleporting = true;
            timeIsTeleporting -= Time.deltaTime;
            if (isTeleporting)
            {
                if (hitWall && !hitPlayer)
                {
                    transform.position = new Vector3(transform.position.x + bounce, transform.position.y, 0);
                }
                else if(hitWall)
                {

                }
                else if(hitPlayer)
                {
                    transform.position = new Vector3(transform.position.x + bounce2, transform.position.y, 0);
                }
            }

            if (timeIsTeleporting <= 0)
            {
                isTeleporting = false;
                CDToTeleport = newCDToTeleport;
            }

            if (!isTeleporting && CDToTeleport >= 0)
            {
                timeIsTeleporting = 0.002f;
            }
        }
    }
}
