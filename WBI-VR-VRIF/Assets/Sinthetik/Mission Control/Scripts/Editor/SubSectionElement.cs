using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Sinthetik.MissionControl
{
    public class SubSectionElement : BaseSectionElement
    {
        public BaseSectionElement parentElement;
        public SubSectionElement(BaseSectionElement parentElement, MissionEditor editor, SerializedObject serializedObject, SerializedProperty listItem, int index) : base(editor, serializedObject, listItem, index)
        {
            this.parentElement = parentElement;
        }
        public override void SetContainer()
        {
            container = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Sinthetik/Mission Control/Scripts/Editor/SubSectionContainer.uxml");
            container.CloneTree(this);
        }
        public override void UpdateSubList()
        {
            listSlot.Clear();
            if(subList != null)
            {
                for(int i = 0; i < subList.arraySize; i++)
                {
                    SerializedProperty currentListItem = subList.GetArrayElementAtIndex(i);
                    ModuleElement listElement = new ModuleElement(serializedObject, currentListItem, i, this);
                    listSlot.Add(listElement);
                }
            }
        }
        public override void MoveThisItemUp()
        {
            parentElement.MoveItemUp(index);
        }
        public override void MoveThisItemDown()
        {
            parentElement.MoveItemDown(index);
        }
        public override void DeleteThisItem()
        {
            parentElement.DeleteListItem(index);
        }
    }
}