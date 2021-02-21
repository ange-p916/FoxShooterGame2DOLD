using UnityEngine;
using System.Collections;
using Rewired;

public class MetroidCamera : MonoBehaviour
{
    public static MetroidCamera Instance; 

    [Header("Camera values")]
    public float verticalOffset;
    public float fallSpeedYToSmooth;
    public float fallSmoothVelY;
    public float newLookVariable;
    public float lookUpVariable = 2f;
    public float lookDownVariable = 2f;
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    public Vector2 focusAreaSize;
    PlayablePlayer mPlayer;
    Player thePlayer;

    FocusArea focusArea;
    private Controller2D target;
    public bool switchToCinematicMode = false;

    [HideInInspector]
    public float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    bool lookAheadStopped;

    void Awake()
    {
        thePlayer = ReInput.players.GetPlayer(0);
        mPlayer = FindObjectOfType<PlayablePlayer>();
        target = FindObjectOfType<Controller2D>();
    }

    void Start()
    {
        focusArea = new FocusArea(target.thisCollider.bounds, focusAreaSize);
    }

    void LateUpdate()
    {
        CameraLateUpdateStuff();
    }

    public void CameraLateUpdateStuff()
    {

        focusArea.Update(target.thisCollider.bounds);

        Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffset;

        if(mPlayer.velocity.y < fallSpeedYToSmooth && !target.collisions.below)
        {
            newLookVariable = -5f;
        }
        else
        {
            newLookVariable = Mathf.SmoothDamp(transform.position.y, focusPosition.y - 2.2f, ref smoothVelocityY, fallSmoothVelY);
        }

        if (focusArea.velocity.x != 0)
        {
            lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
            if (Mathf.Sign(target.playerInputVector2.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInputVector2.x != 0)
            {
                lookAheadStopped = false;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;

            }
            else if (Mathf.Sign(target.playerInputVector2.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInputVector2.x == 0)
            {
                lookAheadStopped = true;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;
            }
            //TODO: 
            //ENABLE THIS TO MAKE THE CAMERA LERP AND MOVE SLOWLY TO ONE SIDE WHEN MOVING LEFT OR RIGHT
            /*else
            {
                if (!lookAheadStopped)
                {
                    lookAheadStopped = true;
                    targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
                }

            }*/
        }
        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        focusPosition += Vector2.right * currentLookAheadX;
        transform.position = (Vector3)focusPosition + Vector3.forward * -10;

        if (thePlayer.GetAxisRaw("Look") < -0.5f)
        {
            verticalOffset -= lookDownVariable;
        }
        else if(thePlayer.GetAxisRaw("Look") > 0.5f)
        {
            verticalOffset += lookUpVariable;
        }
        else
        {
            verticalOffset = newLookVariable;
        }

        verticalOffset = Mathf.Clamp(verticalOffset, -lookDownVariable, lookUpVariable);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
    }

    struct FocusArea
    {
        public Vector2 centre;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);


        }
    }

    public void SnapCam(Transform snapPos)
    {
        var snapPosition = snapPos.position;
        transform.position = new Vector3(snapPosition.x, snapPosition.y, -10);
    }

}
