using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Inspector�ɕ\��������
public class Boundary
{
    public float xMin, xMax; //�Q�[���̒[�����߂�(X��)
}

public class PaddleController : MonoBehaviour
{
    public Boundary boundary; //��ō�����N���X�ɎQ�Ƃ���
    public float paddleSpeed; //Paddle�𓮂������x

    private Rigidbody2D rb2D; //RigidBody2D������ϐ�

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); //RigidBody2D������
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); //AorD�L�[�A�������͍�Arrowor�EArrow�������ꂽ���ǂ���
        Vector2 movement = new Vector2(moveHorizontal, 0f); //���������������߂�
        rb2D.velocity = movement * paddleSpeed; //������

        rb2D.position = new Vector2(Mathf.Clamp(rb2D.position.x, boundary.xMin, boundary.xMax), -4f); //Paddle��xMin�ȉ��AxMax�ȏ�ɂȂ�Ȃ��悤�ɂ���
    }
}
