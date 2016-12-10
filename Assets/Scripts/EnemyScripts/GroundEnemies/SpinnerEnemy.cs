using UnityEngine;
using System.Collections;

public class SpinnerEnemy : EnemyReqComp {

    public float acceleration = 5f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        rb2d.velocity = whatSideIsPlayerAt;
        Spin();
    }

    void Spin()
    {
        rb2d.velocity = rb2d.velocity * acceleration;
        acceleration += Time.deltaTime * acceleration;
        acceleration = Mathf.Clamp(acceleration, -50f, 50f);
    }

    Vector2 MoveLerpThingy(Vector2 vel, Vector2 from, Vector2 to)
    {
        vel = rb2d.velocity;

        from = transform.position;
        to = from - to;

        return to;
    }
}
