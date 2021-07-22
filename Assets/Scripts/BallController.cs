using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float initialVelocityY = 200.0f; //Y方向に打ち出す速度
    public AudioClip brickHit; //ブロックに当たった時の音
    public AudioClip wall_paddleHit; //壁とPaddleに当たった時の音
    public Text countDownText; //カウントダウンを表示するテキスト

    private Rigidbody2D rb2D; //RigidBody2Dを入れる変数
    private AudioSource audioSource; //AudioSourceを入れる変数
    private float initialVelocityX; //X方向に打ち出す速度
    private int twoNumber; //打ち出す方向を左か右か決める変数
    private float countDown = 3.0f; //カウントダウンする変数
    private int stateNumber = 0; //ステートナンバー
    //private bool isCalled = false; //1回だけ実行するための判定

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); //RigidBody2Dを入れる
        audioSource = GetComponent<AudioSource>(); //AudioSourceを入れる
        countDownText.text = "3"; //3を表示する

        twoNumber = Random.Range(0, 2); //0か1をtwoNumberに入れる

        switch (twoNumber)
        {
            case 0: //左方向に打ち出そう
                {
                    initialVelocityX = Random.Range(-initialVelocityY * 2, -initialVelocityY / 2); //-20から-5の範囲で抽選する
                }break;

            case 1: //右方向に打ち出そう
                {
                    initialVelocityX = Random.Range(initialVelocityY / 2, initialVelocityY * 2); //5から20の範囲で抽選する
                }break;

            default: break;
        }
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
        }
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Paddle")
        {
            audioSource.clip = wall_paddleHit; //Wall_PaddleHitの音をセットする
            audioSource.Play(); //音を再生する
        }
    }
}
