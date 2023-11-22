using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using System;
using System.Linq;

public class MeshLoader : MonoBehaviour
{
	[SerializeField] private Material material;
	[SerializeField] private bool center;
	[SerializeField] private bool normalize;
	[SerializeField] private string mesh_file;
	private Mesh mesh;

	// Start is called before the first frame update
	void Start()
	{
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();

		ReadOFFFile("Assets/TD2/" + mesh_file + ".off");

        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshRenderer>().material = material;

        if (center)
		{
			CenterVertices();
		}

		if (normalize)
		{
			NormalizeSize();
		}

		mesh.RecalculateNormals();

		PropertiesLog("Assets/TD2/plog.txt");

		Write("tiles.off");
        //WriteHalf("Assets/TD2/bdh.off");
    }

	// Update is called once per frame
	void Update()
	{
		
	}

	public void ReadOFFFile(string path)
	{
		StreamReader streamReader = new StreamReader(path);

		int n_vertices = 0;
		int n_faces = 0;
		int n_edges = 0;

		string line = streamReader.ReadLine();
		if(line != "OFF")
		{
			Debug.Log("first line isn't OFF");
			return;
		}
 
		if(!streamReader.EndOfStream)
		{
			line = streamReader.ReadLine();

			string[] n = line.Split(" ");

			n_vertices = int.Parse(n[0]);
			n_faces = int.Parse(n[1]);
			n_edges = int.Parse(n[2]);

			Debug.Log(n_vertices);
			Debug.Log(n_faces);
			Debug.Log(n_edges);
		}

		Vector3[] vertices = new Vector3[n_vertices];
		for(int i = 0; i < n_vertices; i++)
		{
			line = streamReader.ReadLine();

			string[] str_coords = line.Split(" ");

			vertices[i] = new Vector3(float.Parse(str_coords[0], CultureInfo.InvariantCulture), float.Parse(str_coords[1], CultureInfo.InvariantCulture), float.Parse(str_coords[2], CultureInfo.InvariantCulture));
		}

		int[] triangles = new int[3 * n_faces];
		for (int i = 0; i < n_faces; i++)
		{
			line = streamReader.ReadLine();

			string[] s = line.Split(" ");
			int n = int.Parse(s[0]);

			for (int j = 0; j < n; j++)
			{
				triangles[3*i + j] = int.Parse(s[j + 1]);
			}
		}

		streamReader.Close();

		mesh = new Mesh();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
	}

	public void CenterVertices()
	{
		Vector3 avg = Vector3.zero;

		Vector3[] vertices = mesh.vertices;

		foreach(Vector3 v in vertices)
		{
			avg += v;
		}

		avg /= vertices.Length;

		for(int i = 0; i < vertices.Length; i++)
		{
			vertices[i] -= avg;
		}

		mesh.vertices = vertices;
	}

	public void NormalizeSize()
	{
		float max = 0;
		Vector3[] vertices = mesh.vertices;

		foreach (Vector3 v in vertices)
		{
			if(Mathf.Abs(v.x) > max)
			{
				max = Mathf.Abs(v.x);
			}

			if (Mathf.Abs(v.y) > max)
			{
				max = Mathf.Abs(v.y);
			}

			if (Mathf.Abs(v.y) > max)
			{
				max = Mathf.Abs(v.y);
			}
		}

		for (int i = 0; i < vertices.Length; i++)
		{
			vertices[i] /= max;
		}

		mesh.vertices = vertices;
	}

