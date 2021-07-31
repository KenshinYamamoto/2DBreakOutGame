using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    static public int stateNumber = 0; //ステートナンバー

    public AudioClip brickHit; //ブロックに当たった時の音
    public AudioClip wall_paddleHit; //壁とPaddleに当たった時の音
    public Text countDownText; //カウントダウンを表示するテキスト
    public GameObject resultText; //ClearかGameOverかを入れるテキスト

    private float initialVelocityY = 250f; //Y方向に打ち出す速度
    private Rigidbody2D rb2D; //RigidBody2Dを入れる変数
    private AudioSource audioSource; //AudioSourceを入れる変数
    private int initialVelocityX; //X方向に打ち出す速度
    private float countDown = 3.0f; //カウントダウン(3秒前)
    private float countUp = 0f; //カウントアップ
    private bool isCalled = false; //1回だけ実行するための判定

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); //RigidBody2Dを入れる
        audioSource = GetComponent<AudioSource>(); //AudioSourceを入れる
        countDownText.text = "3"; //3を表示する
        resultText.GetComponent<Text>().text = ""; //resultTextをリセット

        resultText.SetActive(false); //resultTextを非表示にする(デフォルト)
        
        initialVelocityX = Random.Range(-5, 6); //-5から5の範囲で抽選する
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime; //時間の更新

        switch (stateNumber) //ステートマシン
        {
            case 0: //待機
                {
                    if (countDown < 0.0f)
                    {
                        countDownText.text = ""; //カウントダウンを消す

                        stateNumber = 3; //case3に行く
                    }
                    else if (countDown < 1.0f)
                        stateNumber = 1; //case1に行く

                    else if (countDown < 2.0f)
                        stateNumber = 2; //case2に行く
                }
                break;

            case 1:
                {
                    countDownText.text = "1"; //1を表示する

                    stateNumber = 0; //case0に戻る
                }
                break;

            case 2:
                {
                    countDownText.text = "2"; //2を表示する

                    stateNumber = 0; //case0に戻る
                }
                break;

            case 3: //ゲームが動いているとき
                {
                    if (!SystemDaemon.isGameStarted)
                    {
                        transform.parent = null; //親子関係を切る
                        rb2D.isKinematic = false; //iskinematicのチェックを外す

                        rb2D.AddForce(new Vector2(initialVelocityX, initialVelocityY)); //ボールに力を加えて動かす

                        SystemDaemon.isGameStarted = true; //ゲームを動かす

                        countDown = 0f; //時間のリセット
                    }

                    if(GameDirectorController.brickQuantity == 0) //ブロックがなくなったら
                    {
                        stateNumber = 5; //case5に進む
                    }
                }
                break;

            case 4: //ゲームオーバー
                {
                    if (!isCalled)
                    {
                        SystemDaemon.isGameStarted = false; //ゲームを止める

                        resultText.GetComponent<Text>().color = new Color(0f, 0f, 1f, 1f);//字の色を青にする
                        resultText.GetComponent<Text>().text = "GAME OVER"; //GameOverを表示
                        resultText.SetActive(true); //resultTextを表示する

                        isCalled = true; //ロックをかける
                    }

                    countUp += Time.deltaTime; //時間の更新

                    if (countUp >= 2f) //2秒たったら
                    {
                        SystemDaemon.LoadScene("Title"); //タイトルに戻る

                        stateNumber = 0; //ステートナンバーのリセット
                    }
                }break;

            case 5: //クリア
                {
                    if (!isCalled)
                    {
                        SystemDaemon.isGameStarted = false; //ゲームを止める

                        resultText.GetComponent<Text>().color = new Color(1f, 1f, 0f, 1f);//字の色を黄にする
                        resultText.GetComponent<Text>().text = "CLEAR"; //Clearを表示
                        resultText.SetActive(true); //resultTextを表示する

                        switch (SystemDaemon.stageNumber) //ステージの番号による分岐
                        {
                            case 1: //ステージ1-1だったら
                                {
                                    SystemDaemon.gameData.clear1_1 = true;
                                }break;
                            case 2: //ステージ1-2だったら
                                {
                                    SystemDaemon.gameData.clear1_2 = true;
                                }
                                break;
                            case 3: //ステージ1-3だったら
                                {
                                    SystemDaemon.gameData.clear1_3 = true;
                                }break;
                            case 4: //ステージ1-4だったら
                                {
                                    SystemDaemon.gameData.clear1_4 = true;
                                }
                                break;
                            case 5: //ステージ1-5だったら
                                {
                                    SystemDaemon.gameData.clear1_5 = true;
                                }
                                break;
                            case 6: //ステージ1-6だったら
                                {
                                    SystemDaemon.gameData.clear1_6 = true;
                                }
                                break;
                            default:break;
                        }
                        isCalled = true; //ロックをかける
                    }

                    countUp += Time.deltaTime; //時間の更新

                    if (countUp >= 2f) //2秒たったら
                    {
                        SystemDaemon.LoadScene("Title"); //タイトルに戻る
                        stateNumber = 0; //ステートナンバーのリセット
                    }
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Brick")
        {
            audioSource.clip = brickHit; //brickHitの音をセットする
            audioSource.Play(); //音を再生する
            Destroy(collision.gameObject); //ブロックを壊す

            GameDirectorController.brickQuantity--; //ブロックを壊したら、1減らす
        }
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Paddle")
        {
            audioSource.clip = wall_paddleHit; //Wall_PaddleHitの音をセットする
            audioSource.Play(); //音を再生する
        }
    }
}
