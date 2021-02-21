using UnityEngine;
using System.Collections;
using Rewired;
using System;

public class PlayerShotController : MonoBehaviour
{
    tk2dSpriteAnimator anim;
    private Controller2D controller;
    public float secondsBetweenShots;
    PlayablePlayer thePlayer;
    private float nextPosThrowTime;
    public bool chargingUp;
    public bool hasFiredProjectile;
    Player player;

    public float chargeTimer = 0;

    void Awake()
    {
        player = ReInput.players.GetPlayer(0);
        anim = GetComponent<tk2dSpriteAnimator>();
    }

    void Start()
    {
        controller = GetComponent<Controller2D>();
        thePlayer = FindObjectOfType<PlayablePlayer>();
    }

    void GenericShootMethod(string buttonToPressDown, bool whatShotDir,Action shootMethod ,bool standardShotDir = false, bool ifShootingStraight = false)
    {
        if((player.GetButtonDown(buttonToPressDown) && standardShotDir && whatShotDir))
        {
            CheckBools(true, false, false);
            if(CanShoot())
            {
                shootMethod();
                if(standardShotDir)
                {
                    nextPosThrowTime = Time.time + secondsBetweenShots;
                }
            }
        }
    }

    void CheckBools(bool shootStraight, bool shootUp, bool shootDown)
    {
        controller.canShootStraight = shootStraight;
        controller.canShootUp = shootUp;
        controller.canShootDown = shootDown;
    }

    void ShootOrChargeUp()
    {
        //shooting
        if(player.GetButtonDown("Shoot") && controller.canShoot && controller.canShootStraight && !chargingUp)
        {
            chargeTimer = 0f;
            CheckBools(true, false, false);
            if (CanShoot())
            {
                ProjectilePool.Instance.ShootRight();
                nextPosThrowTime = Time.time + secondsBetweenShots;
                print("shooting");
            }
        }  //charging up the lazor
        else if(player.GetButton("Shoot") && controller.canShoot && controller.canShootStraight)
        {
            chargeTimer += Time.deltaTime;
            
            if(chargeTimer >= 0.15f)
            {
                //if (!MusicManager.Instance.chargeUpSound.isPlaying)
                //{
                //    MusicManager.Instance.chargeUpSound.PlayDelayed(0f);
                //}
            }
            
            if(chargeTimer >= .5f)
            {
                chargingUp = true;
            }
            else
            {
                chargingUp = false;
                //MusicManager.Instance.chargeUpSound.Stop();

            }            
        }//releasing button
        if (player.GetButtonUp("Shoot") && controller.canShoot && controller.canShootStraight)
        {
            if(chargingUp)
            {
                CheckBools(true, false, false);
                if (CanShoot())
                {
                    if (!MusicManager.Instance.fireChargedShotSound.isPlaying)
                    {
                        MusicManager.Instance.fireChargedShotSound.PlayDelayed(0f);
                        MusicManager.Instance.chargeUpSound.Stop();
                    }
                    ProjectilePool.Instance.ShootChargedBlasterShotLR();
                    nextPosThrowTime = Time.time + secondsBetweenShots;
                }
            }
            chargingUp = false;
        }
    }

    void Shoot()
    {
        //if holding down
        if (player.GetButton("Shoot") && controller.canShoot && controller.canShootStraight)
        {
            chargingUp = true;
            hasFiredProjectile = false;
        } //if releasing
        else if (player.GetButtonUp("Shoot") && controller.canShoot && controller.canShootStraight)
        {
            chargingUp = false;
            if (!chargingUp)
            {
                CheckBools(true, false, false);
                if (CanShoot())
                {
                    ProjectilePool.Instance.ShootChargedBlasterShotLR();
                    nextPosThrowTime = Time.time + secondsBetweenShots;
                    hasFiredProjectile = true;
                }
            }
        } //if pressing
        else if (player.GetButtonDown("Shoot") && controller.canShoot && controller.canShootStraight)
        {
            CheckBools(true, false, false);
            if (CanShoot())
            {
                ProjectilePool.Instance.ShootRight();
                nextPosThrowTime = Time.time + secondsBetweenShots;
            }
        }
    }


    void Update()
    {
        //GenericShootMethod("Shoot", controller.canShootStraight, new Action(ProjectilePool.Instance.ShootStuffLR), controller.canShoot);

        //if ((player.GetButtonDown("Shoot") && controller.canShoot && controller.canShootStraight))
        //{
        //    controller.canShootStraight = true;
        //    controller.canShootUp = false;
        //    controller.canShootDown = false;
        //    if (CanShoot())
        //    {
        //        ProjectilePool.Instance.ShootStuffLR();
        //        nextPosThrowTime = Time.time + secondsBetweenShots;
        //    }
        //}
        ShootOrChargeUp();
        if (player.GetAxisRaw("Look") > 0.5f && controller.canShoot)
        {
            CheckBools(false, true, false);
            //walking and looking up
            if(!anim.IsPlaying("walkLookUp") && thePlayer.input.x != 0 && !player.GetButtonDown("run"))
            {
                anim.Play("walkLookUp");
                anim.AnimationCompleted = null;
            }//run and look up
            if(!anim.IsPlaying("runLookUp") && thePlayer.input.x != 0 && player.GetButtonDown("run"))
            {
                anim.Play("runLookUp");
                anim.AnimationCompleted = null;
            }//jump and look up
            if (!anim.IsPlaying("jumpLookUp") && thePlayer.velocity.y > thePlayer.minJumpHeight)
            {
                anim.Play("jumpLookUp");
                anim.AnimationCompleted = null;
            }//stand still and look up
            //else if (!anim.IsPlaying("runLookUp") && thePlayer.input.x != 0 && player.GetButtonDown("run"))
            //{
            //    anim.Play("runLookUp");
            //    anim.AnimationCompleted = null;
            //}
        }
        else if (player.GetAxisRaw("Look") < -0.5f && controller.canShoot && thePlayer.velocity.y > thePlayer.minJumpHeight)
        {
            CheckBools(false, false, true);
        }
        else if(player.GetAxisRaw("Look") >= 0 && player.GetAxisRaw("Look") < 0.5f &&  controller.canShoot)
        {
            controller.canShootStraight = true;
        }

        if(player.GetButtonDown("Shoot") && controller.canShootUp)
        {
            if(CanShoot())
            {
                ProjectilePool.Instance.ShootStuffUp();
            }
        }
        else if(player.GetButtonDown("Shoot") && controller.canShootDown)
        {
            if(CanShoot())
            {
                ProjectilePool.Instance.ShootStuffDown();
            }
        }
    }

    private bool CanShoot()
    {
        bool ifcanshoot = true;
        if (Time.time < nextPosThrowTime)
        {
            ifcanshoot = false;
        }
        return Time.time < nextPosThrowTime;
       // return ifcanshoot;
    }
}
