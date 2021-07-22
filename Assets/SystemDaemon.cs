//********************************************************************************
// システム常駐（監視）
//
// このSystemDaemonは、シーンが変わってもDontDestroyOnLoad();で常駐し続けています。
// シーンごとにSystemDaemonがありますが、最初のシーンのが常駐する仕掛けになっています。
// ※シングルトンと云う、クラスのインスタンスが1つしか生成されないことを保証する方法です。
// 
// static publicで宣言した変数・関数は他のクラスから非常に簡単に直接アクセスができます。
// 変数：SystemDaemon.TimeCounter / 関数：SystemDaemon.SceneReset();
//
// 但し、これは保持され続けるという特殊な変数なので、宣言での初期化はバグの原因になります。
// Start()内で忘れずに初期化をしてください ※簡単すぎてこの変数を多用するとよく忘れます。
//********************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scene
using UnityEngine.SceneManagement;

//================================================================================
// MonoBehaviour 継承 ※シングルトン
//================================================================================

public class SystemDaemon : SingletonMonoBehaviour<SystemDaemon>
{
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	// ワーク
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

	// 経過時間(秒)
	static public float TimeCounter;

	// 起動の時刻
	static public string TimeStamp = "19700101-000000";

	// シーン遷移元
	static public string BackScene = "";

	// シーン遷移先
	static public string NextScene = "";

	// シーン現在地
	static public string HereScene = "";

	// 以下に、シーンを跨いで保存したい変数を入れる。例：スコアなど
	//static public int Score;

	static public bool isGameStarted = false; //ゲームが始まっているかどうか

	static public bool isStoried = false; //ストーリーを再生したかどうか

	static public int stageNumber; //ステージの番号(1～6)

	//ステージをクリアしているかどうか
	static public bool clear1_1 = false;
	static public bool clear1_2 = false;
	static public bool clear1_3 = false;
	static public bool clear1_4 = false;
	static public bool clear1_5 = false;
	static public bool clear1_6 = false;

	//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	// アウェイク ※ここは特殊なので慣れるまで変更・追加しないでください
	//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

	void Awake() {
        // すでに存在してたら破棄
        if( this != Instance) {
            // 破棄
            Destroy( gameObject);
            return;
        }

        // 常駐する
        DontDestroyOnLoad( gameObject);

		// 起動の時刻
		TimeStamp = System.DateTime.Now.Year.ToString( "D4") + System.DateTime.Now.Month.ToString( "D2") + System.DateTime.Now.Day.ToString( "D2") + "-" +
			System.DateTime.Now.Hour.ToString( "D2") + System.DateTime.Now.Minute.ToString( "D2") + System.DateTime.Now.Second.ToString( "D2");
	}

	//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	// スタート ※ここでstatic public変数の初期化（代入）をしてください
	//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    void Start()
    {
		// タイマー クリアー
		TimeCounter = 0.0f;
		
		// 例：スコアの初期化
		//Score = 0;
    }

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	// アップデート ※ここで何らかのシステム的な監視をする処理をします。
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    void Update()
    {
		// タイマー
		TimeCounter += Time.deltaTime;
    }

    //--------------------------------------------------------------------------------
    // シーン切り替え
    //--------------------------------------------------------------------------------

	static public void LoadScene( string scenename) {
		// シーン遷移元
		BackScene = SceneManager.GetActiveScene().name;

		// シーン遷移先
		NextScene = scenename;

		// シーンロード
		SceneManager.LoadScene( scenename);
	}

    //--------------------------------------------------------------------------------
    // シーンリセット
    //--------------------------------------------------------------------------------

	static public void SceneReset() {
		// シーン遷移先
		NextScene = "";

		// シーン現在地
		HereScene = SceneManager.GetActiveScene().name;

		// タイマー クリアー
		TimeCounter = 0.0f;
	}
}
