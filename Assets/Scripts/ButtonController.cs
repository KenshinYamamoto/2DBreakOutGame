using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject goSelectionButton; //�X�e�[�W�I����ʂɍs���{�^��
    public GameObject goRuleSceneButton; //���[���V�[���ɍs���{�^��
    public GameObject goSaveAndLoadSceneButton; //�Z�[�u&���[�h�V�[���ɍs���{�^��
    public GameObject goStorySceneButton; //�X�g�[���[�V�[���ɍs���{�^��
    public GameObject attentionImage; //�u�������Ă��܂���v��\������p�l��
    public GameObject panel1_1; //1-1Panel������ϐ�
    public GameObject panel1_2; //1-2Panel������ϐ�
    public GameObject panel1_3; //1-3Panel������ϐ�
    public GameObject panel1_4; //1-4Panel������ϐ�
    public GameObject panel1_5; //1-5Panel������ϐ�
    public GameObject panel1_6; //1-6Panel������ϐ�
    public GameObject button1_1; //1-1Button������ϐ�
    public GameObject button1_2; //1-2Button������ϐ�
    public GameObject button1_3; //1-3Button������ϐ�
    public GameObject button1_4; //1-4Button������ϐ�
    public GameObject button1_5; //1-5Button������ϐ�
    public GameObject button1_6; //1-6Button������ϐ�
    public GameObject backButton; //BackButton������ϐ�
    public GameObject titlePanel; //TitlePanel������ϐ�
    public Sprite[] titleSprites; //TitleSprites������z��
    public AudioClip[] audioClips; //SE������z��

    private bool isAttentioned = false;
    private float attentionCount = 0.0f;
    private AudioSource audioSourse;

    void Start()
    {
        if (!SystemDaemon.gameData.isStoried) //1�x���X�g�[���[���Đ�����Ă��Ȃ�������
        {
            goStorySceneButton.SetActive(false); //�X�g�[���[�V�[���ɍs���{�^�����\���ɂ���
        }
        else
        {
            goStorySceneButton.SetActive(true); //�X�g�[���[�V�[���ɍs���{�^����\������
        }

        //�{�^���̔�\��(�f�t�H���g)
        button1_1.SetActive(false);
        button1_2.SetActive(false);
        button1_3.SetActive(false);
        button1_4.SetActive(false);
        button1_5.SetActive(false);
        button1_6.SetActive(false);

        attentionImage.SetActive(false); //AttentionPanel���\���ɂ���(�f�t�H���g)
        backButton.SetActive(false); //BackButton���\���ɂ���(�f�t�H���g)

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

    public void OnGoSelectionButton() //�X�e�[�W�I����ʂɍs���{�^���������ꂽ��
    {
        //SE��炷
        audioSourse.PlayOneShot(audioClips[0]);

        //�{�^���̔�\��
        goSelectionButton.SetActive(false);
        goRuleSceneButton.SetActive(false);
        goSaveAndLoadSceneButton.SetActive(false);
        goStorySceneButton.SetActive(false);

        //�X�e�[�W�̃{�^���̕\��
        button1_1.SetActive(true);
        button1_2.SetActive(true);
        button1_3.SetActive(true);
        button1_4.SetActive(true);
        button1_5.SetActive(true);
        button1_6.SetActive(true);

        backButton.SetActive(true); //BackButton�̕\��
        titlePanel.GetComponent<Image>().sprite = titleSprites[1]; //CourseSelect�̊G�ɐ؂�ւ���

        //�N���A���ĂȂ�������u�ρv���\���ɂ��āA�N���A���Ă���\��������
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

    public void On1_1Button() //1-1�{�^�����������Ƃ�
    {
        SystemDaemon.stageNumber = 1; //�X�e�[�W�̔ԍ���1�ɐݒ�

        if (!SystemDaemon.gameData.isStoried)
        {
            SystemDaemon.gameData.isStoried = true;

            LoadingController.Scene = "StoryScene";
            SystemDaemon.LoadScene("Loading");
        }
        else
            SystemDaemon.LoadScene("GameScene");
    }

    public void On1_2Button() //1-2�{�^�����������Ƃ�
    {
        if (!SystemDaemon.gameData.clear1_1)
        {
            isAttentioned = true;

            attentionCount = 0.0f;
        }
        else
        {
            SystemDaemon.stageNumber = 2; //�X�e�[�W�̔ԍ���2�ɐݒ�

            SystemDaemon.LoadScene("GameScene");
        }
    }

    public void On1_3Button() //1-3�{�^�����������Ƃ�
    {
        if (!SystemDaemon.gameData.clear1_2)
        {
            isAttentioned = true;

            attentionCount = 0.0f;
        }
        else
        {
            SystemDaemon.stageNumber = 3; //�X�e�[�W�̔ԍ���3�ɐݒ�

            SystemDaemon.LoadScene("GameScene");
        }
    }

    public void On1_4Button() //1-4�{�^�����������Ƃ�
    {
        if (!SystemDaemon.gameData.clear1_3)
        {
            isAttentioned = true;

            attentionCount = 0.0f;
        }
        else
        {
            SystemDaemon.stageNumber = 4; //�X�e�[�W�̔ԍ���4�ɐݒ�

            SystemDaemon.LoadScene("GameScene");
        }
    }

    public void On1_5Button() //1-5�{�^�����������Ƃ�
    {
        if (!SystemDaemon.gameData.clear1_4)
        {
            isAttentioned = true;

            attentionCount = 0.0f;
        }
        else
        {
            SystemDaemon.stageNumber = 5; //�X�e�[�W�̔ԍ���5�ɐݒ�

            SystemDaemon.LoadScene("GameScene");
        }
    }

    public void On1_6Button() //1-6�{�^�����������Ƃ�
    {
        if (!SystemDaemon.gameData.clear1_5)
        {
            isAttentioned = true;

            attentionCount = 0.0f;
        }
        else
        {
            SystemDaemon.stageNumber = 6; //�X�e�[�W�̔ԍ���6�ɐݒ�

            SystemDaemon.LoadScene("GameScene");
        }
    }

    public void OnGoRuleSceneButton() //���[���V�[���ɍs���{�^��
    {
        SystemDaemon.LoadScene("RuleScene");
    }

    public void OnGoStorySceneButton() //�X�g�[���[�V�[���ɍs���{�^��
    {
        SystemDaemon.isFromStoryButton = true;

        LoadingController.Scene = "StoryScene";
        SystemDaemon.LoadScene("Loading");
    }

    public void OnBackButton() //�߂�{�^��
    {
        //SE��炷
        audioSourse.PlayOneShot(audioClips[1]);

        //�X�e�[�W�̃{�^���̔�\��
        button1_1.SetActive(false);
        button1_2.SetActive(false);
        button1_3.SetActive(false);
        button1_4.SetActive(false);
        button1_5.SetActive(false);
        button1_6.SetActive(false);

        backButton.SetActive(false); //BackButton�̔�\��
        titlePanel.GetComponent<Image>().sprite = titleSprites[0]; //Titlet�̊G�ɐ؂�ւ���

        //�{�^���̕\��
        goSelectionButton.SetActive(true);
        goRuleSceneButton.SetActive(true);
        goSaveAndLoadSceneButton.SetActive(true);

        if (SystemDaemon.gameData.isStoried) //1�x���X�g�[���[���Đ�����Ă��Ȃ�������
        {
            goStorySceneButton.SetActive(true); //�X�g�[���[�V�[���ɍs���{�^����\������
        }
    }

    public void OnSaveAndLoadButton() //�Z�[�u&���[�h�V�[���ɍs���{�^��
    {
        SystemDaemon.LoadScene("SaveAndLoadScene");
    }
}
