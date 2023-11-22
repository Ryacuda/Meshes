using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subdivision : MonoBehaviour
{
    [SerializeField] private int nb_of_subdivisions;

    // Start is called before the first frame update
    void Start()
    {
        // get the points
        List<Vector3> points;

        Line l = gameObject.GetComponent<Line>();
        Polygon p = gameObject.GetComponent<Polygon>();

        if (l != null)
        {
            points = l.points;
        }
        else if (p != null)
        {
            points = p.points;
        }
        else
        {
            return;
        }

        // modify the list of points
        List<Vector3> new_points = new List<Vector3>();

        for (int s = 0; s < nb_of_subdivisions; s++)
        {
            for (int i = 1; i < points.Count; i++)
            {
                Vector3 p1 = points[i - 1];
                Vector3 p2 = points[i];

                new_points.Add(0.75f * p1 + 0.25f * p2);      // 
                new_points.Add(0.25f * p1 + 0.75f * p2);      //
            }

            if (p != null)
            {
                Vector3 p1 = points[points.Count - 1];
                Vector3 p2 = points[0];

                new_points.Add(0.75f * p1 + 0.25f * p2);      // 
                new_points.Add(0.25f * p1 + 0.75f * p2);      //
            }

            points.Clear();
            points.AddRange(new_points);
            new_points.Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
