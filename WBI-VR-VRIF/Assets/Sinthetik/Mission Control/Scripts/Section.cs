using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    [Serializable]
    public class Section
    {
        
        [FoldoutGroup("$sectionName")]
        [BoxGroup("$sectionName/Header", false)]
        [HorizontalGroup("$sectionName/Header/Horizontal")]
        [HideLabel]
        public string sectionName;

        // Linear Option Button

        [HorizontalGroup("$sectionName/Header/Horizontal", 100)]
        [ToggleLeft]
        [LabelWidth(1)]
        public bool hasMenu;

        // Show Events Button

        [HorizontalGroup("$sectionName/Header/Horizontal", 120)]
        [Button("$EventsButtonName")]
        public void ShowEvents()
        {
            this.hasEvents = !this.hasEvents;
        }

        private bool hasEvents = false;
        private string EventsButtonName()
        {
            string _name;

            if (hasEvents)
                _name = "Hide Events";
            else
                _name = "Show Events";

            return _name;
        }

        [BoxGroup("$sectionName/Events")]

        [ShowIfGroup("$sectionName/Events/hasEvents")]
        public UnityEvent entryEvent;

        [ShowIfGroup("$sectionName/Events/hasEvents")]
        public UnityEvent exitEvent;

        [FoldoutGroup("$sectionName")]
        public List<Task> tasks = new List<Task>();

        [FoldoutGroup("$sectionName/Outcomes")]
        public List<Module> successList = new List<Module>();

        [FoldoutGroup("$sectionName/Outcomes")]
        public List<Module> failList = new List<Module>();
    }
}