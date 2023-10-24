using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePotentiel : MonoBehaviour
{ 
    [SerializeField] private float n;
    private float threshold;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateN(float n)
    {
        this.n = n;

        if(n < threshold)
        {
            GetComponent<Renderer>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
        }
    }

    public void UpdateThreshold(float t)
    {
        threshold = t;

        if (n < threshold)
        {
            GetComponent<Renderer>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.CompareTag("Brush"))
        {
            UpdateN(n - other.GetComponent<burshcue>().val);
        }
    }
}
