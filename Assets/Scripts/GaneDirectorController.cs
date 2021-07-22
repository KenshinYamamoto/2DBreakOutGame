using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaneDirectorController : MonoBehaviour
{
    public GameObject[] prefabs;
    void Start()
    {
        GameObject go = Instantiate(prefabs[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
