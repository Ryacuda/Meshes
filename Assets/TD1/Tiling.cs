using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{
	public Material mat;

	// Start is called before the first frame update
	void Start()
	{
		int w = 5;
		int h = 6;

		float cell_width = 0.5f;
		float cell_height = 0.6f;

		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();

		Vector3[] vertices = new Vector3[(w+1) * (h+1)];
		int[] triangles = new int[h * w * 6];

		for(int i = 0; i <= w; i++)
		{
			for(int j = 0; j <= h; j++)
			{
				vertices[i * (h+1) + j] = new Vector3(i * cell_width, j * cell_height, 0);
			}
		}

		for(int i = 0 ; i < w; i++)
		{
			for(int j = 0; j < h; j++)
			{
				int v0 = i * (h + 1) + j;               //   v2 --- v3
				int v1 = i * (h + 1) + j + 1;			//   |  \    |
				int v2 = (i + 1) * (h + 1) + j;			//   |    \  |
				int v3 = (i + 1) * (h + 1) + j + 1;     //   v0 --- v1

				int ind = (i*h +j) * 6;

				triangles[ind] = v0;
				triangles[ind+1] = v1;
				triangles[ind+2] = v2;

				triangles[ind + 3] = v1;
				triangles[ind + 4] = v3;
				triangles[ind + 5] = v2;
			}
		}

		Mesh msh = new Mesh();

		msh.vertices = vertices;
		msh.triangles = triangles;

		gameObject.GetComponent<MeshFilter>().mesh = msh;
		gameObject.GetComponent<MeshRenderer>().material = mat;
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
