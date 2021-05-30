using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private float _speed = 10.0f;
    private Vector3 _startPos;

    private float _repeatWidth;
    void Start()
    {
        _startPos = transform.position;
        _repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void Update()
    {
        if(GameManager.Instance.CurrentState == GameManager.GameState.RUNNING)
        {
            if(transform.position.x < _startPos.x - _repeatWidth)
            {
                ResetPostion();
            }
            if(!PlayerController.gameOver)
            {
                MoveLeft();
            }
        }
    }

    void MoveLeft()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    void ResetPostion()
    {
        transform.position = _startPos;
    }
}
