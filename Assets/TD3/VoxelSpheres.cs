using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoxelSpheres : MonoBehaviour
{
	[SerializeField] private List<Quadrique> spheres;
	[SerializeField] private GameObject prefab;
	private Grid instance_grid;

	// Start is called before the first frame update
	void Start()
	{
		instance_grid = GetComponent<Grid>();

		spheres = new List<Quadrique>();

		spheres.Add(new Quadrique(1, 1, 1, -1, -1, -1));

		BoundingBox bb = new BoundingBox(- 10 * Vector3.one, 10 * Vector3.one);
		/*
		for(int i = 1; i < spheres.Count; i++)
		{
			bb = BoundingBox.Union(bb, spheres[i].GetBoundingBox());
		}
		*/

		if(bb == null)
		{
			return;
		}

		int c = 0;

        for (float x = bb.lowerbound.x; x <= bb.upperbound.x; x += instance_grid.cellSize.x)
		{
			for (float y = bb.lowerbound.y; y <= bb.upperbound.y; y+= instance_grid.cellSize.y)
			{
				for(float z = bb.lowerbound.z; z <= bb.upperbound.z; z+= instance_grid.cellSize.z)
				{
                    Vector3 p = new Vector3(x, y, z);

                    foreach (Quadrique torus in spheres)
					{
                        if (/*torus.IsInside(p)*/ true)
                        {
							GameObject pref = Instantiate(prefab, instance_grid.LocalToCell(p), Quaternion.identity);
                            pref.GetComponent<CubePotentiel>().UpdateN(15);
                            pref.GetComponent<CubePotentiel>().UpdateThreshold(11);
                            c++;
                        }
                    }
				}
			}
		}

		Debug.Log(c);
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
