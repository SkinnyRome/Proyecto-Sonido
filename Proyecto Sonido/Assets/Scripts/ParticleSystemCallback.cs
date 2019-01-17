using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemCallback : MonoBehaviour
{

    Sound3D _sound;
    ParticleSystem _particleSystem;
    private int _numberOfParticles;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = gameObject.GetComponent<ParticleSystem>();
        _sound = gameObject.GetComponent<Sound3D>();
    }

    // Update is called once per frame
    void Update()
    {
        var count = _particleSystem.particleCount;
        if (count < _numberOfParticles)
        { //particle has died
            _sound.Stop();
        }
        else if(count > _numberOfParticles)
        { //particle has been born
            _sound.Play();
        }
        _numberOfParticles = count;
    }
}
