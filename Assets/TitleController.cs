using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
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
			SystemDaemon.LoadScene( "Loading");
		}
    }
}
