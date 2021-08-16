using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleScene : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            SystemDaemon.LoadScene("Title");
        }
    }
}
