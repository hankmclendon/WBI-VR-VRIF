using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    public class MissionActivity : MonoBehaviour
    {
        public bool isActive = false;
        public static event System.Action<GameObject> activityComplete;
        void Update()
        {
            if(isActive)
            {
                if (Input.GetKeyDown("space"))
                {
                    activityComplete?.Invoke(gameObject);
                    isActive = false;
                }
            }
            if(isActive)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    activityComplete?.Invoke(gameObject);
                    isActive = false;
                }
            }
        }
        public void Skip()
        {
            activityComplete?.Invoke(gameObject);
            isActive = false;
        }
    }   
}