using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinthetik.MissionControl
{
    public class TriggerTooltip : MonoBehaviour
    {
        public Trigger trigger;
        public GameObject toolTip;

        void Update()
        {
            if (trigger.isActive)
            {
                toolTip.SetActive(true);
            }
            else
            {
                toolTip.SetActive(false);
            }
        }
    }
}
