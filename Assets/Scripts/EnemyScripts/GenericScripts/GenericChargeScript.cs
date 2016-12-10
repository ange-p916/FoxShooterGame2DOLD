using UnityEngine;
using System.Collections;

public class GenericChargeScript : EnemyReqComp {

    public bool countDownStart = false;
    public bool isCharging = false;
    public float timeIsCharging;
    public float chargeSpeed;
    public float countdownTimeToCharge;
    public float startToChargeDist;
    public float newCDCharge;
    public float newChargeTimer;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void Charge()
    {
        countdownTimeToCharge -= Time.deltaTime;
        if(countdownTimeToCharge <= 0)
        {
            isCharging = true;
            timeIsCharging -= Time.deltaTime;
            if(isCharging)
            {
                transform.localScale = new Vector3(whatSideIsPlayerAt.x * 1f, 1f, 1f);
                rb2d.velocity = whatSideIsPlayerAt * chargeSpeed;
            }
            if(timeIsCharging <= 0)
            {
                isCharging = false;
                countdownTimeToCharge = newCDCharge;
            }
            if(!isCharging && countdownTimeToCharge >= 0)
            {
                timeIsCharging = newChargeTimer;
            }
        }
    }
}
