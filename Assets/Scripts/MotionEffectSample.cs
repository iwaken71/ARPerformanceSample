using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionEffectSample : MonoBehaviour
{
    [SerializeField]
    GameObject leftHand;
    [SerializeField]
    GameObject rightHand;

    [SerializeField]
    GameObject effectPrefab;
    [SerializeField]
    GameObject effect2Prefab;


    [SerializeField]
    SkinnedMeshRenderer bodyMesh;

    void Start()
    {
        var effect = Instantiate(effectPrefab, Vector3.zero, Quaternion.identity);
        var effect2 = Instantiate(effect2Prefab, Vector3.zero, Quaternion.identity);
        effect.transform.SetParent(leftHand.transform, false);
        effect2.transform.SetParent(rightHand.transform, false);
    }

    void Update()
    {
        // wip flick
        if (Input.GetMouseButtonDown(0))
        {
            bodyMesh.enabled = !bodyMesh.enabled;
        }
    }
}