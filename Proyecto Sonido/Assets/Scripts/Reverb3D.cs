using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverb3D : MonoBehaviour
{


    public float _minDist;
    public float _maxDist;
    FMOD.Reverb3D _reverb;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.checkError( SoundManager.instance.GetSystem().createReverb3D(out _reverb));

        FMOD.REVERB_PROPERTIES prop = FMOD.PRESET.CONCERTHALL();
        _reverb.setProperties(ref prop);
        

        FMOD.VECTOR pos;
        pos.x = gameObject.transform.position.x;
        pos.y = gameObject.transform.position.y;
        pos.z = gameObject.transform.position.z;

        _reverb.set3DAttributes(ref pos, _minDist, _maxDist);
        _reverb.setActive(true);
    }

    private void Update()
    {
    }

    public void OnEnable()
    {
        _reverb.setActive(true);
    }

    public void OnDisable()
    {
        _reverb.setActive(false);
    }

    public void ToogleActive()
    {
        bool result;
        _reverb.getActive(out result);
        _reverb.setActive(!result);
        Debug.Log("Estado del reverb: " + !result);

    }
}
