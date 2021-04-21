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
            dirtParticle.Play();
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

    void IsOnGround(bool isOnGround)
    {
        this.isOnGround = isOnGround;
    }
}
