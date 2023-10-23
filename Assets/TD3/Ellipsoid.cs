using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ellipsoid
{
    public Vector3 radii;
    public Vector3 center;
    // Start is called before the first frame update
    public Ellipsoid(Vector3 radii, Vector3 center)
    {
        this.radii = radii;
        this.center = center;
    }

    // Methods
    public BoundingBox GetBoundingBox()
    {
        return new BoundingBox(center - radii, center + radii);
    }

    public bool IsInside(Vector3 p)
    {
        Vector3 pp = p - center;
        pp.x /= radii.x;
        pp.y /= radii.y;
        pp.z /= radii.z;

        return Vector3.Dot(pp, pp) - 1  < 0;
    }
}
