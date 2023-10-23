using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoxelSpheres : MonoBehaviour
{
	[SerializeField] private List<Torus> spheres;
	private Grid instance_grid;

	// Start is called before the first frame update
	void Start()
	{
		instance_grid = GetComponent<Grid>();

		spheres = new List<Torus>();

		spheres.Add(new Torus(40, 2, Vector3.zero));
        spheres.Add(new Torus(10, 9, Vector3.zero));

        BoundingBox bb = spheres[0].GetBoundingBox();
		for(int i = 1; i < spheres.Count; i++)
		{
			bb = BoundingBox.Union(bb, spheres[i].GetBoundingBox());
		}

		if(bb == null)
		{
			return;
		}

        for (float x = bb.lowerbound.x; x <= bb.upperbound.x; x++)
		{
			for (float y = bb.lowerbound.y; y <= bb.upperbound.y; y++)
			{
				for(float z = bb.lowerbound.z; z <= bb.upperbound.z; z++)
				{
                    Vector3 p = new Vector3(x, y, z);

                    foreach (Torus torus in spheres)
					{
                        if (torus.IsInside(p))
                        {
                            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            cube.transform.position = instance_grid.LocalToCell(p);
                        }
                    }
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
