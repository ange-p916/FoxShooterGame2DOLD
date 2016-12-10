using UnityEngine;
using System.Collections;

public class HealthBooster : MonoBehaviour {

    public LayerMask WhatIsPlayer;
    PlayerHealthController phc;
    void Start()
    {
        phc = FindObjectOfType<PlayerHealthController>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.localScale, 0, Vector2.zero, 1, WhatIsPlayer);
        if (hit)
        {
            if (phc.curHealth < phc.maxHealth)
            {
                phc.HealAgain();
            }
            this.gameObject.SetActive(false);
        }
    }
}
