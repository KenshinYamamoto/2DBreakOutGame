using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveSceneController : MonoBehaviour
{
    public GameObject saveButton; //�Z�[�u�{�^��
    public GameObject loadButton; //���[�h�{�^��
    public GameObject goTitleButton; //�^�C�g���ɖ߂�{�^��
    public GameObject informImage; //�m�点��C���[�W
    public GameObject saveAttentionImage; //���ӏ����̃C���[�W
    public GameObject loadAttentionImage; //���ӏ����̃C���[�W
    public Text informText; //�m�点��e�L�X�g
    

    private string filePath;
    private bool isInformed = false; //�m�点�邩�ǂ���
    private float informCount = 0f; //�m�点�鎞��
    private AudioSource audioSource; //AudioSource

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".gamedata.json";
    }

    private void Start()
    {
        informImage.SetActive(false); //�m�点��C���[�W���\��(�f�t�H���g)
        saveAttentionImage.SetActive(false); //�Z�[�u���邩�ǂ����̃C���[�W���\��(�f�t�H���g)
        loadAttentionImage.SetActive(false); //���[�h���邩�ǂ����̃C���[�W���\��(�f�t�H���g)

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
        //SE��炷
        audioSource.PlayOneShot(audioSource.clip);

        //�e��{�^�����\���ɂ���
        saveButton.SetActive(false);
        loadButton.SetActive(false);
        goTitleButton.SetActive(false);

        saveAttentionImage.SetActive(true); //�Z�[�u���邩�ǂ����̃C���[�W��\������
    }

    public void SaveYesButton()
    {
        //SE��炷
        audioSource.PlayOneShot(audioSource.clip);

        string json = JsonUtility.ToJson(SystemDaemon.gameData);

        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();

        informText.text = "�Z�[�u���܂���";
        isInformed = true;

        //�e��{�^����\������
        saveButton.SetActive(true);
        loadButton.SetActive(true);
        goTitleButton.SetActive(true);

        saveAttentionImage.SetActive(false); //�Z�[�u���邩�ǂ����̃C���[�W���\���ɂ���
    }

    public void SaveNoButton()
    {
        //SE��炷
        audioSource.PlayOneShot(audioSource.clip);

        //�e��{�^����\������
        saveButton.SetActive(true);
        loadButton.SetActive(true);
        goTitleButton.SetActive(true);

        saveAttentionImage.SetActive(false); //�Z�[�u���邩�ǂ����̃C���[�W���\���ɂ���
    }


    public void OnLoadButton()
    {
        //SE��炷
        audioSource.PlayOneShot(audioSource.clip);

        //�e��{�^�����\���ɂ���
        saveButton.SetActive(false);
        loadButton.SetActive(false);
        goTitleButton.SetActive(false);

        loadAttentionImage.SetActive(true); //���[�h���邩�ǂ����̃C���[�W��\������
    }

    public void LoadYesButton()
    {
        //SE��炷
        audioSource.PlayOneShot(audioSource.clip);

        if (File.Exists(filePath))
        {
            StreamReader streamReader = new StreamReader(filePath);

            string data = streamReader.ReadToEnd();
            streamReader.Close();

            SystemDaemon.gameData = JsonUtility.FromJson<SystemDaemon.GameData>(data);

            informText.text = "���[�h���܂���";
            isInformed = true;

            //�e��{�^����\������
            saveButton.SetActive(true);
            loadButton.SetActive(true);
            goTitleButton.SetActive(true);

            loadAttentionImage.SetActive(false); //���[�h���邩�ǂ����̃C���[�W���\���ɂ���
        }
    }

    public void LoadNoButton()
    {
        //SE��炷
        audioSource.PlayOneShot(audioSource.clip);

        //�e��{�^����\������
        saveButton.SetActive(true);
        loadButton.SetActive(true);
        goTitleButton.SetActive(true);

        loadAttentionImage.SetActive(false); //���[�h���邩�ǂ����̃C���[�W���\���ɂ���
    }

    public void GoTitleButton()
    {
        SystemDaemon.LoadScene("Title");
    }
}
