using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    public float timeToOpenSecondDoor;

    public Transform startPoint;
    public Transform linkedDoor;
    public LayerMask whatIsPlayer;
    tk2dSpriteAnimator anim;
    BoxCollider2D boxCol;
    public bool startLerping = false;
    public bool hasEntered = false;
    public float timer;
    float curLerpTime = 0f;
    public float lerpTime = 3f;

    public bool playingDoorAnim;

    PlayablePlayer thePlayer;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayablePlayer>();
        anim = GetComponent<tk2dSpriteAnimator>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    void HasOpenedDoorDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        if(playingDoorAnim)
        {
            anim.Play("DoorOpen");
        }
        else
        {
            anim.Play("DoorIdle");
        }
    }

    void Update()
    {
        var boxHit = Physics2D.BoxCast(transform.position, transform.localScale, 0, Vector2.zero, transform.localScale.x, whatIsPlayer);
        
        if(boxHit && !hasEntered)
        {
            StartCoroutine(OpenDoor(0.66f));
            curLerpTime = 0f;
            startLerping = true;
            hasEntered = true;
        }
        if(startLerping && hasEntered)
        {
            thePlayer.enabled = false;
            curLerpTime += Time.deltaTime;
            if(curLerpTime > lerpTime)
            {
                curLerpTime = lerpTime;
            }
            var perc = curLerpTime / lerpTime;
            thePlayer.transform.position = Vector3.Lerp(startPoint.position, linkedDoor.position, perc);
        }

        if(curLerpTime >= lerpTime / timeToOpenSecondDoor)
        {
            StartCoroutine(linkedDoor.GetComponentInParent<DoorScript>().OpenDoor(0.66f));
        }

        if(thePlayer.transform.position == linkedDoor.position)
        {
            thePlayer.enabled = true;
            startLerping = false;
            linkedDoor.GetComponentInParent<DoorScript>().startLerping = false;
            linkedDoor.GetComponentInParent<DoorScript>().hasEntered = false;
            curLerpTime = 0f;
        }
    }

    public IEnumerator OpenDoor(float mTimer)
    {
        if(!anim.IsPlaying("DoorOpen"))
        {
            anim.Play("DoorOpen");
        }
        yield return new WaitForSeconds(mTimer);
        anim.Play("DoorIdle");
    }
}
