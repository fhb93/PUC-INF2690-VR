using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus;
using Unity.XR.Oculus.Input;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;

    public float sensitivity;

    Vector3 offset;

   // public GearVRTrackedController controller;

    void FixedUpdate()
    {
#if UNITY_EDITOR
        //float rotateHorizontal = Input.GetAxis("Mouse X") * 20;
        //float rotateVertical = Input.GetAxis("Mouse Y") * 18;
        //transform.Rotate(transform.up * rotateHorizontal * sensitivity);
        //transform.Rotate(-transform.right * rotateVertical * sensitivity);
       
#else


#endif
        // transform.RotateAround(player.transform.position, -Vector3.up, rotateHorizontal * sensitivity); //use transform.Rotate(-transform.up * rotateHorizontal * sensitivity) instead if you dont want the camera to rotate around the player
        // transform.RotateAround(Vector3.zero, transform.right, rotateVertical * sensitivity); // again, use transform.Rotate(transform.right * rotateVertical * sensitivity) if you don't want the camera to rotate around the player

        //transform.position = player.transform.position + offset;

    }


    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("MainCamera");

        //offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
