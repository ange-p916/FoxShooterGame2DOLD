using UnityEngine;
using System.Collections;

public class SlugEnemy : EnemyReqComp {

    public Vector2 moveDir;
    public Vector2 moveSpeed;

    GenericShootingScript shoot;

    protected override void Start()
    {
        base.Start();
        shoot = GetComponent<GenericShootingScript>();
    }

    protected override void Update()
    {
        base.Update();
        if(distanceToPlayer <= startActionDistance * startActionDistance)
        {
            DoSlugStuff();
        }
        
    }

    void DoSlugStuff()
    {
        rb2d.velocity = whatSideIsPlayerAt.x * moveSpeed;
        shoot.Shoot();
    }

}
