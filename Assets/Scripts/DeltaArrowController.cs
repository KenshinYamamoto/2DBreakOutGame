using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaArrowController : MonoBehaviour
{
    private int counter; //�t���[�����J�E���g����
    private int a = 1; //���̐�

    void Update()
    {
        counter += 1; //�t���[�����J�E���g
        transform.Translate(a * Vector3.up * Time.deltaTime * 20.0f); //a�����Ȃ��ɁAa�����Ȃ牺�ɓ�����

        if (counter % 100 == 0) //100�t���[����1��
        {
            a *= -1; //���ƕ������ւ���
        }
    }
}
