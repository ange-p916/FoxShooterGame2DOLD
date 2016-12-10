using UnityEngine;
using System.Collections;

public class EnemyProjScripts : MonoBehaviour
{
    public LayerMask WhatIsPlayer;
    public float timer = 0f;
    public float travelDuration = 2f;
    private Rigidbody2D rb2d;
    public float radiusndistance;

    public Sprite[] bulletSprites;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    void OnEnable()
    {
        timer = 0;
    }

    void Update()
    {
        HitStuff();
        timer += Time.deltaTime;
        if (timer >= travelDuration)
        {
            gameObject.SetActive(false);
        }
    }

    void HitStuff()
    {
        var hit = Physics2D.CircleCast(transform.position, radiusndistance, Vector2.zero, radiusndistance, WhatIsPlayer);
        if (hit)
        {
            ExplosionPool.Instance.impactPoint = hit.point;
            ExplosionPool.Instance.ExplodeHere();
            hit.collider.gameObject.GetComponent<PlayerHealthController>().PlayerTakeDamage(1f, this.transform);
            this.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            ExplosionPool.Instance.impactPoint = other.contacts[0].point;
            ExplosionPool.Instance.ExplodeHere();

            this.gameObject.SetActive(false);
        }
    }
}
