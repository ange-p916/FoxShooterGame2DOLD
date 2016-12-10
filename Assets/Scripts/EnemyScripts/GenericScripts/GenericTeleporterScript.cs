using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GenericTeleporterScript : EnemyReqComp
{
    public float time;
    public EnemyProjScripts bullets;
    public float shotSpeed = 20f;
    public float yMultiplier;
    public float xMultiplier;

    [Header("Cooldowns")]
    public float CDToShoot;
    public float timeIsShooting;
    public float newCDToShoot;

    [Header("Booleans")]
    public bool shootStaticDir = false;
    public bool shootPlayerDir = false;
    public bool shootPlayerDirX = false;

    [Header("Shot vector2s")]
    public Vector2 staticShootingVec;
    public Vector2 playerShootingVec;

    public Transform shootpos;

    bool isShooting = false;

    protected override void Start()
    {
        base.Start();
        bullets = FindObjectOfType<EnemyProjScripts>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void ShootArrowAndTeleportAndShootFireBalls()
    {
        transform.localScale = (whatSideIsPlayerAt.x > 0) ? Vector3.one : new Vector3(-1,1,1);

        //var shootWhatDir = (player.transform.position - this.transform.position).x > 0 ? shootposR : shootposL;
        CDToShoot -= Time.deltaTime;
        
        if (CDToShoot <= 0)
        {
            isShooting = true;
            timeIsShooting -= Time.deltaTime;
            if (isShooting)
            {
                if (shootStaticDir)
                {
                    staticShootingVec = new Vector2(whatSideIsPlayerAt.x, 0);

                    EnemyProjectilePool.Instance.enemyProjPos = shootpos;
                    EnemyProjectilePool.Instance.EnemyShooting(staticShootingVec * shotSpeed);
                }
                else if(shootPlayerDir)
                {
                    
                    playerShootingVec = new Vector2(whatSideIsPlayerAt.x, 2);
                    EnemyProjectilePool.Instance.enemyProjPos = shootpos;
                    EnemyProjectilePool.Instance.EnemyShooting(playerShootingVec * shotSpeed);
                }
            }

            if (timeIsShooting <= 0)
            {
                isShooting = false;
                CDToShoot = Random.Range(0.7f, newCDToShoot);
            }
        }
    }
}
