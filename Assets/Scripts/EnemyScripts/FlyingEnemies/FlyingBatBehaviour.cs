using UnityEngine;
using System.Collections;

public class FlyingBatBehaviour : EnemyBehaviourTemplate {

    FlyingBatEnemy bat;

    protected override void Start()
    {
        base.Start();
        bat = GetComponent<FlyingBatEnemy>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        bat = GetComponent<FlyingBatEnemy>();
        //bat.anim = bat.GetComponent<Animator>();
        bat.startToChase = false;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        bat.startToChase = true;
        bat.chasingTimer = bat.newCDChasingTimer;
    }
}
