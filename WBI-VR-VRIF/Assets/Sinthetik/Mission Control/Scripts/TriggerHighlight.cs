using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinthetik.MissionControl
{
    public class TriggerHighlight : MonoBehaviour
    {
        MeshRenderer renderer;
        public Material normalMaterial;
        public Material highlightedMaterial;
        public Trigger trigger;

        void Start()
        {
            renderer = gameObject.GetComponent<MeshRenderer>();
        }
        void Update()
        {
            if(trigger.isActive)
            {
                renderer.material = highlightedMaterial;
            }
            else
            {
                renderer.material = normalMaterial;
            }
        }
    }
}