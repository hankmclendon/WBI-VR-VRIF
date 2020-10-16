using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinthetik.MissionControl
{
    public class MissionActivityHighlight : MonoBehaviour
    {
        MeshRenderer renderer;
        public Material normalMaterial;
        public Material highlightedMaterial;
        public MissionActivity activity;

        void Start()
        {
            renderer = gameObject.GetComponent<MeshRenderer>();
        }
        void Update()
        {
            if(activity.isActive)
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