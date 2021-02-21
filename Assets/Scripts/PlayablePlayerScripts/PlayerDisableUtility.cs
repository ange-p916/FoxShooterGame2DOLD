using UnityEngine;
using System.Collections;

public class PlayerDisableUtility : MonoBehaviour {

    public static PlayerDisableUtility Instance;
    float curLerpTime = 0f;
    PlayablePlayer thePlayer;
    Controller2D controller2D;
    PlayerHealthController phc;
    PlayerShotController psc;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        thePlayer = FindObjectOfType<PlayablePlayer>();
        controller2D = FindObjectOfType<Controller2D>();
        phc = FindObjectOfType<PlayerHealthController>();
        psc = FindObjectOfType<PlayerShotController>();
    }

    public void PlayerAbility(bool setTrueOrFalse)
    {
        thePlayer.enabled = setTrueOrFalse;
        controller2D.enabled = setTrueOrFalse;
        phc.enabled = setTrueOrFalse;
        psc.enabled = setTrueOrFalse;
    }

    public Vector3 MyLerp(Transform pointA, Transform pointB, float lerpTime)
    {
        curLerpTime += Time.deltaTime;
        if (curLerpTime > lerpTime)
        {
            curLerpTime = lerpTime;
        }
        float percentage = curLerpTime / lerpTime;

        return Vector3.Lerp(pointA.position, pointB.position, percentage);
    }

}
