using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueScript : MonoBehaviour {

    public LayerMask WhatIsPlayer;
    ActivateHologramManager holoMan;
    public bool hasPickedUpYet = false;
    TypeWriter twman;
    public bool shouldThisScriptLerp = false;
    bool startLerping = false;
    public float totalLerpTime = 3f;
    public float curLerpTime = 0f;
    public List<Transform> points = new List<Transform>();

    PlayablePlayer player;

    void Start()
    {
        player = FindObjectOfType<PlayablePlayer>();
        twman = GetComponent<TypeWriter>();
        holoMan = GetComponent<ActivateHologramManager>();
    }

    void Update()
    {
        var boxcastHit = Physics2D.BoxCast(transform.position, transform.localScale, 0, Vector2.zero, transform.localScale.x, WhatIsPlayer);
        if (boxcastHit && !hasPickedUpYet)
        {
            hasPickedUpYet = true;
            if(shouldThisScriptLerp)
            {
                startLerping = true;
            }
            else
            {
                twman.startDialogue = true;
            }
            
        }
        if(startLerping)
        {
            player.transform.position = PlayerDisableUtility.Instance.MyLerp(points[0], points[1], totalLerpTime);
            if (player.transform.position == points[1].position)
            {
                twman.startDialogue = true;
                if (holoMan != null)
                {
                    holoMan.enabled = true;
                }
                startLerping = false;
                this.enabled = false;
            }
        }
        
        

    }
}
