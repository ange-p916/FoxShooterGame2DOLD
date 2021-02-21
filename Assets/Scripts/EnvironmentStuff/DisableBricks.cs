using UnityEngine;
using System.Collections;

public class DisableBricks : MonoBehaviour {

    public GameObject[] bricks;
    BossHealthBarController bhbc;

    Vector3 losc;
    Vector3 pointToExplodeAt;

    public bool startexplosion;

    [Header("Explosion stuff")]
    public bool initiateExplosion;
    public float cdToExplode = 0.3f;
    public float newCdToExplode = 0.3f;
    public float timeIsExploding = 0.02f;
    public float newTimeIsExploding = 0.02f;

    void Start()
    {
        bhbc = FindObjectOfType<BossHealthBarController>();
    }

    void Update()
    {
        if(bhbc.health <= 0 && bhbc.gameObject.name == "ThrowLogBoss")
        {
            StartCoroutine(waitwithdisable());
        }

        if(startexplosion)
        {
            ExplodeWithin();
        }
    }

    IEnumerator waitwithdisable()
    {
        initiateExplosion = true;
        startexplosion = true;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < bricks.Length; i++)
        {
            bricks[i].gameObject.SetActive(false);
        }
        startexplosion = false;
        initiateExplosion = false;
        this.gameObject.SetActive(false);
    }

    void ExplodeWithin()
    {
        bool isExploding = false;

        if (initiateExplosion)
        {
            cdToExplode -= Time.deltaTime;
        }

        if (cdToExplode <= 0)
        {  
            for (int i = 0; i < bricks.Length; i++)
            {
                losc = bricks[i].transform.localScale;
                pointToExplodeAt = bricks[i].transform.TransformPoint(
                 Random.Range(-losc.x, losc.x),
                 Random.Range(-losc.y, losc.y), 0);
            }
            
            isExploding = true;
            timeIsExploding -= Time.deltaTime;
            if (isExploding)
            {
                ExplosionPool.Instance.impactPoint = pointToExplodeAt;

                ExplosionPool.Instance.ExplodeHere();
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
