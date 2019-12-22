using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleOnlyEffectSystem : MonoBehaviour,IEffectSystem
{
    ParticleSystem system;

    void Awake() {
        system = GetComponent<ParticleSystem>();
        system.playOnAwake = false;
    }
    public  void Initialize() {

    }
    public void Play() {
        Debug.Log(transform.name + " is Playing");
        system.Play();
    }
    public void Stop() {
        system.Stop();
    }
}
