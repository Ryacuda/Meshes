using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hello_Triangle : MonoBehaviour
{
	public Material mat;

	// Start is called before the first frame update
	void Start()
	{
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();

		Vector3[] vertices = new Vector3[4];
		int[] triangles = new int[6];

		vertices[0] = new Vector3(0, 0, 0);
		vertices[1] = new Vector3(1, 0, 0);
		vertices[2] = new Vector3(0, 1, 0);
		vertices[3] = new Vector3(1, 1, 0);

		triangles[0] = 0;
		triangles[1] = 1;
		triangles[2] = 2;

		triangles[3] = 3;
		triangles[4] = 2;
		triangles[5] = 1;

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
