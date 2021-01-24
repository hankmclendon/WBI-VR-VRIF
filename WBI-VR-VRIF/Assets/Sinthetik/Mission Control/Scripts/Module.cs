using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEditor;
using System.Linq;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
#endif

namespace Sinthetik.MissionControl
{
    [Serializable]
    public class Module
    {
        public enum ModuleType { dialogue, audio, trigger, activity, timer, choice, menu };
        private bool hasEvents = false;
        private bool hasTrigger = false;
        private string triggerButtonName = "Add Trigger";

        [FoldoutGroup("$moduleName")]
        [HorizontalGroup("$moduleName/Horizontal")]
        [BoxGroup("$moduleName/Horizontal/Name")]
        [HideLabel]
        [OnValueChanged("RenameTrigger")]
        public string moduleName;

        [BoxGroup("$moduleName/Horizontal/Type")]
        [HideLabel]
        public ModuleType moduleType = ModuleType.dialogue;

        // Events ////////////////

        [BoxGroup("$moduleName/Horizontal/Events")]
        [Button("$EventsButtonName")]
        public void ShowEvents()
        {
            this.hasEvents = !this.hasEvents;
        }

        private string EventsButtonName()
        {
            string eventsButtonName;

            if (hasEvents)
                eventsButtonName = "Hide Events";
            else
                eventsButtonName = "Show Events";

            return eventsButtonName;
        }

        [BoxGroup("$moduleName/Events")]

        [ShowIfGroup("$moduleName/Events/hasEvents")]
        public UnityEvent entryEvent = new UnityEvent();

        [ShowIfGroup("$moduleName/Events/hasEvents")]
        public UnityEvent exitEvent = new UnityEvent();

        public void CallEntryEvent()
        {
            entryEvent?.Invoke();
        }
        public void CallExitEvent()
        {
            exitEvent?.Invoke();
        }

        // Audio ///////////////////////////

        [BoxGroup("$moduleName/audio", ShowLabel = false)]
        [ShowIfGroup("$moduleName/audio/moduleType", Value = ModuleType.audio)]
        [ShowIfGroup("$moduleName/dialogue/moduleType", Value = ModuleType.dialogue)]
        [ShowIfGroup("$moduleName/choice/moduleType", Value = ModuleType.choice)]
        public AudioClip audioClip;

        // Dialogue Data /////////////////////////

        [BoxGroup("$moduleName/dialogue", ShowLabel = false)]
        [ShowIfGroup("$moduleName/dialogue/moduleType", Value = ModuleType.dialogue)]
        [ShowIfGroup("$moduleName/choice/moduleType", Value = ModuleType.choice)]
        [InlineEditor]
        public ModuleData data;


        // Trigger ////////////////////////

        [BoxGroup("$moduleName/trigger", ShowLabel = false)]
        [ShowIfGroup("$moduleName/trigger/moduleType", Value = ModuleType.trigger)]
        [Button("$TriggerButtonName")]
        public void TriggerButton()
        {
            if (trigger == null)
            {
                GameObject triggerFolder = GameObject.Find("_Triggers");
                if (triggerFolder == null)
                {
                    triggerFolder = new GameObject();
                    triggerFolder.name = "_Triggers";
                }
                
                trigger = new GameObject();
                trigger.transform.parent = triggerFolder.transform;
                trigger.name = moduleName + "_Trigger";
                BoxCollider col = trigger.AddComponent<BoxCollider>();
                col.isTrigger = true;
                col.center = new Vector3(0, 0.5f, 0);
                trigger.AddComponent<Trigger>();
                trigger.layer = 2;

                GameObject triggerLocator = new GameObject();
                triggerLocator.name = "Trigger Locator";
                triggerLocator.transform.parent = trigger.transform;
                triggerLocator.transform.position = new Vector3(0, 0.5f, 0);
                triggerLocator.layer = 2;

                GameObject tempLocator = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                tempLocator.transform.parent = triggerLocator.transform;
                tempLocator.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                tempLocator.transform.position = new Vector3(0, 0.5f, 0);
                tempLocator.GetComponent<CapsuleCollider>().enabled = false;
                tempLocator.layer = 2;
            }
            else
            {
                GameObject[] newSelection = new GameObject[1];
                newSelection[0] = trigger;
                #if UNITY_EDITOR
                Selection.objects = newSelection;
#endif
            }

        }

