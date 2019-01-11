using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{


    public int playerId = -1;

    const float slowFactor = 5.0f;

    private float jumpSpeed = 8;
    private float rotateSpeed = 100.0f;
    private Rigidbody rigidbody;
    private bool canJump;
    FMOD.VECTOR pos;
    FMOD.VECTOR vel;

    FMOD.VECTOR at;
    FMOD.VECTOR up;


    // Use this for initialization
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        canJump = true;

        pos = new FMOD.VECTOR();
        pos.x = gameObject.transform.position.x;
        pos.y = gameObject.transform.position.y;
        pos.z = gameObject.transform.position.z;

        FMOD.VECTOR vel = new FMOD.VECTOR();
        vel.x = vel.y = vel.z = 0;

        FMOD.VECTOR up = new FMOD.VECTOR();
        up.x = 0; up.y = 1; up.z = 0;

        FMOD.VECTOR at = new FMOD.VECTOR();
        at.x = 0; at.y = 0; at.z = 1;

        SoundManager.instance.GetSystem().set3DListenerAttributes(0,ref pos,ref vel, ref up, ref at);
    }


    // Update is called once per frame
    void FixedUpdate()
    {


        //Rotate
        gameObject.transform.Rotate(gameObject.transform.up * (Input.GetAxis("Mouse X")));
        gameObject.transform.Rotate(new Vector3(0.0f, Input.GetAxis("RotateArrowsHorizontal"), 0.0f) * rotateSpeed * Time.deltaTime);

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

        pos.x = gameObject.transform.position.x;
        pos.y = gameObject.transform.position.y;
        pos.z = gameObject.transform.position.z;

        SoundManager.instance.GetSystem().set3DListenerAttributes(0, ref pos, ref vel, ref up, ref at);


        //Debug.Log(pos.x);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Suelo")
        {
            canJump = true;
            rigidbody.velocity = Vector3.zero;
        }
    }
}
