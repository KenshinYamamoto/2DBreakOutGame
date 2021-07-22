using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		// シーン リセット
		SystemDaemon.SceneReset();   
    }

    // Update is called once per frame
    void Update()
    {
        // エンター？
		if( Input.GetKeyDown( KeyCode.Return))
		{
			// シーン ローディング
			LoadingController.Scene = "Game";
			SystemDaemon.LoadScene( "Loading");
		}         
    }
}
