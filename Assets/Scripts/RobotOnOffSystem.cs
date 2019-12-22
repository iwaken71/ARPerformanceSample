using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotOnOffSystem : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer rd;

    public void ChangeOnOff() {
        rd.enabled = !rd.enabled;
    }
}
