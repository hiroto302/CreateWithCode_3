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

    private Animator animator;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    // ダブルジャンプの最中であるかないか
    bool hasDoubleJumped = false;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifilter;
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DoubleJump();
        Jump();
        Dash();
    }

    /*
        collision : 衝突したオブジェクトの情報を扱う
    */

    // collider/rigidbody は他の collider/rigidbody に触れたときに OnCollisionEnter は呼び出される
    // In contrast to OnTriggerEnter, OnCollisionEnter is passed the Collision class and not a Collider.
    void OnCollisionEnter(Collision collision)
    {
        // 地面に接地した時の処理
        if(collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround(true);
            dirtParticle.Play();
            hasDoubleJumped = false;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(crashSound, 1.0f);
            explosionParticle.Play();
            dirtParticle.Stop();
            gameOver = true;
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
            Debug.Log("GameOver");
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            animator.SetTrigger("Jump_trig");
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround(false);
            dirtParticle.Stop();
        }
    }

    // 空中でもう一度ジャンプする機能
    void DoubleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isOnGround && !hasDoubleJumped && !gameOver)
        {
            hasDoubleJumped = true;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            animator.SetTrigger("Jump_trig");
            _rb.AddForce(Vector3.up * JumpForce / 1.3f, ForceMode.Impulse);
        }
    }

    void IsOnGround(bool isOnGround)
    {
        this.isOnGround = isOnGround;
    }

    // 走る機能をシミュレーションする
    void Dash()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            Time.timeScale = 1.5f;
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            Time.timeScale = 1.0f;
        }
    }
}
