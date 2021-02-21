using UnityEngine;
using System.Collections;

public class SlimeSlowDown : MonoBehaviour {

    public LayerMask WhatIsPlayer;

    public float timer = 0f;
    public float stayOnGroundDuration = 7f;
    PlayablePlayer player;

    void OnEnable()
    {
        player = FindObjectOfType<PlayablePlayer>();
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= stayOnGroundDuration)
        {
            gameObject.SetActive(false);
        }
        SlowPlayerDown();
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if(col.gameObject.layer == 10)
    //    {
    //        playerSpeed = 3f;
    //    }
    //}

    //void OnTriggerExit2D(Collider2D col)
    //{
    //    if(col.gameObject.layer == 10)
    //    {
    //        playerSpeed = 6f;
    //    }
    //}


    void SlowPlayerDown()
    {
        var boxcastHit = Physics2D.BoxCast(transform.position, transform.localScale, 0, Vector2.zero, transform.localScale.x, WhatIsPlayer);
        if(boxcastHit)
        {
            player.moveSpeed = 3f;
        }
        else
        {
            player.moveSpeed = 6f;
        }
    }

}
