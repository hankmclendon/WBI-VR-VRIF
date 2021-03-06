﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinthetik.MissionControl
{
    [CreateAssetMenu(fileName = "DialogueContent", menuName = "Sinthetik/Mission Control/Dialogue Content", order = 0)]
    public class ModuleData : ScriptableObject {
        public string title;
        [TextArea(6,10)]
        public string copy;
        public string buttonOneText;
        public string buttonTwoText;
        public Sprite backgroundImage;
        //public AudioClip voiceOver;

    }
}