using UnityEngine;
using System.Collections;

public class ChargingUpScript : MonoBehaviour {

    tk2dSpriteAnimator anim;
    PlayerShotController psc;
    public MeshRenderer msr;
    void Start()
    {
        psc = GetComponentInParent<PlayerShotController>();
        anim = GetComponent<tk2dSpriteAnimator>();
    }

    void Update()
    {
        if(psc.chargingUp)
        {
            msr.enabled = true;
            if(!anim.IsPlaying("ChargingUp"))
            {
                anim.Play("ChargingUp");
                anim.AnimationCompleted = null;
            }
        }
        else
        {
            msr.enabled = false;
        }
    }



}
