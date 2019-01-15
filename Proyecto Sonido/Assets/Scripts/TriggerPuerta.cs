using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPuerta : MonoBehaviour
{

    public HorizontalMove puerta;

    private bool active;

    private void Start()
    {
        active = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        active = true;
    }

    public void OnTriggerExit(Collider other)
    {
        active = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (active)
                puerta.ToogleMovement();
        }
    }
}
