using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    private Vector3 rotationOffset;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        rotationOffset = transform.rotation.eulerAngles - player.transform.rotation.eulerAngles;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //transform.Rotate(rotationOffset * gameObject.transform.up);
        //transform.LookAt(player.transform);
        //transform.position = transform.localPosition;
        //transform.localEulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
        //transform.position = player.transform.position + offset;



    }
}
