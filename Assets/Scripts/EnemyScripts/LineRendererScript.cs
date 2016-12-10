using UnityEngine;
using System.Collections;

public class LineRendererScript : MonoBehaviour {
    public float segLength = 2;
    public LineRenderer lineR;

    void Start()
    {
        DrawStuff(Vector2.zero, new Vector2(15f, 5f));
    }

    public void DrawStuff(Vector2 start, Vector2 end)
    {
        float dist = Vector2.Distance(start, end);
        int segs = 5;
        if(dist > segLength)
        {
            segs = Mathf.FloorToInt(dist / segLength) + 2;
        }
        else
        {
            segs = 4;
        }

        for (int i = 1; i < segs - 1; i++)
        {
            lineR.SetVertexCount(segs);
            lineR.SetPosition(0, start);

            var lastPos = start;

            var tmp = Vector2.Lerp(start, end, (float)i / (float)segs);

            lastPos = new Vector2(tmp.x, tmp.y);

            lineR.SetPosition(i, lastPos);
        }
        lineR.SetPosition(segs - 1, end);
    }

}
