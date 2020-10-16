using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Sinthetik.MissionControl {

   public class MissionControl : MonoBehaviour 
   {
        public GameObject menuPanel;
        public GameObject debugPanel;
        public GameObject dialoguePanel;
        public GameObject timerPanel;
        public GameObject audioPanel;
        public GameObject choicePanel;
        public GameObject instructionalPanel;
        public List<Section> missionList = new List<Section>( );
   }
}