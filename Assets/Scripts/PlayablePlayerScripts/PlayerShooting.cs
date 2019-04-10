using UnityEngine;
using System.Collections;
using Rewired;
using System;


public class PlayerShooting : MonoBehaviour {

    tk2dSpriteAnimator anim;
    private Controller2D controller;
    PlayablePlayer thePlayer;
    Player player;

    public float next_shot_timer;
    public float time_between_shots = 0.15f;
    public float shot_cd = 0.05f;

    void Awake()
    {
        player = ReInput.players.GetPlayer(0);
        anim = GetComponent<tk2dSpriteAnimator>();
    }

    void Start()
    {
        controller = GetComponent<Controller2D>();
        thePlayer = GetComponent<PlayablePlayer>();

    }

    void Update()
    {
        if (player.GetButtonDown("Shoot") && CanShoot )
        {
            ProjectilePool.Instance.ShootStuffLR();
            next_shot_timer = Time.time + time_between_shots;
        }

    }

    void CheckBools(bool shootStraight, bool shootUp, bool shootDown)
    {
        controller.canShootStraight = shootStraight;
        controller.canShootUp = shootUp;
        controller.canShootDown = shootDown;
    }
    private bool CanShoot
    {
        get
        {
            return Time.time > next_shot_timer;
        }
    }
    void RegularShot()
    {
        
    }

    void Shoot()
    {
        
    }

}
