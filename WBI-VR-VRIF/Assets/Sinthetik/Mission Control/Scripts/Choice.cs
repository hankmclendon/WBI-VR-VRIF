using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinthetik.MissionControl
{
    [System.Serializable]
    public class Choice
    {
        public bool result;
        public Module choice = new Module();
    }
}