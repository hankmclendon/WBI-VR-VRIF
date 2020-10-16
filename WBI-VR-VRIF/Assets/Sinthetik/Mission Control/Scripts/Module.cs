using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinthetik.MissionControl
{
    [System.Serializable]
    public class Module
    {
        [SerializeField]
        public string name;
        [SerializeField]
        public ModuleData moduleData;
        public enum ModuleType { Dialogue, Audio, Trigger, Activity, Timer, Choice, Instructional };
        [SerializeField]
        public ModuleType moduleType = ModuleType.Dialogue;
        public GameObject destinationTrigger;
        public GameObject missionActivity;
        public float timeout;
        public GameObject customActivity;
        public AudioClip audioClip;

    }
}