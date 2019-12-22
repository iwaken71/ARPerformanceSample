using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PostEffectManager : MonoBehaviour
{

    public static PostEffectManager Instance = null;

    int postEffectIndex;

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
}


