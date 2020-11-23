using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Sinthetik.MissionControl
{
    [CreateAssetMenu(fileName = "MenuItem", menuName = "Sinthetik/Mission Control/Menu Item")]
    [InlineEditor]
    public class MenuContent : ScriptableObject
    {
        [HorizontalGroup("icons", 80)]
        [PreviewField(Alignment = ObjectFieldAlignment.Center)]
        [HideLabel]
        [Title("Normal", TitleAlignment = TitleAlignments.Centered, HorizontalLine = false)]
        public Sprite normal;

        [HorizontalGroup("icons", 80)]
        [PreviewField(Alignment = ObjectFieldAlignment.Center)]
        [HideLabel]
        [Title("Rollover", TitleAlignment = TitleAlignments.Centered, HorizontalLine = false)]
        public Sprite rollover;

        [HorizontalGroup("icons", 80)]
        [PreviewField(Alignment = ObjectFieldAlignment.Center)]
        [HideLabel]
        [Title("Selected", TitleAlignment = TitleAlignments.Centered, HorizontalLine = false)]
        public Sprite selected;

        [HorizontalGroup("icons", 80)]
        [PreviewField(Alignment = ObjectFieldAlignment.Center)]
        [HideLabel]
        [Title("Completed", TitleAlignment = TitleAlignments.Centered, HorizontalLine = false)]
        public Sprite completed;

        [HorizontalGroup("icons", 80)]
        [PreviewField(Alignment = ObjectFieldAlignment.Center)]
        [HideLabel]
        [Title("Failed", TitleAlignment = TitleAlignments.Centered, HorizontalLine = false)]
        public Sprite failed;

        [Space]
        public Sprite titleContent;

        [Title("Description", HorizontalLine = false)]
        [HideLabel]
        [MultiLineProperty(5)]
        [Space]
        public string description;

        [Space]
        public Vector3 location;
    }
}