using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamLerpNew : MonoBehaviour {

    public bool startDialogueYet = true; 
    public DialogueScript dialogue;
    public TypeWriter twman;
    public List<Transform> points = new List<Transform>();
    public float shortTimer = 2f;
    public float smooth;
    public bool switchingToCinMode;
    MetroidCamera mCam;
    public float lerpTime = 1f;
    public float curLerpTime;
    public bool startLerpingBool;

    void Start()
    {
        mCam = GetComponent<MetroidCamera>();
        if(!GameScript.gameContinue)
        {
            if (!startDialogueYet)
            {
                startLerpingBool = true;
            }
        }
        
    }

    void Update()
    {
        if (startLerpingBool)
        {
            switchingToCinMode = true;
            StartLerping(points[0], points[1]);
        }
    }

    IEnumerator WaitAbitMan(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        startLerpingBool = true;
    }

    public void StartLerping(Transform pointOne, Transform pointTwo)
    {
        if (switchingToCinMode)
        {
            //player.anim.SetInteger("AnimState", 0);
            PlayerDisableUtility.Instance.PlayerAbility(false);
            mCam.enabled = false;
            curLerpTime += Time.deltaTime;
            if (curLerpTime > lerpTime)
            {
                curLerpTime = lerpTime;
            }
            float percentage = curLerpTime / lerpTime;

            transform.position = Vector3.Lerp(pointOne.position, pointTwo.position, percentage);



            if (transform.position == pointTwo.position)
            {
                if(dialogue != null)
                {
                    dialogue.enabled = true;
                }
                shortTimer -= Time.deltaTime;
                if(shortTimer <= 0f)
                {
                    shortTimer = 0f;
                    if (!twman.startDialogue)
                    {
                        startLerpingBool = false;

                        switchingToCinMode = false;
                        PlayerDisableUtility.Instance.PlayerAbility(true);
                        mCam.enabled = true;
                    }
                }
                
                
            }
        }
        else
        {
            mCam.enabled = true;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

}
