using UnityEngine;
using System.Collections;
using Rewired;
using System;


public class PlayerShooting : MonoBehaviour {

    tk2dSpriteAnimator anim;
    private Controller2D controller;
    PlayablePlayer thePlayer;
<<<<<<< Updated upstream
    Player player;
=======
    Player input;
>>>>>>> Stashed changes

    public float next_shot_timer;
    public float time_between_shots = 0.15f;
    public float shot_cd = 0.05f;
<<<<<<< Updated upstream

    void Awake()
    {
        player = ReInput.players.GetPlayer(0);
=======
    public float time_since_last_shot = 0.05f;

    public bool is_walking = false;
    public bool is_shooting = false;
    public bool is_idling = false;
    bool running = false;

    void Awake()
    {
        input = ReInput.players.GetPlayer(0);
>>>>>>> Stashed changes
        anim = GetComponent<tk2dSpriteAnimator>();
    }

    void Start()
    {
        controller = GetComponent<Controller2D>();
        thePlayer = GetComponent<PlayablePlayer>();

    }

<<<<<<< Updated upstream
    void Update()
    {
        if (player.GetButtonDown("Shoot") && CanShoot )
        {
            ProjectilePool.Instance.ShootStuffLR();
            next_shot_timer = Time.time + time_between_shots;
=======
    void ShootCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if (is_walking)
        {
            anim.Play("walk");
        }
        else if (running)
        {
            anim.Play("run");
        }
        else
        {
            anim.Play("idle");
        }
    }

    void Update()
    {
        if (controller.collisions.below)
        {
            if(thePlayer.input.x != 0)
            {
                if (!is_shooting && !anim.IsPlaying("walk") )
                {
                    anim.Play("walk");
                    anim.AnimationCompleted = null;
                    is_walking = true;
                    is_shooting = false;
                }
            }
            else
            {
                if(!is_shooting)
                {
                    
                    is_idling = true;
                    anim.Play("idle");
                    anim.AnimationCompleted = null;
                    is_walking = false;
                }
                else
                {
                    is_shooting = true;
                }
            }
            
            if(input.GetAxisRaw("Move Horizontal") != 0)
            {
                if (input.GetButtonDown("Shoot") && CanShoot)
                {
                    ProjectilePool.Instance.ShootStuffLR();
                    next_shot_timer = Time.time + time_between_shots;
                    anim.Play("still_shoot");
                    is_walking = false;
                    
                    print("is true now");
                    is_shooting = true;
                    time_since_last_shot = next_shot_timer;
                }
                if (Time.time + time_between_shots > time_since_last_shot + 0.5f)
                {
                    is_shooting = false;
                    is_walking = true;
                    print("is false now");
                }
            }
            else 
            {
                if (input.GetButtonDown("Shoot") && CanShoot)
                {
                    ProjectilePool.Instance.ShootStuffLR();
                    next_shot_timer = Time.time + time_between_shots;
                    is_idling = false;
                    anim.Play("still_shoot");
                    is_shooting = true;
                    time_since_last_shot = next_shot_timer;
                }
                if (Time.time + time_between_shots > time_since_last_shot + 0.5f)
                {
                    is_shooting = false;
                    is_idling = true;
                    print("is false now");
                }

            }
            
>>>>>>> Stashed changes
        }

    }

<<<<<<< Updated upstream
=======
    void animations()
    {

    }

>>>>>>> Stashed changes
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
