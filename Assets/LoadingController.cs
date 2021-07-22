//********************************************************************************
// シーン ローディング
//********************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Canvas
using UnityEngine.UI;
// Scene
using UnityEngine.SceneManagement;

//================================================================================
// MonoBehaviour 継承
//================================================================================

public class LoadingController : MonoBehaviour
{	
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // ワーク
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

	// シーン遷移先
	static public string Scene = "Game";

	// 読み込みの状況
    private AsyncOperation Condition;

	// プログレス
	public GameObject Progress;

	//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	// スタート
	//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    void Start()
    {
		// ローディング コルーチン
		StartCoroutine( LoadScene());

		// シーン アクティブ コルーチン
		StartCoroutine( SceneWait());      
    }

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	// アップデート
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

	void Update() {
		// 何もしない
	}

    //--------------------------------------------------------------------------------
	// シーン アクティブ ※ウエイト付き
    //--------------------------------------------------------------------------------

	IEnumerator SceneWait() {
		// ウエイト 
        yield return new WaitForSeconds( 1.0f);

		// シーン アクティブを行う
		Condition.allowSceneActivation= true;

		// シーン遷移先
		SystemDaemon.NextScene = Scene;
    }

    //--------------------------------------------------------------------------------
    // ローディング コルーチン
    //--------------------------------------------------------------------------------

    IEnumerator LoadScene() {
		// シーン読み込み開始
        Condition = SceneManager.LoadSceneAsync( Scene);
		// シーン切り替えを止め
		Condition.allowSceneActivation= false;

        while( !Condition.isDone) {
			// 進捗率を求める
			float value = (Condition.progress / 0.9f) * 100.0f;
		
			// Canvasの更新
			Progress.GetComponent<Text>().text = "Now Loading ... " + value.ToString( "F1") + "%";

			// ウェイト
			yield return new WaitForSeconds( 0.0f);
        }
    }
}
