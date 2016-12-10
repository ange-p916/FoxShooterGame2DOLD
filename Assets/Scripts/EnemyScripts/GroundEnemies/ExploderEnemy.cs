using UnityEngine;
using System.Collections;

public class ExploderEnemy : EnemyBehaviourTemplate {


    protected override void Start()
    {
        base.Start();
    }

    //protected override void Update()
    //{
    //    base.Update();
    //    if(distanceToPlayer <= startActionDistance * startActionDistance)
    //    {
    //        gsc.state = GenericStateController.WhatStateIsThis.EXPLODER;
    //    }
    //    else
    //    {
    //        gsc.state = GenericStateController.WhatStateIsThis.IDLE;
    //    }
    //}
}
