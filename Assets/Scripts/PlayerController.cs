using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rb;
    public float JumpForce = 15;
    public float gravityModifilter = 2.5f;
    public bool isOnGround = true;

    public static bool gameOver;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifilter;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    /*
        collision : 衝突したオブジェクトの情報を扱う
    */

    // collider/rigidbody は他の collider/rigidbody に触れたときに OnCollisionEnter は呼び出される
    // In contrast to OnTriggerEnter, OnCollisionEnter is passed the Collision class and not a Collider.
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround(true);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("GameOver");
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround(false);
        }
    }

    void IsOnGround(bool isOnGround)
    {
        this.isOnGround = isOnGround;
    }
}
