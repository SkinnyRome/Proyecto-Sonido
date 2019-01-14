using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    PlayerController _playerController;
    private PLAYER_STATE _state;
    private FMOD.Studio.EventInstance _walkInstance;
    private FMOD.Studio.EventInstance _jumpInstance;
    private int _velocityParameterIndex;

    

    // Start is called before the first frame update
    void Start()
    {
        _playerController = gameObject.GetComponent<PlayerController>();

        FMOD.Studio.EventDescription walkDescription;
        SoundManager.instance.checkError(SoundManager.instance.GetStudioSystem().getEvent("event:/Steps", out walkDescription));
        FMOD.Studio.PARAMETER_DESCRIPTION velDescription;
        SoundManager.instance.checkError(walkDescription.createInstance(out _walkInstance));

        SoundManager.instance.checkError(walkDescription.getParameter("Velocity", out velDescription));
        _velocityParameterIndex = velDescription.index;

        FMOD.Studio.EventDescription jumpDescription;
        SoundManager.instance.checkError(SoundManager.instance.GetStudioSystem().getEvent("event:/Jump", out jumpDescription));
        SoundManager.instance.checkError(jumpDescription.createInstance(out _jumpInstance));

        _state = PLAYER_STATE.STOPPED;

      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerState(PLAYER_STATE s)
    {
        if(s != _state){
            _state = s;
            ChangeMovementSound();
        }

    }

    private void ChangeMovementSound()
    {
        switch (_state)
        {
            case PLAYER_STATE.STOPPED:
                _jumpInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                _walkInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                break;
            case PLAYER_STATE.WALKING:
                _walkInstance.start();
                _walkInstance.setParameterValueByIndex(_velocityParameterIndex, 0.5f);
                break;
            case PLAYER_STATE.RUNNING:
                _walkInstance.start();
                _walkInstance.setParameterValueByIndex(_velocityParameterIndex, 1.0f);
                break;
            case PLAYER_STATE.JUMPING:
                _walkInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                _jumpInstance.start();
                break;
            default:
                break;
        }
    }
}
