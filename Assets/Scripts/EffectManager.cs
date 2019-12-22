using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectManager : MonoBehaviour
{

   //[SerializeField] GameObject RootRobot;


    [SerializeField] EventTask[] events;
    bool isPlaying = false;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();


    }

    private void Initialize()
    {
        isPlaying = false;
        timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isPlaying) {
            return;
        }

        timer += Time.deltaTime;
        TimerCheck();
    }

    void TimerCheck() {
        var eventsTask = events.Where(events => !events.HasPlayed).Where(events => events.time < timer).ToArray();
        if (eventsTask.Length <= 0) {
            return;
        }
        foreach (var eve in eventsTask)
        {
            eve.Play();
        }
    }

    public void StartTimer() {
        isPlaying = true;
    }


    public void ResetTimer() {
        timer = 0;
        isPlaying = false;
    }


}

[System.Serializable]
public class EventTask
{
    public float time;
    public int eventID;
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
