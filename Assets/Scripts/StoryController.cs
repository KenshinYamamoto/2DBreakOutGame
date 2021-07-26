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
    public GameObject misakiImage; //�����݂�����Image
    public GameObject yukoImage; //�_�т䂤����Image
    public GameObject skipButton; //�X�L�b�v�{�^��
    public Sprite[] kohakuImages; //�咹���͂��̊G������z��@�@�@//0:�m�[�}�� 1:�ڂ���Ă� 2:�{���Ă� 3:�����Ă� 4:�΂��Ă� 5:��΂�
    public Sprite[] misakiImages; //�����݂����̊G������z��      //0:�m�[�}�� 1:�ڂ���Ă� 2:�{���Ă� 3:�����Ă� 4:�����Ă� 5:�ɂ����Ă�
    public Sprite[] yukoImages; //�_�т䂤���̊G������z��        //0:�m�[�}�� 1:�ڂ���Ă� 2:�����Ă� 3:�{���Ă� 4:�����Ă� 5:�p�����������Ă�H

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
        "����ɂ���!", //���͂�
        "���̖��O�͑咹���͂�!\n��낵���ˁ`", //���͂�
        "����ɂ���!", //�݂���
        "���͓����݂������Ă����܁`��", //�݂���
        "����ɂ���", //�䂤��
        "�_�т䂤���ł�", //�䂤��
        "���̃Q�[�����v���C���Ă���Ă��肪�Ƃˁ`\n�P���ȃQ�[�������ǁA�y����ł��ꂽ��������ȁB", //���͂�
        "���A���͂������̃Q�[���������?", //�݂���
        "������B���̂̓Q�[�������̌��t����B", //���͂�
        "�����A�Ȃ�ق�...", //�䂤��
        "�Q�[�������H���A�P���ȃQ�[���Ȃ񂾂��ǂ�����������炵����B", //���͂�
        "�ǂ�ȃQ�[���Ȃ�?", //�݂���
        "�悭����u���b�N�����Q�[�����ˁB", //���͂�
        "�^�C�g���ɂ����������Ă���������...", //�䂤��
        "�b�͂��Ă����A�����v���C���Ă�������!", //���͂�
        "��������", //�݂������䂤��
        "���������B���̉�b��������x������������^�C�g���Łu����1�x�X�g�[���[������v�������ƌ������B", //���͂�
        "����1�x�������l�Ȃ�Ă��Ȃ��ł���..." //�䂤��
    };


    void Start()
    {
        attentionImage.SetActive(false); //AttentionImage���\���ɂ���(�f�t�H���g)
        deltaArrow.SetActive(false); //DeltaArrowText���\���ɂ���(�f�t�H���g)
        kohakuImage.SetActive(false); //KohakuImage���\���ɂ���(�f�t�H���g)
        yukoImage.SetActive(false); //YukoImage���\���ɂ���(�f�t�H���g)
        misakiImage.SetActive(false); //MisakiImage���\���ɂ���(�f�t�H���g)

        kohakuImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(350f, 0f); //�ʒu���X�V(�f�t�H���g)
        misakiImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(350f, 0f); //�ʒu���X�V(�f�t�H���g)
        yukoImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(350f, 0f); //�ʒu���X�V(�f�t�H���g)

        if (!SystemDaemon.isFromStoryButton) //�����A�u�X�g�[��������v�{�^�����痈�Ă��Ȃ�������
        {
            skipButton.SetActive(false); //�X�L�b�v�{�^����\�����Ȃ�
        }
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

                            if (SystemDaemon.isFromStoryButton) //�u�X�g�[���[������v�{�^�����痈�Ă�����
                            {
                                SystemDaemon.LoadScene("Title"); //�^�C�g���ɖ߂�
                                SystemDaemon.isFromStoryButton = false; //isFromStoryButton�����Z�b�g
                            }
                            else
                            {
                                SystemDaemon.LoadScene("GameScene"); //�Q�[���V�[���ɍs��
                            }
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
        switch (serifNumber)
        {
            case 0: //�u����ɂ���!�v(���͂�)
                {
                    KohakuImageChange(4); //���͂����΂��Ă���G�ɂ���
                    NameChange("???"); //���O���X�V
                    kohakuImage.SetActive(true); //���͂��̊G��\������
                }break;

            case 1: //�u���̖��O�͑咹���͂�!\n��낵���ˁ`�v(���͂�)
                {
                    KohakuImageChange(0); //���͂��̃m�[�}���G�ɂ���
                    NameChange("���͂�"); //���O���X�V
                }break;

            case 2://�u����ɂ���!�v(�݂���)
                {
                    kohakuImage.SetActive(false); //���͂��̊G���\���ɂ���
                    MisakiImageChange(0); //�݂����̃m�[�}���̊G�ɂ���
                    NameChange("???"); //���O���X�V
                    misakiImage.SetActive(true); //�݂����̊G��\������
                }break;

            case 3: //�u���͓����݂������Ă����܁`���v(�݂���)
                {
                    NameChange("�݂���"); //���O���X�V
                }break;

            case 4: //�u����ɂ��́v(�䂤��)
                {
                    misakiImage.SetActive(false); //�݂����̊G���\���ɂ���
                    YukoImageChange(0); //�䂤���������Ă�G�ɂ���
                    NameChange("???"); //���O���X�V
                    yukoImage.SetActive(true); //�䂤���̊G��\������
                }
                break;

            case 5: //�u�_�т䂤���ł��v(�䂤��)
                {
                    NameChange("�䂤��"); //���O���X�V
                }break;

            case 6: //�u���̃Q�[�����v���C���Ă���Ă��肪�Ƃˁ`\n�P���ȃQ�[�������ǁA�y����ł��ꂽ��������ȁB�v(���͂�)
                {
                    yukoImage.SetActive(false); //�䂤���̊G���\���ɂ���
                    KohakuImageChange(4); //���͂����΂��Ă���G�ɂ���
                    NameChange("���͂�"); //���O���X�V
                    kohakuImage.SetActive(true); //���͂��̊G��\������
                }
                break;

            case 7:�@//�u���A���͂������̃Q�[���������?�v(�݂���)
                {
                    MisakiImageChange(3); //�݂����������Ă���G�ɂ���
                    KohakuImageChange(0); //���͂��̃m�[�}���̊G�ɂ���
                    misakiImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-350f, 0f); //�ʒu���X�V
                    misakiImage.GetComponent<RectTransform>().rotation = new Quaternion(0f, 180f, 0f, 1f); //�������X�V
                    NameChange("�݂���"); //���O���X�V
                    misakiImage.SetActive(true); //�݂����̊G��\������
                }break;

            case 8: //�u������B���̂̓Q�[�������̌��t����B�v(���͂�)
                {
                    MisakiImageChange(0); //�݂������m�[�}���̊G�ɂ���
                    KohakuImageChange(1); //���͂����ڂ���Ă�G�ɂ���
                    NameChange("���͂�");
                }break;

            case 9: //�u�����A�Ȃ�ق�...�v(�䂤��)
                {
                    misakiImage.SetActive(false); //�݂����̊G���\���ɂ���
                    KohakuImageChange(0); //���͂��̃m�[�}���̊G�ɂ���
                    yukoImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-350f, 0f); //�ʒu���X�V
                    yukoImage.GetComponent<RectTransform>().rotation = new Quaternion(0f, 180f, 0f, 1f); //�������X�V
                    NameChange("�䂤��"); //���O���X�V
                    yukoImage.SetActive(true); //�䂤���̊G��\������
                }break;

            case 10: //�u�Q�[�������H���A�P���ȃQ�[���Ȃ񂾂��ǂ�����������炵����B�v(���͂�)
                {
                    MisakiImageChange(5); //���͂��̋�΂��̊G�ɂ���
                    NameChange("���͂�"); //���O���X�V
                }break;

            case 11: //�u�ǂ�ȃQ�[���Ȃ�?�v(�݂���)
                {
                    yukoImage.SetActive(false); //�䂤���̊G���\���ɂ���
                    KohakuImageChange(0); //���͂��̃m�[�}���̊G�ɂ���
                    MisakiImageChange(4); //�݂����������Ă���G�ɂ���
                    NameChange("�݂���"); //���O���X�V
                    misakiImage.SetActive(true); //�݂����̊G��\������
                }break;

            case 12: //�u�悭����u���b�N�����Q�[�����ˁB�v(���͂�)
                {
                    MisakiImageChange(0); //�݂����̃m�[�}���̊G�ɂ���
                    NameChange("���͂�"); //���O���X�V
                }break;

            case 13: //�u�^�C�g���ɂ����������Ă���������...�v(�䂤��)
                {
                    misakiImage.SetActive(false); //�݂����̊G���\���ɂ���
                    NameChange("�䂤��"); //���O���X�V
                    yukoImage.SetActive(true); //�䂤���̊G��\������
                }break;

            case 14: //�u�b�͂��Ă����A�����v���C���Ă�������!�v(���͂�)
                {
                    KohakuImageChange(4); //���͂����΂��Ă���G�ɂ���
                    NameChange("���͂�"); //���O���X�V
                }
                break;

            case 15: //�u�������ˁv(�݂���&�䂤��)
                {
                    MisakiImageChange(1); //�݂������ڂ���Ă�G�ɂ���
                    YukoImageChange(1); //�䂤�����ڂ���Ă�G�ɂ���
                    KohakuImageChange(0); //���͂��̃m�[�}���̊G�ɂ���
                    misakiImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-462f, 0f); //�ʒu���X�V
                    yukoImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-286f, 0f); //�ʒu���X�V
                    NameChange("�݂���&�䂤��"); //���O���X�V
                    misakiImage.SetActive(true); //�݂����̊G��\������
                }
                break;

            case 16: //�u���������B���̉�b��������x������������^�C�g���Łu����1�x�X�g�[���[������v�������ƌ������B�v(���͂�)
                {
                    misakiImage.SetActive(false); //�݂����̊G���\���ɂ���
                    yukoImage.SetActive(false); //�䂤���̊G���\���ɂ���
                    KohakuImageChange(4); //���͂����΂��Ă���G�ɂ���
                    NameChange("���͂�"); //���O���X�V
                    kohakuImage.SetActive(true); //���͂��̊G��\������
                }
                break;

            case 17: //�u����1�x�������l�Ȃ�Ă��Ȃ��ł���...�v(�䂤��)
                {
                    KohakuImageChange(0); //���͂��̃m�[�}���̊G�ɂ���
                    YukoImageChange(4); //�䂤���������Ă���G�ɂ���
                    yukoImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-350f, 0f); //�ʒu���X�V
                    NameChange("�䂤��"); //���O���X�V
                    yukoImage.SetActive(true); //�䂤���̊G��\������
                }
                break;

            default: break;
        }
    }

    void KohakuImageChange(int imageNumber) //���͂��̊G��ύX����֐�
    {
        kohakuImage.GetComponent<Image>().sprite = kohakuImages[imageNumber]; //���͂��̊G���X�V
    }
    void MisakiImageChange(int imageNumber) //�݂����̊G��ύX����֐�
    {
        misakiImage.GetComponent<Image>().sprite = misakiImages[imageNumber]; //�݂����̊G���X�V
    }
    void YukoImageChange(int imageNumber) //�䂤���̊G��ύX����֐�
    {
        yukoImage.GetComponent<Image>().sprite = yukoImages[imageNumber]; //�䂤���̊G���X�V
    }
    void NameChange(string name) //���O��ύX����֐�
    {
        nameText.GetComponent<Text>().text = name; //���O���X�V
    }

    public void OnSkipButton() //�X�L�b�v�{�^���������ꂽ��
    {
        isStopped = true; //�~�߂�

        attentionImage.SetActive(true); //attentionImage��\������
    }

    public void OnYesButton() //�u�͂��v�{�^���������ꂽ��
    {
        SystemDaemon.LoadScene("Title"); //�^�C�g���ɖ߂�
    }

    public void OnNoButton() //�u�������v�{�^���������ꂽ��
    {
        isStopped = false; //������

        attentionImage.SetActive(false); //attentionImage���\���ɂ���
    }
}
