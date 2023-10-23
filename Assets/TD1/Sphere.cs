using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Sphere
{
	public Mesh mesh;
    private int n_longitudes = 15;
    private int n_latitudes = 6;
    private float radius = 2;

    // Start is called before the first frame update
    public Sphere()
	{
		Vector3[] vertices = new Vector3[n_longitudes  * (n_latitudes + 1) +2];
		int[] triangles = new int[(n_latitudes+1) * n_longitudes * 6];

		for (int i = 0; i < n_longitudes; i++)
		{
			float theta = -Mathf.PI * 2 * i / n_longitudes;

			for (int j = 0; j <= n_latitudes; j++)
			{
				float phi = Mathf.PI * (j+1) / (n_latitudes + 2);

				vertices[i * (n_latitudes + 1) + j] = new Vector3(Mathf.Cos(theta) * Mathf.Sin(phi),
														Mathf.Cos(phi),
														Mathf.Sin(phi) * Mathf.Sin(theta)) * radius;
			}
		}

        // north and south poles
        //cones at north and south poles
        int v_north = vertices.Length - 1;
        int v_south = vertices.Length - 2;
        vertices[v_south] = new Vector3(0, -radius, 0);
        vertices[v_north] = new Vector3(0, radius, 0);

		int ind = 0;
        for (int i = 0; i < n_longitudes; i++)
		{
			//sides
			for (int j = 0; j < n_latitudes; j++)
			{
				int v0 = i * (n_latitudes + 1) + j;               //   v2 --- v3
				int v1 = i * (n_latitudes + 1) + j + 1;           //   |  \    |
				int v2 = ((i + 1) % n_longitudes ) * (n_latitudes + 1) + j;         //   |    \  |
				int v3 = ((i + 1) % n_longitudes) * (n_latitudes + 1) + j + 1;     //   v0 --- v1

				triangles[ind++] = v0;
				triangles[ind++] = v1;
				triangles[ind++] = v2;

				triangles[ind++] = v1;
				triangles[ind++] = v3;
				triangles[ind++] = v2;
			}

			//cones at north and south poles
			int v_0 = i * (n_latitudes + 1) ;
			int v_1 = ((i + 1) % n_longitudes) * (n_latitudes + 1) ;

			triangles[ind++] = v_0;
			triangles[ind++] = v_1;
			triangles[ind++] = v_north;

            v_0 = i * (n_latitudes + 1) + n_latitudes;
			v_1 = ((i + 1) % n_longitudes) * (n_latitudes + 1) + n_latitudes;

            triangles[ind++] = v_1;
            triangles[ind++] = v_0;
            triangles[ind++] = v_south;
        }

		mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
	}
}