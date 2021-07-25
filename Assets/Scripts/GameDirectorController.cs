using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirectorController : MonoBehaviour
{
    static public int brickQuantity = 0; //ブロックの数

    public GameObject[] prefabs; //ブロックの形を入れる
    void Start()
    {
        switch (SystemDaemon.stageNumber) //1～6の数字
        {
            case 1:
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefabを出力する
                    brickQuantity = 71; //ハートは71個
                }break;
        }
    }

    void Update()
    {
        
    }
}
