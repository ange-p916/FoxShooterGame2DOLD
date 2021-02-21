using UnityEngine;
using System.Collections;
using Rewired;

public class TutAnimController : MonoBehaviour {

    IsInTest IsInTest;
    tk2dSpriteAnimator anim;
    Player input;
    PlayablePlayer thePlayer;
    Controller2D controller;

    bool walking = false;
    bool running = false;

    void Start()
    {
        IsInTest = GetComponent<IsInTest>();
        anim = GetComponent<tk2dSpriteAnimator>();
        input = ReInput.players.GetPlayer(0);
        thePlayer = GetComponent<PlayablePlayer>();
        controller = GetComponent<Controller2D>();
    }

    void HitCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if(walking)
        {
            anim.Play("walk");
        }
        else
        {
            anim.Play("idle");
        }
    }

    void ShootCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if(walking)
        {
            anim.Play("walk");
        }
        else if(running)
        {
            anim.Play("run");
        }
        else
        {
            anim.Play("idle");
        }
    }

    //void ShootCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    //{

    //}

    void Update()
    {
        if(controller.collisions.below)
        {
            //walk
            if (thePlayer.input.x != 0 && !input.GetButton("Run"))
            {
                if (!anim.IsPlaying("walk"))
                {
                    anim.Play("walk");
                    anim.AnimationCompleted = null;
                    running = false;
                    walking = true;
                }
            } //run 
            else if (thePlayer.input.x != 0 && input.GetButton("Run"))
            {
                if (!anim.IsPlaying("run"))
                {
                    anim.Play("run");
                    anim.AnimationCompleted = null;
                    walking = false;
                    running = true;
                }
            }
            else
            {
                anim.Play("idle");
                anim.AnimationCompleted = null;
                walking = false;
                running = false;
            }
            //run and shoot straight
            if (thePlayer.input.x != 0 && input.GetButton("Run") && controller.canShootStraight && input.GetButtonDown("Shoot"))
            {
                if (!anim.IsPlaying("run_shoot"))
                {
                    anim.Play("run_shoot");
                    anim.AnimationCompleted = ShootCompleteDelegate;
                }
            }
            //run and shoot up
            if (thePlayer.input.x != 0 && input.GetButton("Run") && controller.canShootUp && input.GetButtonDown("Shoot"))
            {
                if (!anim.IsPlaying("runShootUp"))
                {
                    anim.Play("runShootUp");
                    anim.AnimationCompleted = ShootCompleteDelegate;
                }
            }

            //walk and shoot straight
            if (thePlayer.input.x != 0 && !input.GetButton("Run") && controller.canShootStraight && input.GetButtonDown("Shoot"))
            {
                if (!anim.IsPlaying("walkShoot"))
                {
                    anim.Play("walkshoot");
                    anim.AnimationCompleted = ShootCompleteDelegate;
                }
            }
            //walk and shoot up
            if (thePlayer.input.x != 0 && !input.GetButton("Run") && controller.canShootUp && input.GetButtonDown("Shoot"))
            {
                if (!anim.IsPlaying("WalkShootUp"))
                {
                    anim.Play("WalkShootUp");
                    anim.AnimationCompleted = ShootCompleteDelegate;
                }
            }

        }

        
        //jump
        if(!controller.collisions.below)
        {
            //jump
            if(!anim.IsPlaying("jump"))
            {
                anim.Play("jump");
                anim.AnimationCompleted = null;
            }

            //jump shoot up
            if(!anim.IsPlaying("jumpShootUp") && controller.canShootUp && input.GetButtonDown("Shoot"))
            {
                anim.Play("jumpShootUp");
                anim.AnimationCompleted = null;
            }//jump shoot
            if (!anim.IsPlaying("jumpShoot") && controller.canShootStraight && input.GetButtonDown("Shoot"))
            {
                anim.Play("jumpShoot");
                anim.AnimationCompleted = null;
            }
            //jump shoot down
            if (!anim.IsPlaying("jumpShootDown") && controller.canShootDown && input.GetButtonDown("Shoot"))
            {
                anim.Play("jumpShootDown");
                anim.AnimationCompleted = null;
            }

        }

        if (!IsInTest.Testing)
        {
            if (CheckpointManager.Instance.isDead)
            {
                if (!anim.IsPlaying("death"))
                {
                    anim.Play("death");
                    anim.AnimationCompleted = null;
                }
            }
        }

        
    }


}
