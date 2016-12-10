using UnityEngine;
using System.Collections;

public class TestEnemyScript : MonoBehaviour {

    public bool initiateExplosion;
    public bool isExploding;

    public float cdToExplode;
    public float newCdToExplode;
    public float timeIsExploding;
    public float newTimeIsExploding;

    BoxCollider2D boxCol;

    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        ExplodeWithin();
    }

	void ExplodeWithin()
    {
        if (initiateExplosion)
        {
            cdToExplode -= Time.deltaTime;
        }

        if (cdToExplode <= 0)
        {
            var losc = transform.localScale;
            var pointToExplodeAt = transform.TransformPoint(
                Random.Range(-losc.x, losc.x),
                Random.Range(-losc.y, losc.y),0);

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
