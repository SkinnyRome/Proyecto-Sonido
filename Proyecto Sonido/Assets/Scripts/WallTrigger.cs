﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WallTrigger : MonoBehaviour
{
    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            soundManager.ChangeAmbienceMusic();
        }
    
    }
}