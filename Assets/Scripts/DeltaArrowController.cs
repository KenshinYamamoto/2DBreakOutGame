using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaArrowController : MonoBehaviour
{
    private int counter; //フレームをカウントする
    private int a = 1; //正の数

    void Update()
    {
        counter += 1; //フレームをカウント
        transform.Translate(a * Vector3.up * Time.deltaTime * 20.0f); //aが正なら上に、aが負なら下に動かす

        if (counter % 100 == 0) //100フレームに1回
        {
            a *= -1; //正と負を入れ替える
        }
    }
}
