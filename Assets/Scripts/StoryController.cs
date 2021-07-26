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
    public GameObject misakiImage; //藤原みさきのImage
    public GameObject yukoImage; //神林ゆうこのImage
    public GameObject skipButton; //スキップボタン
    public Sprite[] kohakuImages; //大鳥こはくの絵を入れる配列　　　//0:ノーマル 1:目を閉じてる 2:怒ってる 3:驚いてる 4:笑ってる 5:苦笑い
    public Sprite[] misakiImages; //藤原みさきの絵を入れる配列      //0:ノーマル 1:目を閉じてる 2:怒ってる 3:驚いてる 4:困ってる 5:痛がってる
    public Sprite[] yukoImages; //神林ゆうこの絵を入れる配列        //0:ノーマル 1:目を閉じてる 2:驚いてる 3:怒ってる 4:困ってる 5:恥ずかしがってる？

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
        "こんにちは!", //こはく
        "私の名前は大鳥こはく!\nよろしくね～", //こはく
        "こんにちは!", //みさき
        "私は藤原みさきっていいま～す", //みさき
        "こんにちは", //ゆうこ
        "神林ゆうこです", //ゆうこ
        "このゲームをプレイしてくれてありがとね～\n単純なゲームだけど、楽しんでくれたら嬉しいな。", //こはく
        "え、こはくがこのゲーム作ったの?", //みさき
        "ううん。今のはゲーム制作主の言葉だよ。", //こはく
        "あぁ、なるほど...", //ゆうこ
        "ゲーム制作主曰く、単純なゲームなんだけどそこそこ難しいらしいよ。", //こはく
        "どんなゲームなの?", //みさき
        "よくあるブロック崩しゲームだね。", //こはく
        "タイトルにもそう書いてあったしね...", //ゆうこ
        "話はさておき、早速プレイしていこうよ!", //こはく
        "そうだね", //みさき＆ゆうこ
        "そうそう。この会話をもう一度見たかったらタイトルで「もう1度ストーリーを見る」を押すと見られるよ。", //こはく
        "もう1度見たい人なんていないでしょ..." //ゆうこ
    };


    void Start()
    {
        attentionImage.SetActive(false); //AttentionImageを非表示にする(デフォルト)
        deltaArrow.SetActive(false); //DeltaArrowTextを非表示にする(デフォルト)
        kohakuImage.SetActive(false); //KohakuImageを非表示にする(デフォルト)
        yukoImage.SetActive(false); //YukoImageを非表示にする(デフォルト)
        misakiImage.SetActive(false); //MisakiImageを非表示にする(デフォルト)

        kohakuImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(350f, 0f); //位置を更新(デフォルト)
        misakiImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(350f, 0f); //位置を更新(デフォルト)
        yukoImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(350f, 0f); //位置を更新(デフォルト)

        if (!SystemDaemon.isFromStoryButton) //もし、「ストーリを見る」ボタンから来ていなかったら
        {
            skipButton.SetActive(false); //スキップボタンを表示しない
        }
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

                            if (SystemDaemon.isFromStoryButton) //「ストーリーを見る」ボタンから来ていたら
                            {
                                SystemDaemon.LoadScene("Title"); //タイトルに戻る
                                SystemDaemon.isFromStoryButton = false; //isFromStoryButtonをリセット
                            }
                            else
                            {
                                SystemDaemon.LoadScene("GameScene"); //ゲームシーンに行く
                            }
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
        switch (serifNumber)
        {
            case 0: //「こんにちは!」(こはく)
                {
                    KohakuImageChange(4); //こはくが笑っている絵にする
                    NameChange("???"); //名前を更新
                    kohakuImage.SetActive(true); //こはくの絵を表示する
                }break;

            case 1: //「私の名前は大鳥こはく!\nよろしくね～」(こはく)
                {
                    KohakuImageChange(0); //こはくのノーマル絵にする
                    NameChange("こはく"); //名前を更新
                }break;

            case 2://「こんにちは!」(みさき)
                {
                    kohakuImage.SetActive(false); //こはくの絵を非表示にする
                    MisakiImageChange(0); //みさきのノーマルの絵にする
                    NameChange("???"); //名前を更新
                    misakiImage.SetActive(true); //みさきの絵を表示する
                }break;

            case 3: //「私は藤原みさきっていいま～す」(みさき)
                {
                    NameChange("みさき"); //名前を更新
                }break;

            case 4: //「こんにちは」(ゆうこ)
                {
                    misakiImage.SetActive(false); //みさきの絵を非表示にする
                    YukoImageChange(0); //ゆうこが驚いてる絵にする
                    NameChange("???"); //名前を更新
                    yukoImage.SetActive(true); //ゆうこの絵を表示する
                }
                break;

            case 5: //「神林ゆうこです」(ゆうこ)
                {
                    NameChange("ゆうこ"); //名前を更新
                }break;

            case 6: //「このゲームをプレイしてくれてありがとね～\n単純なゲームだけど、楽しんでくれたら嬉しいな。」(こはく)
                {
                    yukoImage.SetActive(false); //ゆうこの絵を非表示にする
                    KohakuImageChange(4); //こはくが笑っている絵にする
                    NameChange("こはく"); //名前を更新
                    kohakuImage.SetActive(true); //こはくの絵を表示する
                }
                break;

            case 7:　//「え、こはくがこのゲーム作ったの?」(みさき)
                {
                    MisakiImageChange(3); //みさきが驚いている絵にする
                    KohakuImageChange(0); //こはくのノーマルの絵にする
                    misakiImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-350f, 0f); //位置を更新
                    misakiImage.GetComponent<RectTransform>().rotation = new Quaternion(0f, 180f, 0f, 1f); //向きを更新
                    NameChange("みさき"); //名前を更新
                    misakiImage.SetActive(true); //みさきの絵を表示する
                }break;

            case 8: //「ううん。今のはゲーム制作主の言葉だよ。」(こはく)
                {
                    MisakiImageChange(0); //みさきをノーマルの絵にする
                    KohakuImageChange(1); //こはくが目を閉じてる絵にする
                    NameChange("こはく");
                }break;

            case 9: //「あぁ、なるほど...」(ゆうこ)
                {
                    misakiImage.SetActive(false); //みさきの絵を非表示にする
                    KohakuImageChange(0); //こはくのノーマルの絵にする
                    yukoImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-350f, 0f); //位置を更新
                    yukoImage.GetComponent<RectTransform>().rotation = new Quaternion(0f, 180f, 0f, 1f); //向きを更新
                    NameChange("ゆうこ"); //名前を更新
                    yukoImage.SetActive(true); //ゆうこの絵を表示する
                }break;

            case 10: //「ゲーム制作主曰く、単純なゲームなんだけどそこそこ難しいらしいよ。」(こはく)
                {
                    MisakiImageChange(5); //こはくの苦笑いの絵にする
                    NameChange("こはく"); //名前を更新
                }break;

            case 11: //「どんなゲームなの?」(みさき)
                {
                    yukoImage.SetActive(false); //ゆうこの絵を非表示にする
                    KohakuImageChange(0); //こはくのノーマルの絵にする
                    MisakiImageChange(4); //みさきを困っている絵にする
                    NameChange("みさき"); //名前を更新
                    misakiImage.SetActive(true); //みさきの絵を表示する
                }break;

            case 12: //「よくあるブロック崩しゲームだね。」(こはく)
                {
                    MisakiImageChange(0); //みさきのノーマルの絵にする
                    NameChange("こはく"); //名前を更新
                }break;

            case 13: //「タイトルにもそう書いてあったしね...」(ゆうこ)
                {
                    misakiImage.SetActive(false); //みさきの絵を非表示にする
                    NameChange("ゆうこ"); //名前を更新
                    yukoImage.SetActive(true); //ゆうこの絵を表示する
                }break;

            case 14: //「話はさておき、早速プレイしていこうよ!」(こはく)
                {
                    KohakuImageChange(4); //こはくが笑っている絵にする
                    NameChange("こはく"); //名前を更新
                }
                break;

            case 15: //「そうだね」(みさき&ゆうこ)
                {
                    MisakiImageChange(1); //みさきが目を閉じてる絵にする
                    YukoImageChange(1); //ゆうこが目を閉じてる絵にする
                    KohakuImageChange(0); //こはくのノーマルの絵にする
                    misakiImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-462f, 0f); //位置を更新
                    yukoImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-286f, 0f); //位置を更新
                    NameChange("みさき&ゆうこ"); //名前を更新
                    misakiImage.SetActive(true); //みさきの絵を表示する
                }
                break;

            case 16: //「そうそう。この会話をもう一度見たかったらタイトルで「もう1度ストーリーを見る」を押すと見られるよ。」(こはく)
                {
                    misakiImage.SetActive(false); //みさきの絵を非表示にする
                    yukoImage.SetActive(false); //ゆうこの絵を非表示にする
                    KohakuImageChange(4); //こはくが笑っている絵にする
                    NameChange("こはく"); //名前を更新
                    kohakuImage.SetActive(true); //こはくの絵を表示する
                }
                break;

            case 17: //「もう1度見たい人なんていないでしょ...」(ゆうこ)
                {
                    KohakuImageChange(0); //こはくのノーマルの絵にする
                    YukoImageChange(4); //ゆうこが困っている絵にする
                    yukoImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-350f, 0f); //位置を更新
                    NameChange("ゆうこ"); //名前を更新
                    yukoImage.SetActive(true); //ゆうこの絵を表示する
                }
                break;

            default: break;
        }
    }

    void KohakuImageChange(int imageNumber) //こはくの絵を変更する関数
    {
        kohakuImage.GetComponent<Image>().sprite = kohakuImages[imageNumber]; //こはくの絵を更新
    }
    void MisakiImageChange(int imageNumber) //みさきの絵を変更する関数
    {
        misakiImage.GetComponent<Image>().sprite = misakiImages[imageNumber]; //みさきの絵を更新
    }
    void YukoImageChange(int imageNumber) //ゆうこの絵を変更する関数
    {
        yukoImage.GetComponent<Image>().sprite = yukoImages[imageNumber]; //ゆうこの絵を更新
    }
    void NameChange(string name) //名前を変更する関数
    {
        nameText.GetComponent<Text>().text = name; //名前を更新
    }

    public void OnSkipButton() //スキップボタンが押されたら
    {
        isStopped = true; //止める

        attentionImage.SetActive(true); //attentionImageを表示する
    }

    public void OnYesButton() //「はい」ボタンが押されたら
    {
        SystemDaemon.LoadScene("Title"); //タイトルに戻る
    }

    public void OnNoButton() //「いいえ」ボタンが押されたら
    {
        isStopped = false; //動かす

        attentionImage.SetActive(false); //attentionImageを非表示にする
    }
}
