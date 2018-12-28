using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


    private FMOD.System _system;

    public static SoundManager instance = null;

    private FMOD.RESULT _result;


    public bool checkError(FMOD.RESULT result) {
        if (result != FMOD.RESULT.OK)
        {
            Debug.Log("FMOD error! " + result + FMOD.Error.String(result));
            return false;
        }
        return true;
    }


    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);



        var lowlevelSystem = FMODUnity.RuntimeManager.LowlevelSystem;
        uint version;
        lowlevelSystem.getVersion(out version);

        checkError(FMOD.Factory.System_Create(out _system));
        _system.setDSPBufferSize(1024, 10);

        checkError(_system.init(32, FMOD.INITFLAGS.NORMAL, (System.IntPtr)0));

        float doppler = 1.0f, rolloff = 1.0f;
        //_system.set3DSettings(doppler, 1.0f, rolloff);
        //FMOD TRABAJA EN SI, METROS SEGUNDOS...




    }

    public void OnApplicationQuit()
    {
        _system.release();
    }

    public FMOD.System GetSystem() {
        return _system;
    }


    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void LateUpdate () {
        _system.update();
	}
}
