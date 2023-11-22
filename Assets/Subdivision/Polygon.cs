using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour
{
    [SerializeField] public List<Vector3> points;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 1; i < points.Count; i++)
        {
            Gizmos.DrawLine(transform.position + points[i - 1], transform.position + points[i]);
        }

        Gizmos.DrawLine(transform.position + points[points.Count - 1], transform.position + points[0]);
    }
}
