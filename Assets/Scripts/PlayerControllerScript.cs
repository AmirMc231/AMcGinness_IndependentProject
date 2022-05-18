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

    public GameObject playerBullet;
    public GameObject powerUpIndicator;
    public float bulletSpeed = 100000;
    public Transform head;
    public Transform gun;
    public float turnSpeed = 150.0f;
    public float jumpForce = 60;
    private float horizontalInput;
    private float forwardInput;
    public bool gameOver = false;
    private bool onGround = true;
    public int health = 5;
    private bool hasPowerUp = false;
    


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        animPlayer = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        powerUpIndicator.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            gameOver = true;
            animPlayer.SetBool("Death_a", true);
            
        }
        
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
        bool fireDown = Input.GetKeyDown(KeyCode.Mouse0);
        if (fireDown && !gameOver && hasPowerUp)
        {
            Debug.Log("MouseClick");
            GameObject clone = Instantiate(playerBullet, gun.position, transform.rotation);
            clone.GetComponent<Rigidbody>().AddForce(head.forward * bulletSpeed);
            Destroy(clone, 10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
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

        if (collision.gameObject.CompareTag("Bullet"))
        {
            health = health - 1;
            Debug.Log("Health: " + health);
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(collision.gameObject);
            StartCoroutine(PowerUpDur());
            powerUpIndicator.SetActive(true);
        }
    }

    IEnumerator PowerUpDur()
    {
        yield return new WaitForSeconds(10);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }
}