	public void WriteHalf(string path)
	{
		StreamWriter sw = new StreamWriter(path);

		sw.WriteLine("OFF");
		sw.WriteLine((mesh.vertices.Length).ToString() + " " + (mesh.triangles.Length / 6).ToString() + " 0");

		for(int i = 0; i < mesh.vertices.Length; i++)
		{
			Vector3 p = mesh.vertices[i];
			sw.WriteLine(p.x.ToString(CultureInfo.InvariantCulture) + " " + p.y.ToString(CultureInfo.InvariantCulture) + " " + p.z.ToString(CultureInfo.InvariantCulture));
		}

		for (int i = 0; i < mesh.triangles.Length / 6; i++)
		{
			sw.WriteLine("3 " + mesh.triangles[6*i] + " " + mesh.triangles[6*i + 1] + " " + mesh.triangles[6*i + 2]);
		}

		sw.Close();
	}

    public void Write(string path)
    {
        StreamWriter sw = new StreamWriter(path);

        sw.WriteLine("OFF");
        sw.WriteLine((mesh.vertices.Length).ToString() + " " + (mesh.triangles.Length / 3).ToString() + " 0");

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Vector3 p = mesh.vertices[i];
            sw.WriteLine(p.x.ToString(CultureInfo.InvariantCulture) + " " + p.y.ToString(CultureInfo.InvariantCulture) + " " + p.z.ToString(CultureInfo.InvariantCulture));
        }

        for (int i = 0; i < mesh.triangles.Length / 3; i++)
        {
            sw.WriteLine("3 " + mesh.triangles[3 * i] + " " + mesh.triangles[3 * i + 1] + " " + mesh.triangles[3 * i + 2]);
        }

        sw.Close();
    }

    public void PropertiesLog(string path)
	{
		Dictionary<Vector2Int, int> edge_count = new Dictionary<Vector2Int, int>();
		//List<int> vertex_count = new List<int>();
		//vertex_count.Capacity = mesh.vertices.Length;

		for(int i = 0; i < mesh.triangles.Length/3; i++)
		{
			int a = mesh.triangles[3 * i];
            int b = mesh.triangles[3 * i + 1];
            int c = mesh.triangles[3 * i + 1];

			// order a b and c so that a <= b <= c
			if(a > c)
			{
				(a, c) = (c, a);
			}

			if(a > b)
			{
				(a, b) = (b, a);
			}

			if(b > c)
			{
				(b, c) = (c, b);
			}

            Vector2Int e1 = new Vector2Int(a, b);
            Vector2Int e2 = new Vector2Int(b, c);
            Vector2Int e3 = new Vector2Int(a, c);

			// increment edge count
            int val = 0;
			edge_count.TryGetValue(e1, out val);
			edge_count[e1] = val + 1;

			val = 0;
            edge_count.TryGetValue(e2, out val);
			edge_count[e2] = val + 1;

            val = 0;
            edge_count.TryGetValue(e3, out val);
            edge_count[e3] = val + 1;

			// increment vertex count
        }

		int min_shared_edge = edge_count.OrderBy(e => e.Value).First().Value;
        int max_shared_edge = edge_count.OrderBy(e => e.Value).Last().Value;

		//int min_shared_vertex = vertex_count.OrderBy(v => v.Value).First().Value;
		//int max_shared_vertex = vertex_count.OrderBy(v => v.Value).First().Value;

        StreamWriter sw = new StreamWriter(path);

		sw.WriteLine("Number of vertices/edges/faces : " + mesh.vertices.Length.ToString(CultureInfo.InvariantCulture) + "\t" + edge_count.Count.ToString(CultureInfo.InvariantCulture) + "\t" + (mesh.triangles.Length/3).ToString(CultureInfo.InvariantCulture));

        sw.WriteLine("\n\t\t\t\t\tmin\t\tmax");
        sw.WriteLine("Shared edges :\t\t" + min_shared_edge.ToString(CultureInfo.InvariantCulture) + "\t\t" + max_shared_edge.ToString(CultureInfo.InvariantCulture));
        //sw.WriteLine("Shared vertices :\t" + min_shared_vertex.ToString(CultureInfo.InvariantCulture) + "\t\t" + max_shared_vertex.ToString(CultureInfo.InvariantCulture));

        sw.Close();
    }
}
