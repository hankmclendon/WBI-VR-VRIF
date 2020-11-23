using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using Sinthetik.MissionControl;

public class SkipController : MonoBehaviour
{
    public Mission mission;
    void Update()
    {
        if (InputBridge.Instance.AButtonDown)
        {
            mission.SkipModule();
        }
        
    }
}
