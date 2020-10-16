using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
namespace Sinthetik.MissionControl
{
    public class SectionElement : BaseSectionElement
    {
        public SectionElement(MissionEditor editor, SerializedObject serializedObject, SerializedProperty listItem, int index) : base(editor, serializedObject, listItem, index)
        {
            
            SerializedProperty hasMenu = listItem.FindPropertyRelative("hasMenu");

            Toggle menuToggle = this.Query<Toggle>("menuToggle").First();
            menuToggle.BindProperty(hasMenu);
        }
    }
}