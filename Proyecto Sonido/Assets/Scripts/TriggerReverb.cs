using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReverb : MonoBehaviour
{
    private bool active;
    public Reverb3D _reverb;

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
                _reverb.ToogleActive();
        }
    }
}
