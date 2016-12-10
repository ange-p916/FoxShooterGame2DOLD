using UnityEngine;
using System.Collections;

public class BossLogProjectileScript : MonoBehaviour {

    public LayerMask WhatIsPlayer;
    public float timer = 0f;
    public float travelDuration = 4f;

    void OnEnable()
    {
        timer = 0;
    }
    void Update()
    {
        var boxcastHit = Physics2D.BoxCast(transform.position, transform.localScale, 0, Vector2.zero, transform.localScale.x, WhatIsPlayer);
        if (boxcastHit)
        {
            ExplosionPool.Instance.impactPoint = boxcastHit.point;
            ExplosionPool.Instance.ExplodeHere();
            //boxcastHit.collider.GetComponent<PlayerHealthController>().PlayerTakeDamage(1f);
            this.gameObject.SetActive(false);
        }

        timer += Time.deltaTime;
        if (timer >= travelDuration)
        {
            gameObject.SetActive(false);
        }

    }

}
