using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABasicMove : MonoBehaviour {

    public float velX;
    public float velZ;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void FixedUpdate () {

     

        transform.Translate(new Vector3(velX,0, velZ) * Time.deltaTime);
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        transform.Rotate(Vector3.up, 180);
        /*velX = -velX;
        velZ = -velZ;*/
    }
}
