using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Inspectorに表示させる
public class Boundary
{
    public float xMin, xMax; //ゲームの端を決める(X軸)
}

public class PaddleController : MonoBehaviour
{
    public Boundary boundary; //上で作ったクラスに参照する
    public float paddleSpeed; //Paddleを動かす速度

    private Rigidbody2D rb2D; //RigidBody2Dを入れる変数

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); //RigidBody2Dを入れる
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); //AorDキー、もしくは左Arrowor右Arrowが押されたかどうか
        Vector2 movement = new Vector2(moveHorizontal, 0f); //動かす方向を決める
        rb2D.velocity = movement * paddleSpeed; //動かす

        rb2D.position = new Vector2(Mathf.Clamp(rb2D.position.x, boundary.xMin, boundary.xMax), -4f); //PaddleがxMin以下、xMax以上にならないようにする
    }
}
