using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinthetik.MissionControl
{
    public class MissionActivityTooltip : MonoBehaviour
    {
        public MissionActivity activity;
        public GameObject toolTip;

        void Update()
        {
            if (activity.isActive)
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