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
            case 1:
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefab���o�͂���
                    brickQuantity = 71; //�n�[�g��71��
                }break;
        }
    }

    void Update()
    {
        
    }
}
