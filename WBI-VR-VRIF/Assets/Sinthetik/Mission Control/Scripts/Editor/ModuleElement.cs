using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Sinthetik.MissionControl
{
    public class ModuleElement : VisualElement
    {
        VisualTreeAsset container;
        SerializedProperty listItemName;
        SerializedProperty listItem;
        BaseSectionElement parentElement;
        SerializedObject serializedObject;
        int index;
        Button triggerButton;
        SerializedProperty trigger;
        SerializedProperty choiceOne;
        SerializedProperty choiceTwo;
        public ModuleElement( SerializedObject serializedObject, SerializedProperty listItem, int index = 0, BaseSectionElement parentElement = null)
        {
            #region Setup

            this.parentElement = parentElement;
            this.listItem = listItem;
            this.index = index;
            this.serializedObject = serializedObject;

            container = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Sinthetik/Mission Control/Scripts/Editor/ModuleContainer.uxml");
            container.CloneTree(this);

            #endregion

            #region Header

            TextField nameTextField = this.Query<TextField>("nameTextField").First();
            listItemName = listItem.FindPropertyRelative("name");
            nameTextField.BindProperty(listItemName);

            Button deleteButton = this.Query<Button>("deleteButton").First();
            deleteButton.clickable.clicked += () => {
                parentElement?.DeleteListItem(index);
            };
            Button upButton = this.Query<Button>("upButton").First();
            upButton.clickable.clicked += () => {
                parentElement?.MoveItemUp(index);
            };
            Button downButton = this.Query<Button>("downButton").First();
            downButton.clickable.clicked += () => {
                parentElement?.MoveItemDown(index);
            };

            EnumField moduleTypeEnum = this.Query<EnumField>("moduleTypeEnum").First();
            SerializedProperty moduleType = listItem.FindPropertyRelative("moduleType");
            moduleTypeEnum.BindProperty(moduleType);

            moduleTypeEnum.RegisterCallback<ChangeEvent<System.Enum>>((evt) =>
            {
                
                parentElement?.UpdateSubList();
            });
            #endregion

            #region Data

            ObjectField dataObjectField = this.Query<ObjectField>("dataObjectField").First();
            dataObjectField.objectType = typeof(ModuleData);
            SerializedProperty moduleData = listItem.FindPropertyRelative("moduleData");
            dataObjectField.BindProperty(moduleData);
            dataObjectField.style.display = moduleType.intValue == 0 || moduleType.intValue == 5 || moduleType.intValue == 6 ? DisplayStyle.Flex : DisplayStyle.None;

            #endregion

            #region Audio

            ObjectField audioObjectField = this.Query<ObjectField>("audioObjectField").First();
            audioObjectField.objectType = typeof(AudioClip);
            SerializedProperty audioClip = listItem.FindPropertyRelative("audioClip");
            audioObjectField.BindProperty(audioClip);
            audioObjectField.style.display = moduleType.intValue == 1 ? DisplayStyle.Flex : DisplayStyle.None;

            #endregion

            #region Trigger

            triggerButton = this.Query<Button>("addTriggerButton").First();
            trigger = listItem.FindPropertyRelative("destinationTrigger");

            triggerButton.text = trigger.objectReferenceValue == null ? "Add Trigger" : "Select Trigger";
            triggerButton.clickable.clicked += ManageTrigger;
            
            Button deleteTriggerButton = this.Query<Button>("deleteTriggerButton").First();
            deleteTriggerButton.clickable.clicked += RemoveTrigger;

            Button renameTriggerButton = this.Query<Button>("renameTriggerButton").First();
            renameTriggerButton.clickable.clicked += RenameTrigger;

            VisualElement triggerElement = this.Query<VisualElement>("triggerElement").First();
            triggerElement.style.display = moduleType.intValue == 2 ? DisplayStyle.Flex : DisplayStyle.None;

            #endregion

            #region Activity

            ObjectField activityObjectField = this.Query<ObjectField>("activityObjectField").First();
            activityObjectField.objectType = typeof(GameObject);
            SerializedProperty missionActivity = listItem.FindPropertyRelative("missionActivity");
            activityObjectField.BindProperty(missionActivity);
            activityObjectField.style.display = moduleType.intValue == 3 || moduleType.intValue == 6 ? DisplayStyle.Flex : DisplayStyle.None;

            #endregion

            #region Timer

            FloatField timerFloatField = this.Query<FloatField>("timerFloatField").First();
            SerializedProperty timeout = listItem.FindPropertyRelative("timeout");
            timerFloatField.BindProperty(timeout);
            timerFloatField.style.display = moduleType.intValue == 4 ? DisplayStyle.Flex : DisplayStyle.None;

            #endregion

            #region Choice

            // VisualElement choiceElement = this.Query<VisualElement>("choiceElement").First();
            // choiceElement.style.display = moduleType.intValue == 5 ? DisplayStyle.Flex : DisplayStyle.None;

            // VisualElement choiceOneSlot = this.Query<VisualElement>("choiceOneSlot").First();
            // choiceOne = listItem.FindPropertyRelative("choiceOne");
            // // ModuleElement choiceOneModule = new ModuleElement(parentElement, listItem, 0);
            // // choiceOneSlot.Add(choiceOneModule);

            // VisualElement choiceTwoSlot = this.Query<VisualElement>("choiceTwoSlot").First();
            // choiceTwo = listItem.FindPropertyRelative("choiceTwo");
            

            #endregion

            #region ModuleType Styling

            VisualElement header = this.Query<VisualElement>("header").First();
            VisualElement options = this.Query<VisualElement>("options").First();
            VisualElement containerElement = this.Query<VisualElement>("containerElement").First();
            if(moduleType.intValue == 1)
            {
                containerElement.ToggleInClassList("module-border-audio");
                header.ToggleInClassList("module-header-audio");
                options.ToggleInClassList("module-optionbar-audio");
            }
            else if(moduleType.intValue == 2)
            {
                containerElement.ToggleInClassList("module-border-trigger");
                header.ToggleInClassList("module-header-trigger");
                options.ToggleInClassList("module-optionbar-trigger");
            }
            else if(moduleType.intValue == 3)
            {
                containerElement.ToggleInClassList("module-border-activity");
                header.ToggleInClassList("module-header-activity");
                options.ToggleInClassList("module-optionbar-activity");
            }
            else if (moduleType.intValue == 4)
            {
                containerElement.ToggleInClassList("module-border-timer");
                header.ToggleInClassList("module-header-timer");
                options.ToggleInClassList("module-optionbar-timer");
            }

            #endregion
        }

        #region Trigger Methods
        private void ManageTrigger()
        {
            if(trigger.objectReferenceValue == null)
            {
                GameObject triggerFolder = GameObject.Find("_Triggers");
                if (triggerFolder == null)
                {
                    triggerFolder = new GameObject();
                    triggerFolder.name = "_Triggers";
                }
                GameObject destinationTrigger = PrefabUtility.InstantiatePrefab(Resources.Load("Trigger", typeof(GameObject)), triggerFolder.transform) as GameObject;
                destinationTrigger.name = listItemName.stringValue + "_Destination";
                trigger.objectReferenceValue = destinationTrigger;
                serializedObject.ApplyModifiedProperties();
                parentElement?.UpdateSubList();
            }
            else
            {
                SelectTrigger();
            }
        }

        private void SelectTrigger()
        {
            Object[] newSelection = new Object[1];
            newSelection[0] = trigger.objectReferenceValue;
            Selection.objects = newSelection;
        }

        public void RenameTrigger()
        {
            GameObject destinationTrigger = (GameObject)trigger.objectReferenceValue;
            if(destinationTrigger != null) destinationTrigger.name = listItemName.stringValue + "_Destination";
        }

        private void RemoveTrigger()
        {
            // parentElement.parentElement.editor.mission.RemoveTrigger((GameObject)trigger.objectReferenceValue);
            // parentElement.UpdateSubList();
        }
        
        #endregion
    }
}