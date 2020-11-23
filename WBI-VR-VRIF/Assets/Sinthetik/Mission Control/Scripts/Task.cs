using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    [Serializable]
    public class Task
    {
        [HideInInspector]
        public bool isComplete = false;
        [HideInInspector]
        public bool deactivated = false;
        private bool showMenu;
        private bool showEvents;

        [FoldoutGroup("$taskName")]
        [BoxGroup("$taskName/Header", false)]
        [HorizontalGroup("$taskName/Header/Horizontal")]
        [HideLabel]
        public string taskName;

        // hide/show menu content button

        [HorizontalGroup("$taskName/Header/Horizontal", 120)]
        [Button("$MenuContentButtonName")]
        public void ShowMenuContent()
        {
            this.showMenu = !this.showMenu;
        }

        private string MenuContentButtonName()
        {
            string _name;

            if (showMenu)
                _name = "Hide Menu Data";
            else
                _name = "Show Menu Data";

            return _name;
        }

        // hide/show events button

        [HorizontalGroup("$taskName/Header/Horizontal", 120)]
        [Button("$EventsButtonName")]
        public void ShowEvents()
        {
            this.showEvents = !this.showEvents;
        }

        private string EventsButtonName()
        {
            string _name;

            if (showEvents)
                _name = "Hide Events";
            else
                _name = "Show Events";

            return _name;
        }

        [BoxGroup("$taskName/MenuData")]
        [ShowIfGroup("$taskName/MenuData/showMenu")]
        public MenuContent menuContent;

        [BoxGroup("$taskName/Events")]

        [ShowIfGroup("$taskName/Events/showEvents")]
        public UnityEvent entryEvent;

        [ShowIfGroup("$taskName/Events/showEvents")]
        public UnityEvent exitEvent;

        [FoldoutGroup("$taskName")]
        public List<Module> modules;

        [FoldoutGroup("$taskName/Outcomes")]
        public List<Module> successList = new List<Module>();

        [FoldoutGroup("$taskName/Outcomes")]
        public List<Module> failList = new List<Module>();
    }
}