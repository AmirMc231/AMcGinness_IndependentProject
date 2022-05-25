using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    //GameObject player;
    private Transform PlayerDir;
    private float distance;
    public float range;
    public Transform head;
    public Transform gun;
    public GameObject bullet;
    public float bulletSpeed = 500;
    public float fireRate = 10;
    //public float nextFire;
    private PlayerControllerScript playerCtrl;
    public int burstNum = 5;
    public float burstInterval = 1.5f;
    private int shotCount = 0;
    private int encounter = 0;
    private int soundInstance = 0;
    public AudioClip breakSound;
    public AudioClip gunSound;
    private AudioSource asTurret;
    public ParticleSystem vaporParticles;
    public ParticleSystem gunFlash;

    // Start is called before the first frame update
    void Start()
    {
        asTurret = GetComponent<AudioSource>();
        PlayerDir = GameObject.Find("TurretTarget").transform;
        //PlayerDir = GameObject.Find("Banana Man").transform;
        //PlayerDir = GameObject.FindGameObjectWithTag("Player").transform;
        playerCtrl = GameObject.Find("Banana Man").GetComponent<PlayerControllerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        //head.LookAt(PlayerDir);
        TurretPointer();
    }

    void TurretPointer()
    {
        //Vector3 turDirection = (PlayerDir.transform.position - transform.position).normalized;
        distance = Vector3.Distance(PlayerDir.position, transform.position);
        if(distance <= range && playerCtrl.gameOver == false)
        {
            head.LookAt(PlayerDir);
            if (encounter < 1)
            {
                InvokeRepeating("shootGun", 1, 1 / fireRate);
                encounter = encounter + 1;
            }
            //if (Time.time >= nextFire)
            //{
                //nextFire = Time.time + 1.0f / fireRate;
            //}
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (soundInstance < 1) 
            { 
            Destroy(gameObject, 0.5f);
            Instantiate(vaporParticles);
            asTurret.PlayOneShot(breakSound, 1.0f);
            soundInstance = soundInstance + 1;
            }
        }
    }

    void shootGun()
    {
        if(shotCount < burstNum)
        {
            GameObject clone = Instantiate(bullet, gun.position, transform.rotation);
            clone.GetComponent<Rigidbody>().AddForce(head.forward * bulletSpeed);
            Instantiate(gunFlash, gun.position, gun.transform.rotation);
            Destroy(clone, 10);
            shotCount = shotCount + 1;
            asTurret.PlayOneShot(gunSound, 1.0f);
        }
        else
        {
            CancelInvoke("shootGun");
            StartCoroutine(GunBurstShoot());
        }
        
    }

    IEnumerator GunBurstShoot()
    {
        yield return new WaitForSeconds(burstInterval);
        shotCount = 0;
        encounter = 0;
    }
}
