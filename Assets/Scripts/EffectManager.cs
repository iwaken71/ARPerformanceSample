using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectManager : MonoBehaviour
{

    [SerializeField] EventTask[] events;
   

    // Start is called before the first frame update
    void Start()
    {
        Initialize();


    }

    private void Initialize()
    {
      


        foreach (var ev in events) {
        }

    }

    // Update is called once per frame
    void Update()
    {
        CheckStartTime();
    }

    void CheckStartTime() {
        if (!TimerManager.Instance.IsPlaying) {
            return;
        }
        float timer = TimerManager.Instance.Timer;

        var eventsTask = events.Where(events => !events.HasPlayed).Where(events => events.startTime < timer).ToArray();
        if (eventsTask.Length <= 0) {
            return;
        }
        foreach (var eve in eventsTask)
        {
            eve.Play();
        }
    }

}

[System.Serializable]
public class EventTask
{
    public float startTime;
    //public int eventID;
    public ParticleOnlyEffectSystem[] systems;
    public bool HasPlayed { private set; get; }// すでに始まっている

    public void Initialize() {
        HasPlayed = false;
        foreach (var system in systems) {
            system.Initialize();
        }
    }

    public void Play()
    {
        if (HasPlayed) {
            return;
        }
        HasPlayed = true;
        foreach (var system in systems)
        {
            system.Play();
        }
    }
    public void Stop() {
        foreach (var system in systems)
        {
            system.Stop();
        }
    }
}
