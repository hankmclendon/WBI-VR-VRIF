using UnityEngine;
using UnityEditor;

namespace Sinthetik.MissionControl
{
    public class MissionControlMissions : Editor
    {
        [MenuItem("Sinthetik/Mission Control/New Mission", false, 1)]
        static void CreateMission()
        {

            GameObject missionFolder = GameObject.Find("_Missions");
            if (missionFolder == null)
            {
                missionFolder = new GameObject();
                missionFolder.name = "_Missions";
            }

            GameObject newMission = new GameObject();
            newMission.name = "New Mission";
            newMission.AddComponent<Mission>();
            newMission.transform.parent = missionFolder.transform;
        }
    }
}