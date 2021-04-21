using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 10.0f;
    public float hidePosX = -5f;


    void Update()
    {
        if(!PlayerController.gameOver)
        {
            MoveLeft();
        }
        Hide();
    }

    void MoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    void Hide()
    {
        if(hidePosX > transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }
}
