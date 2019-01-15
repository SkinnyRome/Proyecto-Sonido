using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMove : MonoBehaviour
{

    public float _distance;
    private float _origin;
    private int moveDirection;
    public float moveVel;
    private bool active;


    // Start is called before the first frame update
    void Start()
    {
        _origin = gameObject.transform.position.x;
        moveDirection = 1;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.x <= _origin)
        {
            moveDirection = 1;
        }
        else if(gameObject.transform.position.x >= _origin + _distance)
        {
            moveDirection = -1;
        }

        if(active)
            gameObject.transform.Translate ( moveDirection * moveVel, 0.0f, 0.0f, Space.World);


    }

    public void ToogleMovement()
    {
        active = !active;
    }
}
