using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private LayerMask PickUpMask;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform pickUpTarget;
    [Space]
    [SerializeField] private float pickUpRange;
    private Rigidbody currentObject;
    private PlayerControllerScript playerCtrl;

    private void Start()
    {
        playerCtrl = GameObject.Find("Banana Man").GetComponent<PlayerControllerScript>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !playerCtrl.gameOver)
        {
            if (currentObject)
            {
                currentObject.useGravity = true;
                currentObject = null;
                return;
            }
            
            Ray CameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if(Physics.Raycast(CameraRay, out RaycastHit HitInfo, pickUpRange, PickUpMask))
            {
                currentObject = HitInfo.rigidbody;
                currentObject.useGravity = false;

            }
        }
    }

    private void FixedUpdate()
    {
        if (currentObject)
        {
            Vector3 DirectionToPoint = pickUpTarget.position - currentObject.position;
            float DistanceToPoint = DirectionToPoint.magnitude;

            currentObject.velocity = DirectionToPoint * 12f * DistanceToPoint;
        }
    }
}
