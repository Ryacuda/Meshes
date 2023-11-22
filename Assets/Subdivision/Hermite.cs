using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hermite : MonoBehaviour
{
    [SerializeField] private int nb_points;
    [SerializeField] private Vector3 p1;
    [SerializeField] private Vector3 p2;
    [SerializeField] private Vector3 v1;
    [SerializeField] private Vector3 v2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private float F1(float t)
    {
        float t2 = t * t;
        return 2 * t2 * t - 3 * t2 + 1;
    }

    private float F2(float t)
    {
        float t2 = t * t;
        return -2 * t2 * t + 3 * t2;
    }

    private float F3(float t)
    {
        float t2 = t * t;
        return t2 * t - 2 * t2 + t;
    }

    private float F4(float t)
    {
        float t2 = t * t;
        return t2 * t - t2;
    }

    private Vector3 PointAt(float t)
    {
        return F1(t) * p1 + F2(t) * p2 + F3(t) * v1 + F4(t) * v2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 vi1 = PointAt(0);
        Vector3 vi2 = PointAt(1f / nb_points);

        for (int i = 1; i < nb_points; i++)
        {
            Gizmos.DrawLine(transform.position + vi1, transform.position + vi2);

            vi1 = vi2;
            vi2 = PointAt((i + 1f) / nb_points);
        }
    }
}
