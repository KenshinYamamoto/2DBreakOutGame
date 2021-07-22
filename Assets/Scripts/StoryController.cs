using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    public GameObject serifText; //セリフのテキスト
    public GameObject attentionImage; //スキップしてよいかどうかの確認
    public GameObject deltaArrow; //矢印
    public GameObject nameText; //しゃべっているキャラの名前を入れるテキスト
    public GameObject kohakuImage; //大鳥こはくのImage
    public GameObject yukoImage; //神林ゆうこのImage
    public GameObject misakiImage; //藤原みさきのImage
    public Sprite[] kohakuImages; //大鳥こはくの絵を入れる配列　　　//0:ノーマル 1:目を閉じてる 2:怒ってる 3:驚いてる 4:笑ってる 5:苦笑い
    public Sprite[] yukoImages; //神林ゆうこの絵を入れる配列        //0:ノーマル 1:目を閉じてる 2:驚いてる 3:怒ってる 4:困ってる 5:恥ずかしがってる？
    public Sprite[] misakiImages; //藤原みさきの絵を入れる配列      //0:ノーマル 1:目を閉じてる 2:怒ってる 3:驚いてる 4:困ってる 5:痛がってる

    private int serifNumber; //どのセリフを表示するかという変数
    private string displaySerif; //表示させる文字
    private int serifCharNumber; //何文字目を表示するかという変数
    private int speed; //表示させる速度
    public int displayspeed;
    private bool isClicked = false; //クリックしたかどうかの判定
    private bool isStopped = false; //止めるかどうかの判定
    private bool isCharClicked = false; //セリフの文字を最後まで飛ばすかどうかの判定

    private string[] serifs =
    {
        "こんにちは!",
        "私の名前は大鳥こはく!\nよろしくね～",
        "私は藤原みさきっていいま～す",
        "私は神林ゆうこです",
        "このゲームをプレイしてくれてありがとね～\n単純なゲームだけど、楽しんでくれたら嬉しいな。",
        "え、こはくがこのゲーム作ったの?",
        "ううん。今のはゲーム制作主の言葉だよ。",
        "あぁ、なるほど...",
        "ゲーム制作主曰く、単純なゲームなんだけどそこそこ難しいらしいよ。",
        "どんなゲームなの?",
        "よくあるブロック崩しゲームだね。",
        "話はさておき、早速プレイしていこうよ!",
        "そうだね",
        "そうそう。この会話をもう一度聞きたかったらタイトルで「もう1度ストーリーを見る」を押すと見られるよ。",
        "もう1度見たい人なんていないでしょ..."
    };


    void Start()
    {
        attentionImage.SetActive(false); //AttentionImageを非表示にする(デフォルト)
        deltaArrow.SetActive(false); //DeltaArrowTextを非表示にする(デフォルト)
        kohakuImage.SetActive(false); //KohakuImageを非表示にする(デフォルト)
        yukoImage.SetActive(false); //YukoImageを非表示にする(デフォルト)
        misakiImage.SetActive(false); //MisakiImageを非表示にする(デフォルト)
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped) //stopがかかっていないとき
        {
            speed++;
            if (speed % displayspeed == 0) //表示する速度を落とす
            {
                if (serifCharNumber != serifs[serifNumber].Length) //serifs[serifNumber]の最後の文字でなければ
                {
                    if (!isCharClicked) //文字の追加中
                    {
                        displaySerif += serifs[serifNumber][serifCharNumber]; //1文字ずつ追加していく

                        serifCharNumber++; //次の文字にする
                    }
                    else //文字の追加中に左クリックされたら
                    {
                        displaySerif = serifs[serifNumber]; //文字列全てを表示する
                        serifCharNumber = serifs[serifNumber].Length; //文字の最後にする
                    }
                }
                else //serifs[serifNumber]が最後の文字だったら
                {
                    deltaArrow.SetActive(true); //矢印を表示する

                    if (serifNumber != serifs.Length - 1) //serifs[]が最後のセリフでなかったら
                    {
                        if (isClicked) //クリックされた判定がtrueだったら
                        {
                            displaySerif = ""; //表示している文字列を消す
                            serifCharNumber = 0; //文字の番号を最初にする
                            serifNumber++; //次のセリフにする
                            deltaArrow.SetActive(false); //矢印を非表示にする
                        }
                    }
                    else //serifs[]が最後だったら
                    {
                        if (isClicked) //クリックされた判定がtrueだったら
                        {
                            displaySerif = ""; //表示している文字列を消す
                            serifCharNumber = 0; //文字の番号を最初にする
                            isStopped = true; //止める

                            SystemDaemon.LoadScene("otherScene");
                        }
                    }
                }
                serifText.GetComponent<Text>().text = displaySerif; //displaySerifを表示
                isClicked = false; //クリックされた判定をfalseに変更
                isCharClicked = false; //クリックされた判定をfalseに変更
            }

            if (Input.GetMouseButtonDown(0) || Input.anyKeyDown) //クリックされたら
            {
                isClicked = true; //クリックされた判定をtrueにする
                isCharClicked = true; //クリックされた判定をtrueにする
            }
        }
    }
}
