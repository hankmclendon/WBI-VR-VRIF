using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Sinthetik.MissionControl
{
    public class BaseSectionElement : VisualElement
    {
        #region Variables
        public VisualTreeAsset container;
        public SerializedProperty listItemName;
        public VisualElement listSlot;
        public SerializedProperty subList;
        public SerializedProperty listItem;
        public SerializedObject serializedObject;
        public int index;
        public SerializedProperty hasSuccess;
        public Toggle successToggle;
        public VisualElement successSlot;
        public SerializedProperty hasFail;
        public Toggle failToggle;
        public VisualElement failSlot;
        public MissionEditor editor;
        public Foldout listFoldout;
        public SerializedProperty foldoutState;
        public SerializedProperty outcomeFoldoutState;
        public SerializedProperty success;
        public SerializedProperty fail;
        public ModuleElement successElement;
        public ModuleElement failElement;
        public Foldout outcomeFoldout;

        #endregion
        public BaseSectionElement(MissionEditor editor, SerializedObject serializedObject, SerializedProperty listItem, int index)
        {
            this.index = index;
            this.listItem = listItem;
            this.serializedObject = serializedObject;
            this.editor = editor;

            SetContainer();

            subList = listItem.FindPropertyRelative("subList");

            TextField nameTextField = this.Query<TextField>("nameTextField").First();
            listItemName = listItem.FindPropertyRelative("name");
            nameTextField.BindProperty(listItemName);

            Button newButton = this.Query<Button>("newButton").First();
            newButton.clickable.clicked += AddListItem;

            Button deleteButton = this.Query<Button>("deleteButton").First();
            deleteButton.clickable.clicked += DeleteThisItem;

            Button upButton = this.Query<Button>("upButton").First();
            upButton.clickable.clicked += MoveThisItemUp;

            Button downButton = this.Query<Button>("downButton").First();
            downButton.clickable.clicked += MoveThisItemDown;

            listFoldout = this.Query<Foldout>("listFoldout").First();
            foldoutState = listItem.FindPropertyRelative("foldoutState");
            listFoldout.BindProperty(foldoutState);

            outcomeFoldout = this.Query<Foldout>("outcomeFoldout").First();
            outcomeFoldoutState = listItem.FindPropertyRelative("outcomeFoldoutState");
            outcomeFoldout.BindProperty(outcomeFoldoutState);
            
            listSlot = this.Query<VisualElement>("listSlot").First();

            hasSuccess = listItem.FindPropertyRelative("hasSuccess");

            successToggle = this.Query<Toggle>("successToggle").First();
            successToggle.BindProperty(hasSuccess);

            successSlot = this.Query<VisualElement>("successSlot").First();
            success = listItem.FindPropertyRelative("success");
            successElement = new ModuleElement(serializedObject, success);
            successSlot.Add(successElement);

            hasFail = listItem.FindPropertyRelative("hasFail");

            failToggle = this.Query<Toggle>("failToggle").First();
            failToggle.BindProperty(hasFail);
            
            failSlot = this.Query<VisualElement>("failSlot").First();
            fail = listItem.FindPropertyRelative("fail");
            failElement = new ModuleElement(serializedObject, fail);
            failSlot.Add(failElement);

            UpdateSubList();
        }
        #region Virtual Methods
        public virtual void SetContainer()
        {
            container = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Sinthetik/Mission Control/Scripts/Editor/SectionContainer.uxml");
            container.CloneTree(this);
        }
        public virtual void MoveThisItemUp()
        {
            editor.MoveItemUp(index);
        }
        public virtual void MoveThisItemDown()
        {
            editor.MoveItemDown(index);
        }
        public virtual void DeleteThisItem()
        {
            editor.DeleteListItem(index);
        }
        public virtual void UpdateSubList()
        {
            listSlot.Clear();
            if(subList != null)
            {
                for(int i = 0; i < subList.arraySize; i++)
                {
                    SerializedProperty currentListItem = subList.GetArrayElementAtIndex(i);
                    SubSectionElement listElement = new SubSectionElement(this, editor, serializedObject, currentListItem, i);
                    listSlot.Add(listElement);
                }
            }
        }

        #endregion
        
        #region ListItem Methods
        public void AddListItem()
        {
            subList.InsertArrayElementAtIndex(subList.arraySize);
            int i = subList.arraySize - 1;
            serializedObject.ApplyModifiedProperties();            
            UpdateSubList();
        }
        public void DeleteListItem(int index)
        {
            subList.DeleteArrayElementAtIndex(index);
            serializedObject.ApplyModifiedProperties();
            UpdateSubList();
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

        #endregion
    }
}