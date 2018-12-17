using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var lowlevelSystem = FMODUnity.RuntimeManager.LowlevelSystem;
        uint version;
        lowlevelSystem.getVersion(out version);


       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
