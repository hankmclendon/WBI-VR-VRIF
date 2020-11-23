using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Sinthetik.MissionControl
{
    public class TriggerHighlight : MonoBehaviour
    {
        //MeshRenderer renderer;
        //public Material normalMaterial;
        //public Material highlightedMaterial;
        public Trigger trigger;

        //public void OnEnable()
        //{
        //    trigger.triggerStartEvent += ShowHighlight;
        //    trigger.triggerStopEvent += HideHighlight;
        //}

        //public void OnDisable()
        //{
        //    trigger.triggerStartEvent -= ShowHighlight;
        //    trigger.triggerStopEvent -= HideHighlight;
        //}

        void Start()
        {
            gameObject.SetActive(false);
            //renderer = gameObject.GetComponent<MeshRenderer>();
        }

        public void ShowHighlight()
        {
            gameObject.SetActive(true);
        }

        public void HideHighlight()
        {
            gameObject.SetActive(false);
        }

        //void Update()
        //{
        //    if(trigger.isActive)
        //    {
        //        //renderer.material = highlightedMaterial;
        //        gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        //renderer.material = normalMaterial;
        //        gameObject.SetActive(false);
        //    }
        //}


    }
}