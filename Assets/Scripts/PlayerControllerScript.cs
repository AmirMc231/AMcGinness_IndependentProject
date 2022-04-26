using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private Rigidbody rbPlayer;
    private float speed = 5.0f;
    public float gravityModifier;
    private Animator animPlayer;
    private AudioSource audioPlayer;

    public AudioClip jumpSound;
    public AudioClip deathSound;

    public float turnSpeed = 150.0f;
    public float jumpForce = 60;
    private float horizontalInput;
    private float forwardInput;
    public bool gameOver = false;
    private bool onGround = true;
    


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        animPlayer = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");
            //Move the Player Forward
            //Debug.Log(forwardInput);
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        }
        
        if(forwardInput != 0)
        {
            animPlayer.SetBool("Run_a", true);
        }else
        {
            animPlayer.SetBool("Run_a", false);
        }

        bool spaceDown = Input.GetKeyDown(KeyCode.Space);
        if (spaceDown && !gameOver && onGround)
        {
            rbPlayer.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            animPlayer.SetTrigger("Jump_trig");
            audioPlayer.PlayOneShot(jumpSound, 1.0f);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            animPlayer.SetBool("Death_a", true);
            audioPlayer.PlayOneShot(deathSound, 1.0f);
        }
        else if (collision.gameObject.CompareTag("Poison"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            animPlayer.SetBool("Death_a", true);
            audioPlayer.PlayOneShot(deathSound, 1.0f);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            animPlayer.SetBool("Jump_a", false);
        }
    }
}