        [ShowIfGroup("$moduleName/trigger/moduleType", Value = ModuleType.trigger)]
        public GameObject trigger;

        private string TriggerButtonName()
        {
            //trigger = GameObject.Find(moduleName + "_Trigger");
            if (trigger == null)
                triggerButtonName = "Add Trigger";
            else
                triggerButtonName = "Select Trigger";
            return triggerButtonName;
        }

        private void RenameTrigger()
        {
            if (trigger != null)
            {
                trigger.name = moduleName + "_Trigger";
            }
        }


        // Activity //////////////////////////

        [BoxGroup("$moduleName/activity", ShowLabel = false)]
        [ShowIfGroup("$moduleName/activity/moduleType", Value = ModuleType.activity)]
        [SceneObjectsOnly]
        public Activity activity;

        // Timer /////////////////////////////

        [BoxGroup("$moduleName/timer", ShowLabel = false)]
        [ShowIfGroup("$moduleName/timer/moduleType", Value = ModuleType.timer)]
        [LabelText("Time")]
        public float timer;

        // Choice /////////////////////////

        [BoxGroup("$moduleName/choice", ShowLabel = false)]
        [ShowIfGroup("$moduleName/choice/moduleType", Value = ModuleType.choice)]
        public enum Choice { NextModule, NextTask, NextSection, End, Custom };

        [ShowIfGroup("$moduleName/choice/moduleType", Value = ModuleType.choice)]
        public Choice choiceOne;

        [ShowIfGroup("$moduleName/choice/moduleType", Value = ModuleType.choice)]
        public Choice choiceTwo;

        // Menu Data /////////////////////////

        [BoxGroup("$moduleName/Menu")]
        
        [ShowIfGroup("$moduleName/Menu/moduleType", Value = ModuleType.menu)]
        [InlineEditor]
        public MenuContent menuContent;
    }

    
}

// classes for custom color foldout. need to figure out how to make the name dynamic
public class ColorFoldoutGroupAttribute : PropertyGroupAttribute
{
    public float R, G, B, A;

    public ColorFoldoutGroupAttribute(string path) : base(path)
    {

    }

    public ColorFoldoutGroupAttribute(string path, float r, float g, float b, float a = 1f) : base(path)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = a;
    }

    protected override void CombineValuesWith(PropertyGroupAttribute other)
    {
        var otherAttr = (ColorFoldoutGroupAttribute)other;

        this.R = Math.Max(otherAttr.R, this.R);
        this.G = Math.Max(otherAttr.G, this.G);
        this.B = Math.Max(otherAttr.B, this.B);
        this.A = Math.Max(otherAttr.A, this.A);
    }
}

#if UNITY_EDITOR
public class ColorFoldoutGroupAttributeDrawer : OdinGroupDrawer<ColorFoldoutGroupAttribute>
{
    private LocalPersistentContext<bool> isExpanded;

    protected override void Initialize()
    {
        this.isExpanded = this.GetPersistentValue<bool>("ColorFoldoutGroupAttributeDrawer.isExpanded",
            GeneralDrawerConfig.Instance.ExpandFoldoutByDefault);
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        GUIHelper.PushColor(new Color(this.Attribute.R, this.Attribute.G, this.Attribute.B, this.Attribute.A));
        SirenixEditorGUI.BeginBox();
        SirenixEditorGUI.BeginBoxHeader();
        GUIHelper.PopColor();

        this.isExpanded.Value = SirenixEditorGUI.Foldout(this.isExpanded.Value, label);
        SirenixEditorGUI.EndBoxHeader();

        if (SirenixEditorGUI.BeginFadeGroup(this, this.isExpanded.Value))
        {
            for (int i = 0; i < this.Property.Children.Count; i++)
            {
                this.Property.Children[i].Draw();
            }
        }
        SirenixEditorGUI.EndFadeGroup();
        SirenixEditorGUI.EndBox();
    }
}
#endif