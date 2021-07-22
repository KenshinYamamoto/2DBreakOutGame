using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
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
			// シーン遷移（ローディング無し）
			SystemDaemon.LoadScene( "Another");
		}
    }
}
