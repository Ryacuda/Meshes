using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burshcue : MonoBehaviour
{
    public float val;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("e");
    }
}
