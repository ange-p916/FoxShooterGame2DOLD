using UnityEngine;
using System.Collections;

public class MedusaHead : EnemyReqComp {

    public float timer = 0f;
    public float travelDuration = 5f;

    public float radiusndist = 1f;

    public float timingOffset = 0f;
    public float timeWaveVar = 1f;
    public float height;
    public float speed;
    public Vector2 whatDirection;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if(distanceToPlayer <= startActionDistance * startActionDistance)
        {
            FlyInSineWave();
        }
        timer += Time.deltaTime;
        if(timer >= travelDuration)
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        timer = 0f;
    }

    void FlyInSineWave()
    {
        var push = (Mathf.Sin((Time.time + timingOffset) * timeWaveVar)) * height;
        rb2d.velocity = new Vector2(whatDirection.x * speed, push);
        DoDamage();
    }
    void DoDamage()
    {
        var circlecasthit = Physics2D.CircleCast(transform.position, radiusndist, Vector2.zero, radiusndist, WhatIsPlayer);
        if(circlecasthit)
        {
            circlecasthit.collider.GetComponent<PlayerHealthController>().PlayerTakeDamage(1f, this.transform);
        }
    }
}
