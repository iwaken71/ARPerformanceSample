using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ターゲットのGameObjectにエフェクトをアタッチするスクリプト
/// </summary>
public class MotionEffectSample : MonoBehaviour
{
    #region エフェクトをアタッチするターゲット
    [SerializeField]
    GameObject leftHand;
    [SerializeField]
    GameObject rightHand;
    #endregion

    #region エフェクトのprefab
    [SerializeField]
    GameObject effectPrefab;
    [SerializeField]
    GameObject effect2Prefab;
    #endregion

    [SerializeField]
    SkinnedMeshRenderer bodyMesh;

    void Start()
    {
        // エフェクトをInstantiateしGameObjectにアタッチ
        var effect = Instantiate(effectPrefab, Vector3.zero, Quaternion.identity);
        var effect2 = Instantiate(effect2Prefab, Vector3.zero, Quaternion.identity);
        effect.transform.SetParent(leftHand.transform, false);
        effect2.transform.SetParent(rightHand.transform, false);
    }

    void Update()
    {

    }

    public void RobotOnOff() {
        bodyMesh.enabled = !bodyMesh.enabled;
    }
}

