using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    public GameObject serifText; //�Z���t�̃e�L�X�g
    public GameObject attentionImage; //�X�L�b�v���Ă悢���ǂ����̊m�F
    public GameObject deltaArrow; //���
    public GameObject nameText; //����ׂ��Ă���L�����̖��O������e�L�X�g
    public GameObject kohakuImage; //�咹���͂���Image
    public GameObject yukoImage; //�_�т䂤����Image
    public GameObject misakiImage; //�����݂�����Image
    public Sprite[] kohakuImages; //�咹���͂��̊G������z��@�@�@//0:�m�[�}�� 1:�ڂ���Ă� 2:�{���Ă� 3:�����Ă� 4:�΂��Ă� 5:��΂�
    public Sprite[] yukoImages; //�_�т䂤���̊G������z��        //0:�m�[�}�� 1:�ڂ���Ă� 2:�����Ă� 3:�{���Ă� 4:�����Ă� 5:�p�����������Ă�H
    public Sprite[] misakiImages; //�����݂����̊G������z��      //0:�m�[�}�� 1:�ڂ���Ă� 2:�{���Ă� 3:�����Ă� 4:�����Ă� 5:�ɂ����Ă�

    private int serifNumber; //�ǂ̃Z���t��\�����邩�Ƃ����ϐ�
    private string displaySerif; //�\�������镶��
    private int serifCharNumber; //�������ڂ�\�����邩�Ƃ����ϐ�
    private int speed; //�\�������鑬�x
    public int displayspeed;
    private bool isClicked = false; //�N���b�N�������ǂ����̔���
    private bool isStopped = false; //�~�߂邩�ǂ����̔���
    private bool isCharClicked = false; //�Z���t�̕������Ō�܂Ŕ�΂����ǂ����̔���

    private string[] serifs =
    {
        "����ɂ���!",
        "���̖��O�͑咹���͂�!\n��낵���ˁ`",
        "���͓����݂������Ă����܁`��",
        "���͐_�т䂤���ł�",
        "���̃Q�[�����v���C���Ă���Ă��肪�Ƃˁ`\n�P���ȃQ�[�������ǁA�y����ł��ꂽ��������ȁB",
        "���A���͂������̃Q�[���������?",
        "������B���̂̓Q�[�������̌��t����B",
        "�����A�Ȃ�ق�...",
        "�Q�[�������H���A�P���ȃQ�[���Ȃ񂾂��ǂ�����������炵����B",
        "�ǂ�ȃQ�[���Ȃ�?",
        "�悭����u���b�N�����Q�[�����ˁB",
        "�b�͂��Ă����A�����v���C���Ă�������!",
        "��������",
        "���������B���̉�b��������x��������������^�C�g���Łu����1�x�X�g�[���[������v�������ƌ������B",
        "����1�x�������l�Ȃ�Ă��Ȃ��ł���..."
    };


    void Start()
    {
        attentionImage.SetActive(false); //AttentionImage���\���ɂ���(�f�t�H���g)
        deltaArrow.SetActive(false); //DeltaArrowText���\���ɂ���(�f�t�H���g)
        kohakuImage.SetActive(false); //KohakuImage���\���ɂ���(�f�t�H���g)
        yukoImage.SetActive(false); //YukoImage���\���ɂ���(�f�t�H���g)
        misakiImage.SetActive(false); //MisakiImage���\���ɂ���(�f�t�H���g)
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped) //stop���������Ă��Ȃ��Ƃ�
        {
            speed++;
            if (speed % displayspeed == 0) //�\�����鑬�x�𗎂Ƃ�
            {
                if (serifCharNumber != serifs[serifNumber].Length) //serifs[serifNumber]�̍Ō�̕����łȂ����
                {
                    if (!isCharClicked) //�����̒ǉ���
                    {
                        displaySerif += serifs[serifNumber][serifCharNumber]; //1�������ǉ����Ă���

                        serifCharNumber++; //���̕����ɂ���
                    }
                    else //�����̒ǉ����ɍ��N���b�N���ꂽ��
                    {
                        displaySerif = serifs[serifNumber]; //������S�Ă�\������
                        serifCharNumber = serifs[serifNumber].Length; //�����̍Ō�ɂ���
                    }
                }
                else //serifs[serifNumber]���Ō�̕�����������
                {
                    deltaArrow.SetActive(true); //����\������

                    if (serifNumber != serifs.Length - 1) //serifs[]���Ō�̃Z���t�łȂ�������
                    {
                        if (isClicked) //�N���b�N���ꂽ���肪true��������
                        {
                            displaySerif = ""; //�\�����Ă��镶���������
                            serifCharNumber = 0; //�����̔ԍ����ŏ��ɂ���
                            serifNumber++; //���̃Z���t�ɂ���
                            deltaArrow.SetActive(false); //�����\���ɂ���
                        }
                    }
                    else //serifs[]���Ōゾ������
                    {
                        if (isClicked) //�N���b�N���ꂽ���肪true��������
                        {
                            displaySerif = ""; //�\�����Ă��镶���������
                            serifCharNumber = 0; //�����̔ԍ����ŏ��ɂ���
                            isStopped = true; //�~�߂�

                            SystemDaemon.LoadScene("otherScene");
                        }
                    }
                }
                serifText.GetComponent<Text>().text = displaySerif; //displaySerif��\��
                isClicked = false; //�N���b�N���ꂽ�����false�ɕύX
                isCharClicked = false; //�N���b�N���ꂽ�����false�ɕύX
            }

            if (Input.GetMouseButtonDown(0) || Input.anyKeyDown) //�N���b�N���ꂽ��
            {
                isClicked = true; //�N���b�N���ꂽ�����true�ɂ���
                isCharClicked = true; //�N���b�N���ꂽ�����true�ɂ���
            }
        }
    }
}
