using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinthetik.MissionControl
{
    [CreateAssetMenu(fileName = "SubSectionData", menuName = "MissionControl/SubSection Data", order = 0)]
    public class SubSectionData : ScriptableObject {
        public string name;
        public string title;
        public Sprite titleImage;
        [TextArea(6,10)]
        public string descriptionCopy;
        public Sprite despriptionImage;
        public AudioClip descriptionAudio;
        public string buttonTitle;
        public Sprite buttonNormal;
        public Sprite buttonRollover;
        public Sprite buttonSelected;
        public Vector3 locator;
    }
}