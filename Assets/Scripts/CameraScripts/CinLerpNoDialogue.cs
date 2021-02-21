using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CinLerpNoDialogue : MonoBehaviour {

    public float totalLerpTime = 10f;

    public bool startLerping = true;

    public List<Transform> points = new List<Transform>();

	void Update()
    {
        if (startLerping)
        {
            
            transform.position = PlayerDisableUtility.Instance.MyLerp(points[0], points[1], totalLerpTime);
            if (transform.position == points[1].position)
            {
                startLerping = false;
                
            }
        }
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
