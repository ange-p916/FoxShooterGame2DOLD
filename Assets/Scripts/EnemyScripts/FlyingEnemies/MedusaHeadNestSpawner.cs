using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MedusaHeadNestSpawner : EnemyReqComp {


    public Vector2 whatDir;
    public float speed;

    [Header("Spawn Timers")]
    public float CDToSpawn;
    public float newCDToSpawn;
    public bool isSpawning;
    public float timeIsSpawning;

    public Transform spawnPoint;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if(distanceToPlayer <= startActionDistance * startActionDistance)
        {
            CheckLoS();
        }
        else
        {
            isSpawning = false;
        }
    }

    void DoMedusaHeadSpawnerStuff()
    {
        CDToSpawn -= Time.deltaTime;

        if (CDToSpawn <= 0)
        {
            isSpawning = true;
            timeIsSpawning -= Time.deltaTime;
            if (isSpawning)
            {
                EnemyProjectilePool.Instance.enemyProjPos = spawnPoint;
                EnemyProjectilePool.Instance.SpawnMedusaHeads(whatDir * speed);
            }

            if (timeIsSpawning <= 0)
            {
                isSpawning = false;
                CDToSpawn = newCDToSpawn;
            }

            if (!isSpawning && CDToSpawn >= 0)
            {
                timeIsSpawning = 0.002f;
            }

        }
    }

    protected override void CheckLoS()
    {
        base.CheckLoS();
        if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, WhatIsGround) && targetInView)
        {
            DoMedusaHeadSpawnerStuff();
        }
    }
}
