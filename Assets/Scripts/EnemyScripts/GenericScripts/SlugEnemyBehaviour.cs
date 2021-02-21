using UnityEngine;
using System.Collections;

public class SlugEnemyBehaviour : EnemyReqComp {

    public Transform spawnPos;
    public float CDToSpawnSlug;
    public float newCDToSpawnSlug;
    public bool isSpawning;
    public float timeIsSpawning;
    public float newTimeIsSpawning;

    GenericObjectPool pool;

    protected override void Start()
    {
        base.Start();
        pool = GetComponent<GenericObjectPool>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void DoSlugStuff()
    {
        CDToSpawnSlug -= Time.deltaTime;
        if(CDToSpawnSlug <= 0)
        {
            isSpawning = true;
            timeIsSpawning -= Time.deltaTime;
            if (isSpawning)
            {
                for (int i = 0; i < pool.amount; i++)
                {
                    pool.objs[i].transform.position = spawnPos.position;
                }
            }
            if (timeIsSpawning <= 0)
            {
                isSpawning = false;
                CDToSpawnSlug = newCDToSpawnSlug;
            }
            if(!isSpawning && CDToSpawnSlug >= 0)
            {
                timeIsSpawning = newTimeIsSpawning;
            }
        }
    }


}
