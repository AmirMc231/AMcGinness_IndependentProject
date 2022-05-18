using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerGameVan : MonoBehaviour
{
    

    public float RotationSpeed = 1;
    float mX = 0;
    float mY = 0;
    //public Transform Target, Player;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        CamControl();

    }

    void CamControl()
    {
        //transform.position = player.transform.position + offset;
        mX += Input.GetAxis("Mouse X") * RotationSpeed * 2;

        mY -= Input.GetAxis("Mouse Y") * RotationSpeed * 2;
        //mY = Mathf.Clamp(mY, -35, 60);

        //transform.Rotate(-mY, mX, 0f);
        transform.rotation = Quaternion.Euler(mY, mX, 0);
    }

}
