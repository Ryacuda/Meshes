using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadrique
{
    public float a;
    public float f;
    public float e;
    public float b;
    public float d;
    public float c;

    // Constructor
    public Quadrique(float a, float f, float e, float b, float d, float c)
    {
        this.a = a;
        this.f = f;
        this.e = e;
        this.b = b;
        this.d = d;
        this.c = c;
    }

    // Methods
    public bool IsInside(in Vector3 p)
    {
        return a * p.x * p.x + b * p.y * p.y + c * p.z * p.z + 2 * (d * p.z * p.y + e * p.x * p.z + f * p.x * p.y) < 0;
    }
}
