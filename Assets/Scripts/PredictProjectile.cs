using UnityEngine;
using System.Collections;

public class PredictProjectile : MonoBehaviour {
    
    public LineRenderer lineR;

    public static PredictProjectile Instance;

    [Header("Trajectory stuff")]
    public Vector2 startVelocity;
    public int segs = 5;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
    }

    Vector2 PlotTrajectoryAtTime(Vector2 start, Vector2 startVel, float time)
    {
        return start + startVel * time + Physics2D.gravity * time * time * 0.5f;
    }

    public void PlotTrajectory(Vector2 start, Vector2 startVel, float timestep, float maxTime)
    {
        Vector2 prev = start;
        for (int i = 1;; i++)
        {
            float t = timestep * i;
            if (t > maxTime) break;
            Vector2 pos = PlotTrajectoryAtTime(start, startVel, t);
            if (Physics2D.Linecast(prev, pos)) break;
            Debug.DrawLine(prev, pos, Color.red);
            //DrawStuff(prev, pos);
            prev = pos;
        }
    }

    public void Parabola(Vector2 pStart, Vector2 pVel)
    {
        float velocity = Mathf.Sqrt((pVel.x * pVel.x) + (pVel.y * pVel.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVel.y, pVel.x));
        float fTime = 0;

        fTime += 0.1f;

        for (int i = 0; i < segs; i++)
        {
            lineR.SetVertexCount(segs);
            lineR.SetPosition(0, pStart);
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2f);
            Vector2 pos = new Vector2(pStart.x + dx, pStart.y + dy);
            lineR.SetPosition(i, pos);
            fTime += 0.1f;
        }
    }
}
