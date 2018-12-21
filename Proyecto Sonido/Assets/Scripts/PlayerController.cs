using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{


    public int playerId = -1;

    const float slowFactor = 2.0f;

    private float jumpSpeed = 8;
    private Rigidbody rigidbody;
    private bool canJump;

    // Use this for initialization
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {


        //Rotate
        gameObject.transform.Rotate(gameObject.transform.up * (Input.GetAxis("Mouse X")));

        //Move
        gameObject.transform.Translate(((Input.GetAxis("Horizontal") / slowFactor)), 0, ((Input.GetAxis("Vertical") / slowFactor)));
        float f = (((Input.GetAxis("Vertical") + 1) / 2) * 255);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump) {
                canJump = false;
                rigidbody.velocity += jumpSpeed * Vector3.up;
            }
        }




    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Suelo")
        {
            canJump = true;
        }
    }
}
