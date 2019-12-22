using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectSystem : MonoBehaviour,IEffectSystem
{

    void Awake()
    {
    }
    public void Initialize()
    {

    }
    public void Play()
    {
        Debug.Log(transform.name + " is Playing");
    }
    public void Stop()
    {

    }
}
