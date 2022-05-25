using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    
    private Rigidbody rbPlayer;
    private float speed = 5.0f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
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

    //ingame GUI
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;
    public Button quitButton;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask pickUpMask;
    Vector3 velocity;
    public float jumpHeight = 3f;

    private float horizontalInput;
    private float forwardInput;
    public bool gameOver = false;
    private bool onGround = true;
    private bool onPickUp = true;
    public int health = 5;
    private bool hasPowerUp = false;


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        //Physics.gravity *= gravityModifier;
        animPlayer = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        powerUpIndicator.SetActive(false);
        gameoverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !gameOver)
        {
            audioPlayer.PlayOneShot(deathSound, 1.0f);
            gameOver = true;
            animPlayer.SetBool("Death_a", true);
        }
        if (gameOver)
        {
            
            GameOver();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        moveAction();
        jumpAction();
        UpdateHealth();

        bool fireDown = Input.GetKeyDown(KeyCode.Mouse0);
        if (fireDown && !gameOver && hasPowerUp)
        {
            //Debug.Log("MouseClick");
            GameObject clone = Instantiate(playerBullet, gun.position, transform.rotation);
            clone.GetComponent<Rigidbody>().AddForce(head.forward * bulletSpeed);
            Destroy(clone, 10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health = health - 1;
            Debug.Log("Health: " + health);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            health = health + 3;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpDur());
            powerUpIndicator.SetActive(true);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Fire") && !gameOver)
        {
            health = 0;
            Debug.Log("Game Over");
            //gameOver = true;
            animPlayer.SetBool("Death_a", true);
            audioPlayer.PlayOneShot(deathSound, 1.0f);
        }
        else if (hit.gameObject.CompareTag("Poison") && !gameOver)
        {
            health = 0;
            Debug.Log("Game Over");
            //gameOver = true;
            animPlayer.SetBool("Death_a", true);
            audioPlayer.PlayOneShot(deathSound, 1.0f);
        }

        //if (hit.gameObject.CompareTag("Ground"))
        //{
        //    onGround = true;
        //    animPlayer.SetBool("Jump_a", false);
        //}
        
    }

    IEnumerator PowerUpDur()
    {
        yield return new WaitForSeconds(10);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }

    private void jumpAction()
    {
        if (forwardInput != 0 || horizontalInput != 0)
        {
            animPlayer.SetBool("Run_a", true);
        }
        else
        {
            animPlayer.SetBool("Run_a", false);
        }

        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        onPickUp = Physics.CheckSphere(groundCheck.position, groundDistance, pickUpMask);
        
        if (onGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        bool spaceDown = Input.GetKeyDown(KeyCode.Space);
        if (spaceDown && !gameOver && (onGround || onPickUp))
        {
            //rbPlayer.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityModifier);
            onGround = false;
            animPlayer.SetTrigger("Jump_trig");
            audioPlayer.PlayOneShot(jumpSound, 1.0f);
        }

        velocity.y += gravityModifier * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void moveAction()
    {
        if (!gameOver)
        {
            //Player movement code here

            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");
            //Move the Player Forward
            

            Vector3 direction = new Vector3(horizontalInput, 0f, forwardInput).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                //transform.Translate(moveDir.normalized * Time.deltaTime * speed);
                controller.Move(moveDir.normalized * speed * Time.deltaTime);

            }

        }
    }
    public void GameOver()
    {
        gameoverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
    

    public void UpdateHealth()
    {
        healthText.text = "Health: " + health;
    }

    public void GameRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGameButton()
    {
        Application.Quit(); //quit button function
    }
}
