using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PLAYER_STATE { STOPPED, WALKING, RUNNING, JUMPING};

public class PlayerController : MonoBehaviour
{

    private PlayerSounds _playerSounds;
    public int playerId = -1;
    private PLAYER_STATE _state;
    const float WALKING_VEL = 5.0f;
    const float RUNNING_VEL = 3.0f;
    float _velocity;

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

        _playerSounds = gameObject.GetComponent<PlayerSounds>();
        SoundManager.instance.GetSystem().set3DListenerAttributes(0,ref pos,ref vel, ref up, ref at);
        _state = PLAYER_STATE.STOPPED;
    }


    // Update is called once per frame
    void FixedUpdate()
    {


        //Rotate
        gameObject.transform.Rotate(gameObject.transform.up * (Input.GetAxis("Mouse X")));
        gameObject.transform.Rotate(new Vector3(0.0f, Input.GetAxis("RotateArrowsHorizontal"), 0.0f) * rotateSpeed * Time.deltaTime);

        

        if(Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") > 0)
        {
            if (Input.GetKey(KeyCode.C))
            {
                if (canJump)
                {
                    _velocity = RUNNING_VEL;
                    _playerSounds.SetPlayerState(PLAYER_STATE.RUNNING);
                }
            }
            else
            {
                if (canJump)
                {
                    _velocity = WALKING_VEL;
                    _playerSounds.SetPlayerState(PLAYER_STATE.WALKING);
                }
            }
            //Move
            gameObject.transform.Translate(((Input.GetAxis("Horizontal") / _velocity)), 0, ((Input.GetAxis("Vertical") / _velocity)));
            float f = (((Input.GetAxis("Vertical") + 1) / 2) * 255);

        }
        else
        {
            if(canJump)
                _playerSounds.SetPlayerState(PLAYER_STATE.STOPPED);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump) {
                canJump = false;
                rigidbody.velocity += jumpSpeed * Vector3.up;
                _playerSounds.SetPlayerState(PLAYER_STATE.JUMPING);
            }
        }

        pos.x = gameObject.transform.position.x;
        pos.y = gameObject.transform.position.y;
        pos.z = gameObject.transform.position.z;

        SoundManager.instance.GetSystem().set3DListenerAttributes(0, ref pos, ref vel, ref up, ref at);



    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Suelo")
        {
            canJump = true;
            rigidbody.velocity = Vector3.zero;
            _playerSounds.SetPlayerState(PLAYER_STATE.STOPPED);
        }
    }
}
