using UnityEngine;
using System.Collections;

public class CamLerpScript : MonoBehaviour {

    public GameObject holdTheDoor;
    public bool holdTheDoorFaded = false;
    bool startToFade;
    float changeColor;
    public float timeToFadeOut;
    ThrowLogBoss throwBoss;
    public float timeToActivatePlayerAgain;
    public Transform pointA, pointB;
    public Transform pointC, pointD;
    public float smooth;
    public bool switchingToCinMode;
    public bool startA, startB;
    MetroidCamera mCam;
    PlayablePlayer player;
    public float lerpTime = 1f;
    public float curLerpTime;
    void Start()
    {
        mCam = GetComponent<MetroidCamera>();
        player = FindObjectOfType<PlayablePlayer>();
        throwBoss = FindObjectOfType<ThrowLogBoss>();
    }

    void Update()
    {
        if(startA)
        {
            switchingToCinMode = true;
            StartLerping(pointA, pointB);
        }
        else if(startB)
        {
            switchingToCinMode = true;
            StartLerping(pointC, pointD);
        }
    }

    void ResetValues()
    {
        curLerpTime = 0f;
        timeToActivatePlayerAgain = 2f;
    }

    public void StartLerping(Transform pointOne, Transform pointTwo)
    {
        if (switchingToCinMode)
        {
            //player.anim.SetInteger("AnimState", 0);
            player.enabled = false;
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
                timeToActivatePlayerAgain -= Time.deltaTime;
                
                if(startA)
                {
                    startToFade = true;
                    FadeOut();
                    holdTheDoorFaded = true;
                }

                if (timeToActivatePlayerAgain <= 0)
                {
                    if(startA)
                    {
                        holdTheDoor.SetActive(false);
                        startA = false;
                    }
                    if(startB)
                    {
                        startB = false;
                    }
                    
                    switchingToCinMode = false;
                    player.enabled = true;
                    mCam.enabled = true;
                }
            }
        }
        else
        {
            mCam.enabled = true;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

    void FadeOut()
    {
        if (startToFade)
        {
            changeColor = holdTheDoor.GetComponent<SpriteRenderer>().color.a;
            if (changeColor >= 0)
            {
                changeColor -= Time.deltaTime / timeToFadeOut;
                holdTheDoor.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255f, changeColor);
            }
            if (changeColor <= 0)
            {
                changeColor = 1f;
                holdTheDoor.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255f, changeColor);
                startToFade = false;
            }
        }
    }

}
