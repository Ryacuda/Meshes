using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone
{
    public Mesh mesh;
    private int n = 4;
    private float radius = 2;
    private float hauteur = 2;


    // Start is called before the first frame update
    public Cone()
    {
        Vector3[] vertices = new Vector3[2 * n - 1];
        int[] triangles = new int[6*(n-1)];

        int ind = 0;
        for (int i = 0; i < n; i++)
        {
            float theta = -Mathf.PI * 2 * i / n;

            vertices[ind++] = new Vector3(Mathf.Cos(theta),0,Mathf.Sin(theta)) * radius;
        }

        // north and south poles
        //cones at north and south poles
        int v_north = vertices.Length - 1;
        vertices[v_north] = new Vector3(0, hauteur, 0);

        ind = 0;

        //sides
        for (int i = 0; i < n; i++)
        {
            //cones at north and south poles
            int v_0 = i;
            int v_1 = ((i + 1) % n);

            triangles[ind++] = v_0;
            triangles[ind++] = v_1;
            triangles[ind++] = v_north;
        }

        // caps
        for (int i = 0; i < n - 2; i++)
        {
            int v0 = 0;
            int v1 = i + 1;
            int v2 = i + 2;

            triangles[ind++] = v1;
            triangles[ind++] = v0;
            triangles[ind++] = v2;
        }

        mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
