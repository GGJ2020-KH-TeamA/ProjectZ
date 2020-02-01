using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Color originColor = Gizmos.color;
        for (int i = 0; i < points.Count; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(points[i].position, 0.15f);
            if (i > 0)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(points[i - 1].position, points[i].position);
            }
        }
        Gizmos.color = originColor;
    }
}
