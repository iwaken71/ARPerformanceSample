using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PostEffectManager : MonoBehaviour
{

    [SerializeField]
    public  PostEffectEventTask[] events;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();

    }

    private void Initialize()
    {

    }

    // Update is called once per frame
    void Update()
    {

        StartTimeCheck();
        EndTimeCheck();
    }

    void StartTimeCheck()
    {
        if (!TimerManager.Instance.IsPlaying) {
            return;
        }
        if (events == null) {
            return;
        }
        if (events.Length == 0)
        {
            return;
        }
        float timer = TimerManager.Instance.Timer;
        var eventsTask = events.Where(events => !events.HasPlayed).Where(events => events.startTime < timer).ToArray();
        if (eventsTask.Length <= 0)
        {
            return;
        }
        foreach (var eve in eventsTask)
        {
            eve.Play();
        }
    }
    void EndTimeCheck() {
        if (!TimerManager.Instance.IsPlaying)
        {
            return;
        }
        if (events == null)
        {
            return;
        }
        if (events.Length == 0)
        {
            return;
        }
        float timer = TimerManager.Instance.Timer;
        var eventsTask = events.Where(events => events.HasPlayed).Where(events => events.endTime < timer).ToArray();
        if (eventsTask.Length <= 0)
        {
            return;
        }
        foreach (var eve in eventsTask)
        {
            eve.Stop();
        }
    }
}

[System.Serializable]
public class PostEffectEventTask 
{
    public float startTime;
    public float endTime;

    public PostEffectSystem[] systems;
    public bool HasPlayed { private set; get; }// すでに始まっている

    public void Initialize()
    {
        HasPlayed = false;
        foreach (var system in systems)
        {
            system.Initialize();
        }
        
    }

    public void Play()
    {
        if (systems.Length == 0)
        {
            return;
        }
        if (HasPlayed)
        {
            return;
        }

        HasPlayed = true;
        foreach (var system in systems)
        {
            system.Play();
        }
    }
    public void Stop()
    {
        if (!HasPlayed)
        {
            return;
        }
        if (systems.Length == 0) {
            return;
        }
        foreach (var system in systems)
        {
            system.Stop();
        }
    }
}