using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveSceneController : MonoBehaviour
{
    public GameObject saveButton; //セーブボタン
    public GameObject loadButton; //ロードボタン
    public GameObject goTitleButton; //タイトルに戻るボタン
    public GameObject informImage; //知らせるイメージ
    public GameObject saveAttentionImage; //注意書きのイメージ
    public GameObject loadAttentionImage; //注意書きのイメージ
    public Text informText; //知らせるテキスト
    

    private string filePath;
    private bool isInformed = false; //知らせるかどうか
    private float informCount = 0f; //知らせる時間
    private AudioSource audioSource; //AudioSource

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".gamedata.json";
    }

    private void Start()
    {
        informImage.SetActive(false); //知らせるイメージを非表示(デフォルト)
        saveAttentionImage.SetActive(false); //セーブするかどうかのイメージを非表示(デフォルト)
        loadAttentionImage.SetActive(false); //ロードするかどうかのイメージを非表示(デフォルト)

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isInformed)
        {
            informCount += Time.deltaTime;
            informImage.SetActive(true);

            if(informCount >= 1f)
            {
                informImage.SetActive(false);
                informCount = 0f;
                isInformed = false;
            }
        }
    }

    public void OnSaveButton()
    {
        //SEを鳴らす
        audioSource.PlayOneShot(audioSource.clip);

        //各種ボタンを非表示にする
        saveButton.SetActive(false);
        loadButton.SetActive(false);
        goTitleButton.SetActive(false);

        saveAttentionImage.SetActive(true); //セーブするかどうかのイメージを表示する
    }

    public void SaveYesButton()
    {
        //SEを鳴らす
        audioSource.PlayOneShot(audioSource.clip);

        string json = JsonUtility.ToJson(SystemDaemon.gameData);

        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();

        informText.text = "セーブしました";
        isInformed = true;

        //各種ボタンを表示する
        saveButton.SetActive(true);
        loadButton.SetActive(true);
        goTitleButton.SetActive(true);

        saveAttentionImage.SetActive(false); //セーブするかどうかのイメージを非表示にする
    }

    public void SaveNoButton()
    {
        //SEを鳴らす
        audioSource.PlayOneShot(audioSource.clip);

        //各種ボタンを表示する
        saveButton.SetActive(true);
        loadButton.SetActive(true);
        goTitleButton.SetActive(true);

        saveAttentionImage.SetActive(false); //セーブするかどうかのイメージを非表示にする
    }


    public void OnLoadButton()
    {
        //SEを鳴らす
        audioSource.PlayOneShot(audioSource.clip);

        //各種ボタンを非表示にする
        saveButton.SetActive(false);
        loadButton.SetActive(false);
        goTitleButton.SetActive(false);

        loadAttentionImage.SetActive(true); //ロードするかどうかのイメージを表示する
    }

    public void LoadYesButton()
    {
        //SEを鳴らす
        audioSource.PlayOneShot(audioSource.clip);

        if (File.Exists(filePath))
        {
            StreamReader streamReader = new StreamReader(filePath);

            string data = streamReader.ReadToEnd();
            streamReader.Close();

            SystemDaemon.gameData = JsonUtility.FromJson<SystemDaemon.GameData>(data);

            informText.text = "ロードしました";
            isInformed = true;

            //各種ボタンを表示する
            saveButton.SetActive(true);
            loadButton.SetActive(true);
            goTitleButton.SetActive(true);

            loadAttentionImage.SetActive(false); //ロードするかどうかのイメージを非表示にする
        }
    }

    public void LoadNoButton()
    {
        //SEを鳴らす
        audioSource.PlayOneShot(audioSource.clip);

        //各種ボタンを表示する
        saveButton.SetActive(true);
        loadButton.SetActive(true);
        goTitleButton.SetActive(true);

        loadAttentionImage.SetActive(false); //ロードするかどうかのイメージを非表示にする
    }

    public void GoTitleButton()
    {
        SystemDaemon.LoadScene("Title");
    }
}
