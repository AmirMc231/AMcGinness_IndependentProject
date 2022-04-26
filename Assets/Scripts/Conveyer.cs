using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour
{
    public float speed = 3;
    public string boxHazard;

    public AudioClip breakSound;
    public ParticleSystem vaporParticles;

    private AudioSource asBox;
    private PlayerControllerScript playerCtrl;

    void Start()
    {
        asBox = GetComponent<AudioSource>();
        playerCtrl = GameObject.Find("Banana Man").GetComponent<PlayerControllerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Conveyer") && playerCtrl.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(boxHazard))
        {
            vaporParticles.Play();
            Destroy(gameObject, 1.0f);
            asBox.PlayOneShot(breakSound, 1.0f);
            
        }
    }
}
