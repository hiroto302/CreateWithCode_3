using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_lab3 : MonoBehaviour
{
    private Rigidbody _rb;
    float _speed = 5.0f;
    float _horizontalInput;
    float _verticalInput;

    float _zBound = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ConstrainPlayerPosition();
        MovePlayer();
    }

    // Moves the Player based on arrow key input
    void MovePlayer()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        _rb.AddForce(Vector3.right * _speed * _horizontalInput);
        _rb.AddForce(Vector3.forward * _speed * _verticalInput);
    }

    // Prevent the player from leaving the top or bottom of the screen
    void ConstrainPlayerPosition()
    {
        if(transform.position.z > _zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zBound);
        }
        if(transform.position.z < -_zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -_zBound);
        }
    }
}
