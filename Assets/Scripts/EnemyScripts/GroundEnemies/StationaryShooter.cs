using UnityEngine;
using System.Collections;

public class StationaryShooter : EnemyReqComp {

    GenericShootingScript shooter;

    protected override void Start()
    {
        base.Start();
        shooter = GetComponent<GenericShootingScript>();
    }

    protected override void Update()
    {
        base.Update();
        if(distanceToPlayer <= startActionDistance * startActionDistance)
        {
            shooter.Shoot();
        }
        
    }

	
}
