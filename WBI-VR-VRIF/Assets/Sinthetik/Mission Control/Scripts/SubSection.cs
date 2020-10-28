using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinthetik.MissionControl
{
    [System.Serializable]
    public class SubSection
    {
        public List<Module> subList = new List<Module>();
        public SubSectionData data;
        public string name;
        public bool foldoutState = true;
        public bool outcomeFoldoutState = false;
        public bool isComplete = false;
        public bool deactivated = false;
        public bool hasSuccess = false;
        public bool hasFail = false;
        public Module success = new Module();
        public Module fail = new Module();
    }
}