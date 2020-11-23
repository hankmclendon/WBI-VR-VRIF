using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    public class ActivityTest : MonoBehaviour
    {
        public UnityEvent keyPressed;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                keyPressed?.Invoke();
            }
        }
    }
}