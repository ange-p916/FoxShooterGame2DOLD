using UnityEngine;
using System.Collections;

public class GenericJumpingScript : MonoBehaviour
{

    Rigidbody2D rb2d;
    PlayablePlayer player;

    public LayerMask WhatIsGround;

    [Header("Jump variables")]
    public float jumpSpeedX;
    public float jumpSpeedY;

    [Header("Timers")]
    public float timeIsJumping;
    public float countDownTimeToJump;
    public float newCDToJump;

    [Header("Booleans")]
    public bool countDownStart;
    public bool isJumping;


    void Start()
    {
        player = FindObjectOfType<PlayablePlayer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        var whereIsPlayer = (player.transform.position - this.transform.position).x > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, 1, WhatIsGround);
        if (groundHit)
        {
            countDownStart = true;
        }

        if (countDownStart)
        {
            countDownTimeToJump -= Time.deltaTime;
        }

        if (countDownTimeToJump <= 0)
        {
            countDownStart = false;
            isJumping = true;
            timeIsJumping -= Time.deltaTime;

            if (isJumping)
            {
                rb2d.velocity = new Vector2(whereIsPlayer.x * jumpSpeedX, jumpSpeedY);
            }

            if (timeIsJumping <= 0)
            {
                isJumping = false;
                countDownTimeToJump = newCDToJump;
            }

            if (!isJumping && countDownTimeToJump >= 0)
            {
                timeIsJumping = 0.1f;
            }
        }
    }
}
