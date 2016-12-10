using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BoatLerpScript : MonoBehaviour
{
    public string levelToLoad = "";
    public TypeWriter twman;

    public bool startLerpingBool = false;
    public bool switchToCinMode = false;

    public List<Transform> points = new List<Transform>();

    public float lerpTime = 1f;
    public float curLerpTime;

    void Start()
    {
        twman.startDialogue = true;
    }


    void Update()
    {
        if(startLerpingBool)
        {
            switchToCinMode = true;
            StartLerping(points[0], points[1]);
        }
    }

    void StartLerping(Transform pointOne, Transform pointTwo)
    {
        if(switchToCinMode)
        {
            curLerpTime += Time.deltaTime;
            if (curLerpTime > lerpTime)
            {
                curLerpTime = lerpTime;
            }
            float percentage = curLerpTime / lerpTime;

            transform.position = Vector3.Lerp(pointOne.position, pointTwo.position, percentage);



            if (transform.position == pointTwo.position || twman.hasFinished)
            {
                SceneManager.LoadScene(levelToLoad);                
            }
        }
        
    }
}
