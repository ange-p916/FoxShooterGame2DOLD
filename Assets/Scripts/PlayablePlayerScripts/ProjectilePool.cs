using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour
{

    public static ProjectilePool Instance;

    GameObject newObj;
    public GameObject bullet;

    [Header("LR Up Down")]
    public Transform playerBulletPosRight;
    public Transform playerBulletPosLeft;
    public Transform playerBulletPosUp;
    public Transform playerBulletPosDown;
    //public Transform muzzleFlashPos;
    public List<GameObject> stuff = new List<GameObject>();
    public int bulletCount;
    public Vector2 shootDirX;
    public Vector2 shootDirY = new Vector2(0, 1);
    public Vector2 shootDirYDown = new Vector2(0, -1);
    bool lookingRight;
    public bool canShoot = true;

    PlayablePlayer player;

    GameObject cBlaster;

    [Header("Charged Blaster shots")]
    public GameObject chargedBlasterShot;
    public List<GameObject> cBlasterList = new List<GameObject>();
    public int cBlasterAMount;

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < bulletCount; i++)
        {
            newObj = Instantiate(bullet);
            newObj.SetActive(false);
            stuff.Add(newObj);
        }
        player = FindObjectOfType<PlayablePlayer>();
        for (int i = 0; i < cBlasterAMount; i++)
        {
            cBlaster = Instantiate(chargedBlasterShot);
            cBlaster.SetActive(false);
            cBlasterList.Add(cBlaster);
        }

        playerBulletPosRight = GameObject.Find("Player").transform.GetChild(0);
        playerBulletPosLeft = GameObject.Find("Player").transform.GetChild(1);
        playerBulletPosUp = GameObject.Find("Player").transform.GetChild(2);
        playerBulletPosDown = GameObject.Find("Player").transform.GetChild(3);

    }

    public void ShootStuffDown()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            if (!stuff[i].activeInHierarchy)
            {
                stuff[i].SetActive(true);
                stuff[i].transform.position = playerBulletPosDown.position;
                stuff[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270f));
                stuff[i].GetComponent<Rigidbody2D>().velocity = shootDirYDown * 20f;
                break;
            }
        }
    }

    public void ShootStuffUp()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            if(!stuff[i].activeInHierarchy)
            {
                stuff[i].SetActive(true);
                stuff[i].transform.position = playerBulletPosUp.position;
                stuff[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));
                stuff[i].GetComponent<Rigidbody2D>().velocity = shootDirY * 20f;
                break;
            }
        }
    }

    public void ShootRight()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            if (!stuff[i].activeInHierarchy)
            {
                stuff[i].SetActive(true);
                //MuzzleFlashFuncLR();
                stuff[i].transform.position = playerBulletPosRight.position;
                stuff[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                stuff[i].GetComponent<Rigidbody2D>().velocity = Vector2.right * 20f;
                
                break;
            }
        }
    }

    public void ShootLeft()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            if (!stuff[i].activeInHierarchy)
            {
                stuff[i].SetActive(true);
                //MuzzleFlashFuncLR();
                stuff[i].transform.position = playerBulletPosLeft.position;
                stuff[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                //stuff[i].transform.localScale = player.lookRight ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
                stuff[i].GetComponent<Rigidbody2D>().velocity = Vector2.left * 20f;
                
                break;
            }
        }
    }

    public void ShootChargedBlasterShotLR()
    {
        for (int i = 0; i < cBlasterAMount; i++)
        {
            if (!cBlasterList[i].activeInHierarchy)
            {
                cBlasterList[i].SetActive(true);
                cBlasterList[i].transform.position = playerBulletPosRight.position;
                cBlasterList[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                cBlasterList[i].transform.localScale = player.lookRight ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
                cBlasterList[i].GetComponent<Rigidbody2D>().velocity = shootDirX;
                break;
            }
        }
    }
    //down
    public void ShootChargedBlasterShotDown()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            if (!cBlasterList[i].activeInHierarchy)
            {
                cBlasterList[i].SetActive(true);
                cBlasterList[i].transform.position = playerBulletPosDown.position;
                cBlasterList[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270f));
                cBlasterList[i].GetComponent<Rigidbody2D>().velocity = shootDirYDown * 20f;
                break;
            }
        }
    }

    public void ShootChargedBlasterShotUp()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            if (!cBlasterList[i].activeInHierarchy)
            {
                cBlasterList[i].SetActive(true);
                cBlasterList[i].transform.position = playerBulletPosUp.position;
                cBlasterList[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));
                cBlasterList[i].GetComponent<Rigidbody2D>().velocity = shootDirY * 20f;
                break;
            }
        }
    }

    //void MuzzleFlashFuncLR()
    //{
    //    for (int i = 0; i < muzzleFlashCount; i++)
    //    {
    //        if (!muzzleflashlist[i].activeInHierarchy)
    //        {
    //            muzzleflashlist[i].SetActive(true);
    //            muzzleflashlist[i].transform.position = muzzleFlashPos.position;
    //            return;
    //        }
    //    }
    //}
}
