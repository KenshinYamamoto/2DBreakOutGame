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
            case 1: //Square
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefabを出力する
                    brickQuantity = 15; //Squareは15個
                }
                break;

            case 2: //Box
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefabを出力する
                    brickQuantity = 21; //Boxは21個
                }
                break;

            case 3: //Note
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefabを出力する
                    brickQuantity = 22; //Noteは22個
                }
                break;

            case 4: //Triangle
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefabを出力する
                    brickQuantity = 25; //Triangleは25個
                }
                break;

            case 5: //Star
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefabを出力する
                    brickQuantity = 26; //Starは26個
                }break;

            case 6: //Heart
                {
                    GameObject go = Instantiate(prefabs[SystemDaemon.stageNumber - 1]); //Prefabを出力する
                    brickQuantity = 71; //Heartは71個
                }
                break;
            default:break;
        }
    }

    void Update()
    {
        
    }
}
