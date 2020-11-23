using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MissionControlPanels : Editor
{

    [MenuItem("Sinthetik/Mission Control/Install Panels", false, 1)]
    static void CreatePanels()
    {
        
        GameObject panelCanvas = GameObject.Find("_Panels");
        if (panelCanvas == null)
        {
            panelCanvas = new GameObject();
            panelCanvas.layer = 5;
            panelCanvas.name = "_Panels";
            Canvas canvas = panelCanvas.AddComponent<Canvas>();
            panelCanvas.AddComponent<CanvasScaler>();
            panelCanvas.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
        GameObject eventSystem = GameObject.Find("EventSystem");
        if (eventSystem == null)
        {
            eventSystem = PrefabUtility.InstantiatePrefab(Resources.Load("EventSystem", typeof(GameObject))) as GameObject;
        }
        GameObject audioPanel = GameObject.Find("AudioPanel");
        if (audioPanel == null)
        { 
            audioPanel = PrefabUtility.InstantiatePrefab(Resources.Load("AudioPanel", typeof(GameObject)), panelCanvas.transform) as GameObject;
        }
        GameObject dialoguePanel = GameObject.Find("DialoguePanel");
        if (dialoguePanel == null)
        {
            dialoguePanel = PrefabUtility.InstantiatePrefab(Resources.Load("DialoguePanel", typeof(GameObject)), panelCanvas.transform) as GameObject;
        }
        GameObject choicePanel = GameObject.Find("ChoicePanel");
        if (choicePanel == null)
        {
            choicePanel = PrefabUtility.InstantiatePrefab(Resources.Load("ChoicePanel", typeof(GameObject)), panelCanvas.transform) as GameObject;
        }
        GameObject instructionalPanel = GameObject.Find("InstructionalPanel");
        if (instructionalPanel == null)
        {
            instructionalPanel = PrefabUtility.InstantiatePrefab(Resources.Load("InstructionalPanel", typeof(GameObject)), panelCanvas.transform) as GameObject;
        }
        GameObject timerPanel = GameObject.Find("TimerPanel");
        if (timerPanel == null)
        {
            timerPanel = PrefabUtility.InstantiatePrefab(Resources.Load("TimerPanel", typeof(GameObject)), panelCanvas.transform) as GameObject;
        }
        GameObject debugPanel = GameObject.Find("DebugPanel");
        if (debugPanel == null)
        {
            debugPanel = PrefabUtility.InstantiatePrefab(Resources.Load("DebugPanel", typeof(GameObject)), panelCanvas.transform) as GameObject;
        }
        GameObject menuPanel = GameObject.Find("MenuPanel");
        if (menuPanel == null)
        {
            menuPanel = PrefabUtility.InstantiatePrefab(Resources.Load("MenuPanel", typeof(GameObject)), panelCanvas.transform) as GameObject;
        }
        bool CheckForAsset(string asset)
        {
            
            bool exists = false;
            var results = AssetDatabase.FindAssets("t:object", new[] { "Assets/Sinthetik/Mission Control/Resources/" });
            foreach (string guid in results)
            {
                if (AssetDatabase.GUIDToAssetPath(guid) == asset)
                {
                    exists = true;
                }
            }
            Debug.Log("Exists: " + exists);
            return exists;
        }
    }
    
}
