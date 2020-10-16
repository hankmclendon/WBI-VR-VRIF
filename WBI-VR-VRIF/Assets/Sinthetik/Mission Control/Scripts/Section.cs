using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinthetik.MissionControl
{
    [System.Serializable]
    public class Section
    {
        public List<SubSection> subList = new List<SubSection>();
        public string name;
        public bool hasMenu = false;
        public bool foldoutState = true;
        public bool outcomeFoldoutState = false;
        public bool isComplete = false;
        public bool hasSuccess = false;
        public bool hasFail = false;
        public Module success = new Module(); 
        public Module fail = new Module();
        
    }
}