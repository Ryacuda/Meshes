using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylindre : MonoBehaviour
{
	public Material mat;

	// Start is called before the first frame update
	void Start()
	{
		int n = 20;
		float h = 2;
		float r = 3 ;

		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();

		Vector3[] vertices = new Vector3[n * 2];
		int[] triangles = new int[24 * (n-1)];

		// vertices
		for (int i = 0; i < n; i++)
		{
			float a = Mathf.PI * 2 * i / n;
			vertices[i] = new Vector3(Mathf.Cos(a), 0, Mathf.Sin(a)) * r;
			vertices[n+i] = new Vector3(Mathf.Cos(a), h, Mathf.Sin(a)) * r;

			Debug.Log(a);
		}

		// caps
		for(int i = 0; i < n-2; i++)
		{
			int v0 = 0;
			int v1 = i+1;
			int v2 = i+2;

			triangles[i * 3] = v0;
			triangles[i * 3 + 1] = v1;
			triangles[i * 3 + 2] = v2;

			triangles[(n + i) * 3] = v0+n;
			triangles[(n + i) * 3 + 1] = v2+n;
			triangles[(n + i) * 3 + 2] = v1+n;
		}

		// sides
		for (int i = 0; i < n; i++)
		{
			int v0 = i;                 //   v2 --- v3
			int v1 = (i + 1) % n;       //   |    /  |
			int v2 = n + i;             //   |  /    |
			int v3 = n + (i + 1) % n;   //   v0 --- v1

			int offset = 6 * (n - 1);

			triangles[offset + i * 6] = v0;
			triangles[offset + i * 6 + 1] = v3;
			triangles[offset + i * 6 + 2] = v1;

			triangles[offset + i * 6 + 3] = v0;
			triangles[offset + i * 6 + 4] = v2;
			triangles[offset + i * 6 + 5] = v3;
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