using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Sinthetik.MissionControl
{
    [CustomEditor(typeof(MissionControl))]
    [CanEditMultipleObjects]
    public class MissionControlEditor : Editor
    {
        MissionControl missionControl;
        VisualElement rootElement;
        VisualTreeAsset container;
        SerializedProperty missionList;
        VisualElement listSlot;
        public void OnEnable()
        {
            missionControl = (MissionControl)target;
            rootElement = new VisualElement();

            // Load in UXML template and USS styles, then apply them to the root element.
            container = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Sinthetik/Mission Control/Scripts/Editor/MissionControlContainer.uxml");
            container.CloneTree(rootElement);


            missionList = serializedObject.FindProperty("missionList");
        }
        public override VisualElement CreateInspectorGUI()
        {

            base.CreateInspectorGUI();
            serializedObject.Update();

            ObjectField dialogueObjectField = rootElement.Query<ObjectField>("dialogueObjectField").First();
            dialogueObjectField.objectType = typeof(GameObject);
            SerializedProperty dialoguePanel = serializedObject.FindProperty("dialoguePanel");
            dialogueObjectField.BindProperty(dialoguePanel);

            ObjectField menuObjectField = rootElement.Query<ObjectField>("menuObjectField").First();
            menuObjectField.objectType = typeof(GameObject);
            SerializedProperty menuPanel = serializedObject.FindProperty("menuPanel");
            menuObjectField.BindProperty(menuPanel);

            ObjectField timerObjectField = rootElement.Query<ObjectField>("timerObjectField").First();
            timerObjectField.objectType = typeof(GameObject);
            SerializedProperty timerPanel = serializedObject.FindProperty("timerPanel");
            timerObjectField.BindProperty(timerPanel);

            ObjectField audioObjectField = rootElement.Query<ObjectField>("audioObjectField").First();
            audioObjectField.objectType = typeof(GameObject);
            SerializedProperty audioPanel = serializedObject.FindProperty("audioPanel");
            audioObjectField.BindProperty(audioPanel);

            ObjectField choiceObjectField = rootElement.Query<ObjectField>("choiceObjectField").First();
            choiceObjectField.objectType = typeof(GameObject);
            SerializedProperty choicePanel = serializedObject.FindProperty("choicePanel");
            choiceObjectField.BindProperty(choicePanel);

            ObjectField instructionalObjectField = rootElement.Query<ObjectField>("instructionalObjectField").First();
            instructionalObjectField.objectType = typeof(GameObject);
            SerializedProperty instructionalPanel = serializedObject.FindProperty("instructionalPanel");
            instructionalObjectField.BindProperty(instructionalPanel);

            ObjectField debugObjectField = rootElement.Query<ObjectField>("debugObjectField").First();
            debugObjectField.objectType = typeof(GameObject);
            SerializedProperty debugPanel = serializedObject.FindProperty("debugPanel");
            debugObjectField.BindProperty(debugPanel);

            listSlot = rootElement.Query<VisualElement>("listSlot").First();

            return rootElement;
        }
    }
}