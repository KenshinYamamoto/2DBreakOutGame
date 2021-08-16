using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject goSelectionButton; //ステージ選択画面に行くボタン
    public GameObject goRuleSceneButton; //ルールシーンに行くボタン
    public GameObject goSaveAndLoadSceneButton; //セーブ&ロードシーンに行くボタン
    public GameObject goStorySceneButton; //ストーリーシーンに行くボタン
    public GameObject attentionImage; //「解放されていません」を表示するパネル
    public GameObject panel1_1; //1-1Panelを入れる変数
    public GameObject panel1_2; //1-2Panelを入れる変数
    public GameObject panel1_3; //1-3Panelを入れる変数
    public GameObject panel1_4; //1-4Panelを入れる変数
    public GameObject panel1_5; //1-5Panelを入れる変数
    public GameObject panel1_6; //1-6Panelを入れる変数
    public GameObject button1_1; //1-1Buttonを入れる変数
    public GameObject button1_2; //1-2Buttonを入れる変数
    public GameObject button1_3; //1-3Buttonを入れる変数
    public GameObject button1_4; //1-4Buttonを入れる変数
    public GameObject button1_5; //1-5Buttonを入れる変数
    public GameObject button1_6; //1-6Buttonを入れる変数
    public GameObject backButton; //BackButtonを入れる変数
    public GameObject titlePanel; //TitlePanelを入れる変数
    public Sprite[] titleSprites; //TitleSpritesを入れる配列
    public AudioClip[] audioClips; //SEを入れる配列

    private bool isAttentioned = false;
    private float attentionCount = 0.0f;
    private AudioSource audioSourse;

    void Start()
    {
        if (!SystemDaemon.gameData.isStoried) //1度もストーリーが再生されていなかったら
        {
            goStorySceneButton.SetActive(false); //ストーリーシーンに行くボタンを非表示にする
        }
        else
        {
            goStorySceneButton.SetActive(true); //ストーリーシーンに行くボタンを表示する
        }

        //ボタンの非表示(デフォルト)
        button1_1.SetActive(false);
        button1_2.SetActive(false);
        button1_3.SetActive(false);
        button1_4.SetActive(false);
        button1_5.SetActive(false);
        button1_6.SetActive(false);

        attentionImage.SetActive(false); //AttentionPanelを非表示にする(デフォルト)
        backButton.SetActive(false); //BackButtonを非表示にする(デフォルト)

        audioSourse = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isAttentioned)
        {
            attentionCount += Time.deltaTime;

            attentionImage.SetActive(true);
            if(attentionCount >= 1.0f)
            {
                attentionImage.SetActive(false);
                isAttentioned = false;
            }
        }
    }

    public void OnGoSelectionButton() //ステージ選択画面に行くボタンが押されたら
    {
        //SEを鳴らす
        audioSourse.PlayOneShot(audioClips[0]);

        //ボタンの非表示
        goSelectionButton.SetActive(false);
        goRuleSceneButton.SetActive(false);
        goSaveAndLoadSceneButton.SetActive(false);
        goStorySceneButton.SetActive(false);

        //ステージのボタンの表示
        button1_1.SetActive(true);
        button1_2.SetActive(true);
        button1_3.SetActive(true);
        button1_4.SetActive(true);
        button1_5.SetActive(true);
        button1_6.SetActive(true);

        backButton.SetActive(true); //BackButtonの表示
        titlePanel.GetComponent<Image>().sprite = titleSprites[1]; //CourseSelectの絵に切り替える

        //クリアしてなかったら「済」を非表示にして、クリアしてたら表示させる
        if (!SystemDaemon.gameData.clear1_1)
            panel1_1.SetActive(false);
        else
            button1_2.GetComponent<Image>().color = Color.white;

        if (!SystemDaemon.gameData.clear1_2)
            panel1_2.SetActive(false);
        else
            button1_3.GetComponent<Image>().color = Color.white;

        if (!SystemDaemon.gameData.clear1_3)
            panel1_3.SetActive(false);
        else
            button1_4.GetComponent<Image>().color = Color.white;

        if (!SystemDaemon.gameData.clear1_4)
            panel1_4.SetActive(false);
        else
            button1_5.GetComponent<Image>().color = Color.white;

        if (!SystemDaemon.gameData.clear1_5)
            panel1_5.SetActive(false);
        else
            button1_6.GetComponent<Image>().color = Color.white;

        if (!SystemDaemon.gameData.clear1_6)
            panel1_6.SetActive(false);
    }

    public void On1_1Button() //1-1ボタンを押したとき
    {
        SystemDaemon.stageNumber = 1; //ステージの番号を1に設定

        if (!SystemDaemon.gameData.isStoried)
        {
            SystemDaemon.gameData.isStoried = true;

            LoadingController.Scene = "StoryScene";
            SystemDaemon.LoadScene("Loading");
        }
        else
            SystemDaemon.LoadScene("GameScene");
    }

    public void On1_2Button() //1-2ボタンを押したとき
    {
        if (!SystemDaemon.gameData.clear1_1)
        {
            isAttentioned = true;

            attentionCount = 0.0f;
        }
        else
        {
            SystemDaemon.stageNumber = 2; //ステージの番号を2に設定

            SystemDaemon.LoadScene("GameScene");
        }
    }

    public void On1_3Button() //1-3ボタンを押したとき
    {
        if (!SystemDaemon.gameData.clear1_2)
        {
            isAttentioned = true;

            attentionCount = 0.0f;
        }
        else
        {
            SystemDaemon.stageNumber = 3; //ステージの番号を3に設定

            SystemDaemon.LoadScene("GameScene");
        }
    }

    public void On1_4Button() //1-4ボタンを押したとき
    {
        if (!SystemDaemon.gameData.clear1_3)
        {
            isAttentioned = true;

            attentionCount = 0.0f;
        }
        else
        {
            SystemDaemon.stageNumber = 4; //ステージの番号を4に設定

            SystemDaemon.LoadScene("GameScene");
        }
    }

    public void On1_5Button() //1-5ボタンを押したとき
    {
        if (!SystemDaemon.gameData.clear1_4)
        {
            isAttentioned = true;

            attentionCount = 0.0f;
        }
        else
        {
            SystemDaemon.stageNumber = 5; //ステージの番号を5に設定

            SystemDaemon.LoadScene("GameScene");
        }
    }

    public void On1_6Button() //1-6ボタンを押したとき
    {
        if (!SystemDaemon.gameData.clear1_5)
        {
            isAttentioned = true;

            attentionCount = 0.0f;
        }
        else
        {
            SystemDaemon.stageNumber = 6; //ステージの番号を6に設定

            SystemDaemon.LoadScene("GameScene");
        }
    }

    public void OnGoRuleSceneButton() //ルールシーンに行くボタン
    {
        SystemDaemon.LoadScene("RuleScene");
    }

    public void OnGoStorySceneButton() //ストーリーシーンに行くボタン
    {
        SystemDaemon.isFromStoryButton = true;

        LoadingController.Scene = "StoryScene";
        SystemDaemon.LoadScene("Loading");
    }

    public void OnBackButton() //戻るボタン
    {
        //SEを鳴らす
        audioSourse.PlayOneShot(audioClips[1]);

        //ステージのボタンの非表示
        button1_1.SetActive(false);
        button1_2.SetActive(false);
        button1_3.SetActive(false);
        button1_4.SetActive(false);
        button1_5.SetActive(false);
        button1_6.SetActive(false);

        backButton.SetActive(false); //BackButtonの非表示
        titlePanel.GetComponent<Image>().sprite = titleSprites[0]; //Titletの絵に切り替える

        //ボタンの表示
        goSelectionButton.SetActive(true);
        goRuleSceneButton.SetActive(true);
        goSaveAndLoadSceneButton.SetActive(true);

        if (SystemDaemon.gameData.isStoried) //1度もストーリーが再生されていなかったら
        {
            goStorySceneButton.SetActive(true); //ストーリーシーンに行くボタンを表示する
        }
    }

    public void OnSaveAndLoadButton() //セーブ&ロードシーンに行くボタン
    {
        SystemDaemon.LoadScene("SaveAndLoadScene");
    }
}
