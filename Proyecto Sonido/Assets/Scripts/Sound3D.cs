using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound3D : MonoBehaviour
{
    // Start is called before the first frame update
    public string soundName;
    private FMOD.Sound sound;
    private FMOD.VECTOR pos;
    private FMOD.Channel channel;
    public bool loop = true;
    public float _minDistance;
    public float _maxDistance;
    FMOD.System s;

    void Start()
    {
        s = SoundManager.instance.GetSystem();

        FMOD.MODE flags = FMOD.MODE._3D;

        if (loop)
            flags = FMOD.MODE._3D | FMOD.MODE.LOOP_NORMAL;
        
       
        SoundManager.instance.checkError(s.createSound("Assets/Sounds/" + soundName, flags, out sound));

        if (loop)
            Play();

        s.update();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        FMOD.ChannelGroup c = new FMOD.ChannelGroup();
        SoundManager.instance.checkError(s.playSound(sound, c, false, out channel));
        pos = new FMOD.VECTOR();

        pos.x = gameObject.transform.position.x;
        pos.y = gameObject.transform.position.y;
        pos.z = gameObject.transform.position.z;

        FMOD.VECTOR vel = new FMOD.VECTOR();
        vel.x = vel.y = vel.z = 0;

        FMOD.VECTOR aux = new FMOD.VECTOR();
        aux.x = aux.y = aux.z = 0;

        channel.setVolume(1.0f);
        SoundManager.instance.checkError(channel.set3DAttributes(ref pos, ref vel, ref aux));
        channel.set3DMinMaxDistance(_minDistance, _maxDistance);
        float min, max;
        channel.get3DMinMaxDistance(out min, out max);

    }


    public void Stop()
    {
        channel.stop();

    }
}
