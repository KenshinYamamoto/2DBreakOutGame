using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    static public int stateNumber = 0; //�X�e�[�g�i���o�[

    public AudioClip brickHit; //�u���b�N�ɓ����������̉�
    public AudioClip wall_paddleHit; //�ǂ�Paddle�ɓ����������̉�
    public Text countDownText; //�J�E���g�_�E����\������e�L�X�g
    public GameObject resultText; //Clear��GameOver��������e�L�X�g

    private float initialVelocityY = 250f; //Y�����ɑł��o�����x
    private Rigidbody2D rb2D; //RigidBody2D������ϐ�
    private AudioSource audioSource; //AudioSource������ϐ�
    private int initialVelocityX; //X�����ɑł��o�����x
    private float countDown = 3.0f; //�J�E���g�_�E��(3�b�O)
    private float countUp = 0f; //�J�E���g�A�b�v
    private bool isCalled = false; //1�񂾂����s���邽�߂̔���

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); //RigidBody2D������
        audioSource = GetComponent<AudioSource>(); //AudioSource������
        countDownText.text = "3"; //3��\������
        resultText.GetComponent<Text>().text = ""; //resultText�����Z�b�g

        resultText.SetActive(false); //resultText���\���ɂ���(�f�t�H���g)
        
        initialVelocityX = Random.Range(-5, 6); //-5����5�͈̔͂Œ��I����
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime; //���Ԃ̍X�V

        switch (stateNumber) //�X�e�[�g�}�V��
        {
            case 0: //�ҋ@
                {
                    if (countDown < 0.0f)
                    {
                        countDownText.text = ""; //�J�E���g�_�E��������

                        stateNumber = 3; //case3�ɍs��
                    }
                    else if (countDown < 1.0f)
                        stateNumber = 1; //case1�ɍs��

                    else if (countDown < 2.0f)
                        stateNumber = 2; //case2�ɍs��
                }
                break;

            case 1:
                {
                    countDownText.text = "1"; //1��\������

                    stateNumber = 0; //case0�ɖ߂�
                }
                break;

            case 2:
                {
                    countDownText.text = "2"; //2��\������

                    stateNumber = 0; //case0�ɖ߂�
                }
                break;

            case 3: //�Q�[���������Ă���Ƃ�
                {
                    if (!SystemDaemon.isGameStarted)
                    {
                        transform.parent = null; //�e�q�֌W��؂�
                        rb2D.isKinematic = false; //iskinematic�̃`�F�b�N���O��

                        rb2D.AddForce(new Vector2(initialVelocityX, initialVelocityY)); //�{�[���ɗ͂������ē�����

                        SystemDaemon.isGameStarted = true; //�Q�[���𓮂���

                        countDown = 0f; //���Ԃ̃��Z�b�g
                    }

                    if(GameDirectorController.brickQuantity == 0) //�u���b�N���Ȃ��Ȃ�����
                    {
                        stateNumber = 5; //case5�ɐi��
                    }
                }
                break;

            case 4: //�Q�[���I�[�o�[
                {
                    if (!isCalled)
                    {
                        SystemDaemon.isGameStarted = false; //�Q�[�����~�߂�

                        resultText.GetComponent<Text>().color = new Color(0f, 0f, 1f, 1f);//���̐F��ɂ���
                        resultText.GetComponent<Text>().text = "GAME OVER"; //GameOver��\��
                        resultText.SetActive(true); //resultText��\������

                        isCalled = true; //���b�N��������
                    }

                    countUp += Time.deltaTime; //���Ԃ̍X�V

                    if (countUp >= 2f) //2�b��������
                    {
                        SystemDaemon.LoadScene("Title"); //�^�C�g���ɖ߂�

                        stateNumber = 0; //�X�e�[�g�i���o�[�̃��Z�b�g
                    }
                }break;

            case 5: //�N���A
                {
                    if (!isCalled)
                    {
                        SystemDaemon.isGameStarted = false; //�Q�[�����~�߂�

                        resultText.GetComponent<Text>().color = new Color(1f, 1f, 0f, 1f);//���̐F�����ɂ���
                        resultText.GetComponent<Text>().text = "CLEAR"; //Clear��\��
                        resultText.SetActive(true); //resultText��\������

                        switch (SystemDaemon.stageNumber) //�X�e�[�W�̔ԍ��ɂ�镪��
                        {
                            case 1: //�X�e�[�W1-1��������
                                {
                                    SystemDaemon.gameData.clear1_1 = true;
                                }break;
                            case 2: //�X�e�[�W1-2��������
                                {
                                    SystemDaemon.gameData.clear1_2 = true;
                                }
                                break;
                            case 3: //�X�e�[�W1-3��������
                                {
                                    SystemDaemon.gameData.clear1_3 = true;
                                }break;
                            case 4: //�X�e�[�W1-4��������
                                {
                                    SystemDaemon.gameData.clear1_4 = true;
                                }
                                break;
                            case 5: //�X�e�[�W1-5��������
                                {
                                    SystemDaemon.gameData.clear1_5 = true;
                                }
                                break;
                            case 6: //�X�e�[�W1-6��������
                                {
                                    SystemDaemon.gameData.clear1_6 = true;
                                }
                                break;
                            default:break;
                        }
                        isCalled = true; //���b�N��������
                    }

                    countUp += Time.deltaTime; //���Ԃ̍X�V

                    if (countUp >= 2f) //2�b��������
                    {
                        SystemDaemon.LoadScene("Title"); //�^�C�g���ɖ߂�
                        stateNumber = 0; //�X�e�[�g�i���o�[�̃��Z�b�g
                    }
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Brick")
        {
            audioSource.clip = brickHit; //brickHit�̉����Z�b�g����
            audioSource.Play(); //�����Đ�����
            Destroy(collision.gameObject); //�u���b�N����

            GameDirectorController.brickQuantity--; //�u���b�N���󂵂���A1���炷
        }
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Paddle")
        {
            audioSource.clip = wall_paddleHit; //Wall_PaddleHit�̉����Z�b�g����
            audioSource.Play(); //�����Đ�����
        }
    }
}
