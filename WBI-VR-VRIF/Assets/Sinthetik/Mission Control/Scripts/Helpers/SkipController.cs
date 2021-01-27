using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using Sinthetik.MissionControl;

public class SkipController : MonoBehaviour
{
    public Mission mission;
    public SceneChanger sceneChanger;
    void Update()
    {
        if (InputBridge.Instance.AButtonDown)
        {
            mission.SkipModule();
        }
        if (InputBridge.Instance.XButtonDown)
        {
            sceneChanger.LoadWaitingRoomScene();
        }
    }
}
