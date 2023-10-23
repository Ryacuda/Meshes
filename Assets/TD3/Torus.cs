using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Torus
{
    public float ring_radius;
    public float small_ring_radius;
    public Vector3 center;

    // Start is called before the first frame update
    public Torus(float R, float r, Vector3 center)
    {
        ring_radius = R;
        small_ring_radius = r;
        this.center = center;
    }

    // Methods
    public BoundingBox GetBoundingBox()
    {
        Vector3 custom_dim = new Vector3(ring_radius + small_ring_radius, small_ring_radius, ring_radius + small_ring_radius);
        return new BoundingBox(center - custom_dim, center + custom_dim);
    }

    public bool IsInside(Vector3 p)
    {
        Vector3 pp = p - center;
        float a = Mathf.Sqrt(pp.x * pp.x + pp.z * pp.z) - ring_radius;

        return a * a + pp.y * pp.y - small_ring_radius * small_ring_radius < 0;
    }
}
