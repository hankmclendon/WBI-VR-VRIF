using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Sinthetik.MissionControl
{
    [CustomEditor(typeof(Mission))]
    [CanEditMultipleObjects]
    public class MissionEditor : Editor
    {
        VisualTreeAsset container;
        VisualElement rootElement;
        public Mission mission;
        SerializedProperty subList;
        ListView sectionListView;
        VisualElement listSlot;
        VisualElement endSlot;
        public void OnEnable()
        {
            mission = (Mission)target;
            rootElement = new VisualElement();

            // Load in UXML template and USS styles, then apply them to the root element.
            container = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Sinthetik/Mission Control/Scripts/Editor/MissionContainer.uxml");
            container.CloneTree(rootElement);


            subList = serializedObject.FindProperty("subList");
        }
        public override VisualElement CreateInspectorGUI()
        {

            base.CreateInspectorGUI();
            serializedObject.Update();

            TextField nameTextField = rootElement.Query<TextField>("nameTextField").First();
            SerializedProperty missionName = serializedObject.FindProperty("missionName");
            nameTextField.BindProperty(missionName);

            Button newButton = rootElement.Query<Button>("newButton").First();
            newButton.clickable.clicked += AddListItem;

            listSlot = rootElement.Query<VisualElement>("listSlot").First();

            endSlot = rootElement.Query<VisualElement>("endSlot").First();
            SerializedProperty endOfGame = serializedObject.FindProperty("endOfGame");
            ModuleElement endElement = new ModuleElement(serializedObject, endOfGame);
            endSlot.Add(endElement);

            UpdateSubList();
            return rootElement;
        }
        public void AddListItem()
        {
            subList.InsertArrayElementAtIndex(subList.arraySize);
            int i = subList.arraySize - 1;
            serializedObject.ApplyModifiedProperties();            
            UpdateSubList();
        }
        public virtual void DeleteListItem(int index)
        {
            subList.DeleteArrayElementAtIndex(index);
            serializedObject.ApplyModifiedProperties();
            UpdateSubList();
        }

        public void UpdateSubList()
        {
            listSlot.Clear();
            if(subList != null)
            {
                for(int i = 0; i < subList.arraySize; i++)
                {
                    SerializedProperty currentListItem = subList.GetArrayElementAtIndex(i);
                    SectionElement listElement = new SectionElement(this, serializedObject, currentListItem, i);
                    listSlot.Add(listElement);
                }
            }
        }
        public void MoveItemUp(int index)
        {
            if(index > 0)
                subList.MoveArrayElement(index, index - 1);
            serializedObject.ApplyModifiedProperties();
            UpdateSubList();
        }
        public void MoveItemDown(int index)
        {
            if(index < subList.arraySize - 1)
                subList.MoveArrayElement(index, index + 1);
            serializedObject.ApplyModifiedProperties();
            UpdateSubList();
        }
    }
}

