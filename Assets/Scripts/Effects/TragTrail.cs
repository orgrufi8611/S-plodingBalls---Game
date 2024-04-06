using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DragTrail : MonoBehaviour
{
    LineRenderer line;
    // Start is called before the first frame update
    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 start, Vector3 end)
    {
        line.positionCount = 2;
        Vector3[] points = new Vector3[line.positionCount];
        points[0] = start;
        points[1] = end;
        line.SetPositions(points);
    }

    public void EndLine()
    {
        line.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
