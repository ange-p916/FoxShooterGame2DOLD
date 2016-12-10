using UnityEngine;
using System.Collections;

public class CamLerpEnablerScript : MonoBehaviour {

    public LayerMask WhatIsPlayer;
    public bool hasPickedUpYet;
    CamLerpNew camLerp;

    void Start()
    {
        camLerp = FindObjectOfType<MetroidCamera>().GetComponent<CamLerpNew>();
    }

    void Update()
    {
        var boxcastHit = Physics2D.BoxCast(transform.position, transform.localScale, 0, Vector2.zero, transform.localScale.x, WhatIsPlayer);
        if (boxcastHit && !hasPickedUpYet)
        {
            hasPickedUpYet = true;
            
            camLerp.enabled = true;
            camLerp.startLerpingBool = true;
            this.enabled = false;
        }
    }

}
