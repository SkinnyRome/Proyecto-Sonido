using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum AMBIENCE_MUSIC {IDLE, SUSPENSE }



public class SoundManager : MonoBehaviour {


    private FMOD.System _system;
    private FMOD.Studio.System _studioSystem;

    public static SoundManager instance = null;

    private FMOD.RESULT _result;

    private AMBIENCE_MUSIC _ambienceMusic;
    private FMOD.Sound ambienceSound;
    private FMOD.Channel ambienceChannel;




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

        _studioSystem = FMODUnity.RuntimeManager.StudioSystem;
        FMOD.Studio.CPU_USAGE cpuUsage;
        _studioSystem.getCPUUsage(out cpuUsage);

        checkError(FMOD.Factory.System_Create(out _system));
        _system.setDSPBufferSize(1024, 10);

        checkError(_system.init(32, FMOD.INITFLAGS.NORMAL, (System.IntPtr)0));

        float doppler = 1.0f, rolloff = 1.0f;
        //_system.set3DSettings(doppler, 1.0f, rolloff);
        //FMOD TRABAJA EN SI, METROS SEGUNDOS...

        _ambienceMusic = AMBIENCE_MUSIC.SUSPENSE;
        PlayAmbienceMusic();

    

    }

    public void OnApplicationQuit()
    {
        _system.release();
    }

    public FMOD.System GetSystem() {
        return _system;
    }

   
    public FMOD.Studio.System GetStudioSystem()
    {
        return _studioSystem;
    }

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void LateUpdate () {
        _system.update();
	}


    public void ChangeAmbienceMusic()
    {
        ambienceSound.release();


        _ambienceMusic = (_ambienceMusic == AMBIENCE_MUSIC.IDLE) ? AMBIENCE_MUSIC.SUSPENSE : AMBIENCE_MUSIC.IDLE;

        PlayAmbienceMusic();
    }

    private void PlayAmbienceMusic()
    {
      
        FMOD.ChannelGroup c = new FMOD.ChannelGroup();

        string soundName = null;
        switch (_ambienceMusic)
        {
            case AMBIENCE_MUSIC.IDLE:
                soundName = "Idle.mp3";
                break;
            case AMBIENCE_MUSIC.SUSPENSE:
                soundName = "Suspense.mp3";
                break;
            default:
                break;
        }
        checkError(_system.createStream("Assets/Sounds/" + soundName, FMOD.MODE.LOOP_NORMAL |  FMOD.MODE._2D, out ambienceSound));
        //checkError(_system.playSound(ambienceSound, c, false, out ambienceChannel));
        ambienceChannel.setVolume(0.05f);
        
    }

    
}
