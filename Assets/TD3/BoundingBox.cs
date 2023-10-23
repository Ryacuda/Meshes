using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox
{
    public Vector3 lowerbound;
    public Vector3 upperbound;

    // Constructor
    public BoundingBox(Vector3 lowerbound, Vector3 upperbound)
    {
        this.lowerbound = lowerbound;
        this.upperbound = upperbound;
    }

    // Methods
    public static BoundingBox Union(in BoundingBox bb1, in BoundingBox bb2)
    {
        return new BoundingBox(new Vector3(Mathf.Min(bb1.lowerbound.x, bb2.lowerbound.x), Mathf.Min(bb1.lowerbound.y, bb2.lowerbound.y), Mathf.Min(bb1.lowerbound.z, bb2.lowerbound.z)),
                               new Vector3(Mathf.Max(bb1.upperbound.x, bb2.upperbound.x), Mathf.Max(bb1.upperbound.y, bb2.upperbound.y), Mathf.Max(bb1.upperbound.z, bb2.upperbound.z)));
    }

    public static BoundingBox Intersection(in BoundingBox bb1, in BoundingBox bb2)
    {
        return new BoundingBox(new Vector3(Mathf.Max(bb1.lowerbound.x, bb2.lowerbound.x), Mathf.Max(bb1.lowerbound.y, bb2.lowerbound.y), Mathf.Max(bb1.lowerbound.z, bb2.lowerbound.z)),
                               new Vector3(Mathf.Min(bb1.upperbound.x, bb2.upperbound.x), Mathf.Min(bb1.upperbound.y, bb2.upperbound.y), Mathf.Min(bb1.upperbound.z, bb2.upperbound.z)));
    }
}
