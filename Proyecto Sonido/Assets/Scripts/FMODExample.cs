using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODExample : MonoBehaviour {


    private FMOD.System _system;

    private FMOD.RESULT _result;


    public bool checkError(FMOD.RESULT result) {
        if (result != FMOD.RESULT.OK)
        {
            Debug.Log("FMOD error! " + result + FMOD.Error.String(result));
            return false;
        }
        return true;
    }


    // Use this for initialization
    void Start () {
        var lowlevelSystem = FMODUnity.RuntimeManager.LowlevelSystem;
        uint version;
        lowlevelSystem.getVersion(out version);
        
        checkError(FMOD.Factory.System_Create(out _system));
        _system.setDSPBufferSize(1024, 10);

        checkError(_system.init(32, FMOD.INITFLAGS.NORMAL, (System.IntPtr)0));

        FMOD.Sound s;
        checkError(_system.createSound("Assets/Sounds/locutora.mp3", FMOD.MODE.DEFAULT, out s));
        FMOD.Channel channel;
        FMOD.ChannelGroup c = new FMOD.ChannelGroup();
        checkError(_system.playSound(s, c, false, out channel));
        channel.setVolume(1.0f);




    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
