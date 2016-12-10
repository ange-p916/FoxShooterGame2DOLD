using UnityEngine;
using System.Collections;

public class EnemyReqComp : MonoBehaviour {

    protected Vector3 dirToTarget;
    protected float dstToTarget;
    protected RaycastHit2D targetInView;
    protected Rigidbody2D rb2d;
    protected Vector2 whatSideIsPlayerAt;
    protected PlayablePlayer player;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsGround;

    [Header("Distances")]
    public float startActionDistance;
    public float distanceToPlayer;

    protected virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        player = FindObjectOfType<PlayablePlayer>();
        
    }

    protected virtual void Update()
    {
        whatSideIsPlayerAt = (player.transform.position - this.transform.position).x > 0 ? Vector2.right : Vector2.left;
        distanceToPlayer = (player.transform.position - this.transform.position).sqrMagnitude;
    }

    protected virtual void CheckLoS()
    {
        targetInView = Physics2D.CircleCast(transform.position, startActionDistance, Vector2.zero, startActionDistance, WhatIsPlayer);
        dirToTarget = (player.transform.position - this.transform.position).normalized;
        dstToTarget = Vector3.Distance(transform.position, player.transform.position);
    }

}
