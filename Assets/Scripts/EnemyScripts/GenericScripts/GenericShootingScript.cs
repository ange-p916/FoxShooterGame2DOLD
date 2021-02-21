using UnityEngine;
using System.Collections;

public class GenericShootingScript : MonoBehaviour
{
    [Header("Cooldowns")]
    public float CDToShoot;
    public float timeIsShooting;
    public float newCDToShoot;

    [Header("Booleans")]
    public bool shootStaticDir = false;
    public bool shootPlayerDir = false;
    public bool shootPlayerDirX = false;
    public bool leaveStuffBehind = false;
    public bool shootInArc = false;

    [Header("Boss shooting bools")]
    public bool BossShootStaticDir;

    [Header("Shot variables")]
    public float shotSpeed = 5f;

    bool initiateShot;
    public bool isShooting;

    [Header("Shot Direction")]
    public Vector2 shootDirection;

    public Transform shootingPoint;
    public Transform shootingPointR;

    PlayablePlayer player;

    void Awake()
    {
        player = FindObjectOfType<PlayablePlayer>();
    }

    public void Shoot()
    {
        var watSide = (player.transform.position - this.transform.position).x > 0 ? shootingPointR : shootingPoint;
        var shootSide = (player.transform.position - this.transform.position).x > 0 ? Vector2.right : Vector2.left;

        initiateShot = true;
        if (initiateShot)
        {
            CDToShoot -= Time.deltaTime;
        }

        if (CDToShoot <= 0)
        {
            isShooting = true;
            timeIsShooting -= Time.deltaTime;
            if (isShooting)
            {
                if(BossShootStaticDir)
                {
                    EnemyProjectilePool.Instance.bossProjPos = shootingPoint;
                    EnemyProjectilePool.Instance.ShootInArc(player, this.transform, shotSpeed);
                }

                if(shootInArc)
                {
                    EnemyProjectilePool.Instance.enemyProjPos = shootingPoint;
                    EnemyProjectilePool.Instance.ShootInArc(player, this.transform, shotSpeed);
                }

                if(leaveStuffBehind)
                {
                    EnemyProjectilePool.Instance.enemyProjPos = shootingPoint;
                    EnemyProjectilePool.Instance.LeaveStuffBehind();
                }
                if (shootPlayerDirX)
                {
                    EnemyProjectilePool.Instance.enemyProjPos = shootingPoint;
                    EnemyProjectilePool.Instance.EnemyShooting(shootSide * shotSpeed);
                }

                if (shootStaticDir)
                {
                    EnemyProjectilePool.Instance.enemyProjPos = shootingPoint;
                    EnemyProjectilePool.Instance.EnemyShooting(shootDirection * shotSpeed);
                    //PredictProjectile.Instance.Parabola(transform.position, shootDirection * shotSpeed);
                }
                if (shootPlayerDir)
                {
                    var playerDir = (player.transform.position - this.transform.position);

                    EnemyProjectilePool.Instance.enemyProjPos = watSide;
                    EnemyProjectilePool.Instance.EnemyShooting(new Vector2( playerDir.x, playerDir.y).normalized * shotSpeed);
                    //PredictProjectile.Instance.Parabola(transform.position, new Vector2(playerDir.x, playerDir.y).normalized * shotSpeed);
                }

            }

            if (timeIsShooting <= 0)
            {
                isShooting = false;
                CDToShoot = newCDToShoot;
            }

            if (!isShooting && CDToShoot >= 0)
            {
                timeIsShooting = 0.002f;
            }

        }
        if (CDToShoot <= 0.5f)
        {
            initiateShot = false;
        }
    }
}
