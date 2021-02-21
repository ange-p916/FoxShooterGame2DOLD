using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionPool : MonoBehaviour
{
    public static ExplosionPool Instance;
    GameObject newObj;
    public GameObject ExplosionInstance;
    public Vector2 impactPoint;

    public List<GameObject> explosions = new List<GameObject>();
    public int explosionsLength = 5;

    bool initiateExplosion;
    float cdToExplode = 0.3f;
    float newCdToExplode = 0.3f;
    float timeIsExploding = 0.02f;
    float newTimeIsExploding = 0.02f;

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < explosionsLength; i++)
        {
            newObj = (GameObject)Instantiate(ExplosionInstance);
            newObj.SetActive(false);
            explosions.Add(newObj);
        }
    }

    public void ExplodeHere()
    {
        for (int i = 0; i < explosionsLength; i++)
        {
            if (!explosions[i].activeInHierarchy)
            {
                explosions[i].SetActive(true);
                explosions[i].transform.position = impactPoint;
                break;
            }
        }
    }

    public void ExplodeWithin(Transform tsf)
    {
        bool isExploding = false;

        if (initiateExplosion)
        {
            cdToExplode -= Time.deltaTime;
        }

        if (cdToExplode <= 0)
        {
            var losc = tsf.localScale;
            var pointToExplodeAt = tsf.TransformPoint(
                Random.Range(-losc.x, losc.x),
                Random.Range(-losc.y, losc.y), 0);

            isExploding = true;
            timeIsExploding -= Time.deltaTime;
            if (isExploding)
            {
                impactPoint = pointToExplodeAt;

                ExplodeHere();
            }

            if (timeIsExploding <= 0)
            {
                isExploding = false;
                cdToExplode = newCdToExplode;
            }

            if (!isExploding && cdToExplode >= 0)
            {
                timeIsExploding = newTimeIsExploding;
            }

        }
    }

}
