using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionEffectSample : MonoBehaviour
{
    [SerializeField]
    GameObject leftHand;

    [SerializeField]
    GameObject effectPrefab;
    [SerializeField]
    SkinnedMeshRenderer bodyMesh;

    void Start()
    {
        var effect = Instantiate(effectPrefab, Vector3.zero, Quaternion.identity);
        effect.transform.SetParent(leftHand.transform, false);
    }

    void Update()
    {
        // wip flick
        bodyMesh.enabled = false;
        bodyMesh.enabled = true;
    }
}
