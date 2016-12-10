using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EnemyProjectilePool : MonoBehaviour
{
    public static EnemyProjectilePool Instance;
    GameObject newObj;
    GameObject slugObj;
    GameObject headObj;
    GameObject bossProjObj;

    [Header("Boss stuff")]
    public List<GameObject> bossProjs = new List<GameObject>();
    public int bossProjAmount;
    public Transform bossProjPos;
    public GameObject bossProjectile;

    [Header("Generic projectile stuff")]
    public List<GameObject> enemyProjs = new List<GameObject>();
    public int enemyProjectileCount;
    public Transform enemyProjPos;
    public GameObject enemyProjectile;
    public Vector2 enemyShootDirX = Vector2.zero;

    [Header("Slug stuff")]
    public List<GameObject> slugstuffs = new List<GameObject>();
    public GameObject enemySlugObj;
    public int slugAmount;

    [Header("Medusa head stuff")]
    public List<GameObject> heads = new List<GameObject>();
    public int amountOfHeads = 5;
    public GameObject theHeadObj;

    [Header("Speed")]
    public float bulletSpeed = 4f;

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < enemyProjectileCount; i++)
        {
            newObj = Instantiate(enemyProjectile);
            newObj.SetActive(false);
            enemyProjs.Add(newObj);
        }

        for (int i = 0; i < slugAmount; i++)
        {
            slugObj = Instantiate(enemySlugObj);
            slugObj.SetActive(false);
            slugstuffs.Add(slugObj);
        }

        for (int i = 0; i < amountOfHeads; i++)
        {
            headObj = Instantiate(theHeadObj);
            headObj.SetActive(false);
            heads.Add(headObj);
        }
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            for (int i = 0; i < bossProjAmount; i++)
            {
                bossProjObj = Instantiate(bossProjectile);
                bossProjObj.SetActive(false);
                bossProjs.Add(bossProjObj);
            }
        }
    }

    public void BossThrowLogs(Vector2 whatDir)
    {
        for (int i = 0; i < bossProjAmount; i++)
        {
            if(!bossProjs[i].activeInHierarchy)
            {
                bossProjs[i].SetActive(true);
                bossProjs[i].transform.position = bossProjPos.position;
                bossProjs[i].GetComponent<Rigidbody2D>().velocity = whatDir;
                bossProjs[i].GetComponent<Rigidbody2D>().gravityScale = 0;
                break;
            }
        }
    }

    public void ShootInArc(PlayablePlayer player, Transform thisPos, float v)
    {
        float x = player.transform.position.x - thisPos.transform.position.x;
        float y = player.transform.position.y - thisPos.transform.position.y;
        for (int i = 0; i < enemyProjectileCount; i++)
        {
            if(!enemyProjs[i].activeInHierarchy)
            {
                enemyProjs[i].SetActive(true);
                enemyProjs[i].transform.position = enemyProjPos.position;
                float g = enemyProjs[i].GetComponent<Rigidbody2D>().gravityScale * 10f;
                float det = Mathf.Pow(v, 4) - 2 * v * v * g * y - g * g * x * x;
                if(det > 0)
                {
                    float plusMinus = Mathf.Sqrt(det);
                    float dividend = v * v - plusMinus;
                    float theta = Mathf.Atan(dividend / (g * x));
                    enemyProjs[i].GetComponent<Rigidbody2D>().velocity = new Vector2((x > 0 ? 1 : -1) * v * Mathf.Cos(theta),
                                                                         (x > 0 ? 1 : -1) * v * Mathf.Sin(theta));
                    
                }
                break;
            }
        }
    }

    public void EnemyShooting(Vector2 diagonallyOrStraight)
    {
        for (int i = 0; i < enemyProjectileCount; i++)
        {
            if (!enemyProjs[i].activeInHierarchy)
            {
                enemyProjs[i].SetActive(true);
                enemyProjs[i].transform.position = enemyProjPos.position;
                enemyProjs[i].GetComponent<Rigidbody2D>().velocity = diagonallyOrStraight;
                break;
            }
        }
    }

    //public void ShootingSineWave(Vector2 whatDirection, float speed)
    //{
    //    for (int i = 0; i < enemyProjectileCount; i++)
    //    {
    //        if (!enemyProjs[i].activeInHierarchy)
    //        {
    //            enemyProjs[i].SetActive(true);
    //            enemyProjs[i].transform.position = enemyProjPos.position;
    //            enemyProjs[i].GetComponent<EnemyProjScripts>().waveSpeed = speed;
    //            enemyProjs[i].GetComponent<EnemyProjScripts>().whatDirection = enemyShootDirX;
    //            enemyProjs[i].GetComponent<EnemyProjScripts>().flyInCircleOrNot = true;
    //            break;
    //        }
    //    }
    //}


    public void ShootFourDirections()
    {
        for (int i = 0; i < 5; i++)
        {
            if (!enemyProjs[i].activeInHierarchy)
            {
                enemyProjs[i].SetActive(true);
                enemyProjs[i].transform.position = enemyProjPos.position;
                enemyProjs[0].GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1) * bulletSpeed;
                enemyProjs[1].GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1) * bulletSpeed;
                enemyProjs[2].GetComponent<Rigidbody2D>().velocity = new Vector2(1, -1) * bulletSpeed;
                enemyProjs[3].GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -1) * bulletSpeed;
            }
        }
    }

    public void LeaveStuffBehind()
    {
        for (int i = 0; i < slugAmount; i++)
        {
            if (!slugstuffs[i].activeInHierarchy)
            {
                slugstuffs[i].SetActive(true);
                slugstuffs[i].transform.position = enemyProjPos.position;
                break;
            }
        }
    }

    public void SpawnMedusaHeads(Vector2 whatDir)
    {
        for (int i = 0; i < amountOfHeads; i++)
        {
            if (!heads[i].activeInHierarchy)
            {
                heads[i].SetActive(true);
                heads[i].transform.position = enemyProjPos.position;
                heads[i].GetComponent<Rigidbody2D>().velocity = whatDir;
                break;
            }
        }
    }
}
