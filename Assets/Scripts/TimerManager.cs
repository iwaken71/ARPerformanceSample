using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{

    public static TimerManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    float timer;
    bool isPlaying = false;
    public float Timer { get { return timer; } }
    public bool IsPlaying { get { return isPlaying; } }
    // Start is called before the first frame update
    void Start()
    {

        
    }

    private void Initialize()
    {
        timer = 0;
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isPlaying)
        {
            return;
        }

        timer += Time.deltaTime;

    }
    public void StartTimer()
    {
        isPlaying = true;
    }

    public void ResetTimer()
    {
        timer = 0;
        isPlaying = false;
    }
}
