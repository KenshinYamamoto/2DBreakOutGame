using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirectorController : MonoBehaviour
{
    static public int brickQuantity = 0; //�u���b�N�̐�

    public GameObject[] prefabs; //�u���b�N�̌`������
    void Start()
    {
        switch (SystemDaemon.stageNumber) //1�`6�̐���
        {
            case 1: //Square
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefab���o�͂���
                    brickQuantity = 15; //Square��15��
                }
                break;

            case 2: //Box
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefab���o�͂���
                    brickQuantity = 21; //Box��21��
                }
                break;

            case 3: //Note
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefab���o�͂���
                    brickQuantity = 22; //Note��22��
                }
                break;

            case 4: //Triangle
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefab���o�͂���
                    brickQuantity = 25; //Triangle��25��
                }
                break;

            case 5: //Star
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefab���o�͂���
                    brickQuantity = 26; //Star��26��
                }break;

            case 6: //Heart
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefab���o�͂���
                    brickQuantity = 71; //Heart��71��
                }
                break;
            default:break;
        }
    }

    void Update()
    {
        
    }
}
