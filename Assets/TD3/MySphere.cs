using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MySphere
{
    public float radius;
    public Vector3 center;
    // Start is called before the first frame update
    public MySphere(float radius, Vector3 center)
    {
        this.radius = radius;
        this.center = center;
    }

    // Methods
    public BoundingBox GetBoundingBox()
    {
        return new BoundingBox(center - Vector3.one * radius, center + Vector3.one * radius);
    }

    public bool IsInside(Vector3 p)
    {
        return Vector3.Dot(p-center, p-center) - radius * radius < 0;
    }
}
