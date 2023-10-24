using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class bursh : MonoBehaviour
{
    [SerializeField] private MySphere sphere;
    [SerializeField] private GameObject prefab;
    private Grid instance_grid;

    // Start is called before the first frame update
    void Start()
    {
        instance_grid = GetComponent<Grid>();

        sphere = new MySphere(3, Vector3.zero);

        BoundingBox bb = sphere.GetBoundingBox();


        for (float x = bb.lowerbound.x; x <= bb.upperbound.x; x += instance_grid.cellSize.x)
        {
            for (float y = bb.lowerbound.y; y <= bb.upperbound.y; y += instance_grid.cellSize.y)
            {
                for (float z = bb.lowerbound.z; z <= bb.upperbound.z; z += instance_grid.cellSize.z)
                {
                    Vector3 p = new Vector3(x, y, z);

                    if (sphere.IsInside(p))
                    {
                        GameObject pref = Instantiate(prefab, instance_grid.LocalToCell(p), Quaternion.identity);
                        pref.transform.SetParent(this.transform, false);
                        pref.GetComponent<burshcue>().val = Vector3.Dot(p,p);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(1, 0, 0);
    }
}
