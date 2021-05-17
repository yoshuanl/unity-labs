using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public ParticleSystem explosionPart;
    public ParticleSystem dirtPart;

    public AudioClip jumpSound;
    public AudioClip crashSound;


    private float jumpForce = 500.0f;
    private float gravityModifier = 3f;
    private bool isOnGround = true;
    public bool isGameOver = false;
    private int jumped = 0;
    public int jumpLimit = 2;
    public bool isDashing = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Physics.gravity *= gravityModifier;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            // jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isOnGround)
                {
                    isOnGround = false;
                    jumped++;

                    playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    playerAnim.SetTrigger("Jump_trig"); // play jump animation
                    playerAudio.PlayOneShot(jumpSound); // play jump audio once
                    dirtPart.Stop(); // stop dirt when jump
                }
                else if (jumped < jumpLimit)
                {
                    jumped++;

                    playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    playerAudio.PlayOneShot(jumpSound); // play jump audio once
                }
            }

            // dash
            else if (Input.GetKeyDown(KeyCode.D))
            {
                isDashing = !isDashing;

                if (isDashing)
                {
                    playerAnim.SetFloat("Speed_Multiplier", 1.5f);
                    playerAnim.SetFloat("Head_Vertical_f", -0.3f);
                }
                else
                {
                    playerAnim.SetFloat("Speed_Multiplier", 1.0f);
                    playerAnim.SetFloat("Head_Vertical_f", 0.0f);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtPart.Play(); // restart dirt when land
            jumped = 0;
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            playerAnim.SetBool("Death_b", true); // play fall down animation
            playerAnim.SetInteger("DeathType_int", 1);
            explosionPart.Play(); // play smoke animation
            playerAudio.PlayOneShot(crashSound); // play crash audio once
            dirtPart.Stop(); // stop dirt when dead
        }
    }
}
