using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace Sinthetik.MissionControl
{
    public class DebugPanel : MonoBehaviour
    {
        public Text missionInfo;
        public GameObject listContainer;
        
        public Button sectionButtonPrefab;
        public Button subSectionButtonPrefab;
        public Button moduleButtonPrefab;

        public Color sectionSelectedColor;
        public Color sectionColor;
        public Color subSectionSelectedColor;
        public Color subSectionColor;
        public Color moduleSelectedColor;
        public Color moduleColor;
        public Color successSelectedColor;
        public Color successColor;
        public Color failSelectedColor;
        public Color failColor;

        public Sprite normalSprite;
        public Sprite outlineSprite;
        

        private List<DebugButton> buttonList = new List<DebugButton>();

        public void UpdateMission(string missionName)
        {
            missionInfo.text = missionName;
        }

        public void DrawList(List<Section> sectionList, Section currentSection, SubSection currentSubSection, Module currentModule)
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in listContainer.transform)
            {
                children.Add(child.gameObject);
            } 
            children.ForEach(child => Destroy(child));

            for(int i = 0; i < sectionList.Count; i++)
            {
                Section section = sectionList[i];
                Button sectionButton = Instantiate(sectionButtonPrefab, listContainer.transform);
                Text sectionButtonText = sectionButton.transform.GetChild(0).GetComponent<Text>();
                sectionButtonText.text = section.name;
                sectionButton.GetComponent<Image>().sprite = outlineSprite;
                sectionButton.GetComponent<Image>().color = section == currentSection ? sectionSelectedColor : sectionColor;
                buttonList.Add(new DebugButton(sectionButton));

                List<SubSection> subSectionList = section.subList;
                for(int j = 0; j < subSectionList.Count; j++)
                {
                    SubSection subSection = subSectionList[j];
                    Button subSectionButton = Instantiate(subSectionButtonPrefab, listContainer.transform);
                    Text subSectionButtonText = subSectionButton.transform.GetChild(0).GetComponent<Text>();
                    subSectionButtonText.text = subSection.name;
                    subSectionButton.GetComponent<Image>().sprite = outlineSprite;
                    subSectionButton.GetComponent<Image>().color = subSection == currentSubSection ? subSectionSelectedColor : subSectionColor;
                    buttonList[i].subList.Add(new DebugButton(subSectionButton));
                    List<Module> moduleList = subSection.subList;
                    for(int k = 0; k < moduleList.Count; k++)
                    {
                        Module module = moduleList[k];
                        Button moduleButton = Instantiate(moduleButtonPrefab, listContainer.transform);
                        Text moduleButtonText = moduleButton.transform.GetChild(0).GetComponent<Text>();
                        moduleButtonText.text = module.name;
                        moduleButton.GetComponent<Image>().sprite = normalSprite;
                        moduleButton.GetComponent<Image>().color = module == currentModule ? moduleSelectedColor : moduleColor;
                        buttonList[i].subList[j].subList.Add(new DebugButton(moduleButton));
                    }
                    if(subSection.hasSuccess)
                    {
                        Module subSectionSuccess = subSection.success;
                        Button subSectionSuccessButton = Instantiate(subSectionButtonPrefab, listContainer.transform);
                        subSectionSuccessButton.transform.Find("successImage").gameObject.SetActive(true);
                        Text subSectionSuccessText = subSectionSuccessButton.transform.GetChild(0).GetComponent<Text>();
                        subSectionSuccessText.text = subSectionSuccess.name;
                        subSectionSuccessButton.GetComponent<Image>().sprite = outlineSprite;
                        subSectionSuccessButton.GetComponent<Image>().color = subSectionSuccess == currentModule ? successSelectedColor : successColor;
                        buttonList.Add(new DebugButton(subSectionSuccessButton));
                    }
                    if(subSection.hasFail)
                    {
                        Module subSectionFail = subSection.fail;
                        Button subSectionFailButton = Instantiate(subSectionButtonPrefab, listContainer.transform);
                        subSectionFailButton.transform.Find("failImage").gameObject.SetActive(true);
                        Text subSectionFailButtonText = subSectionFailButton.transform.GetChild(0).GetComponent<Text>();
                        subSectionFailButtonText.text = subSectionFail.name;
                        subSectionFailButton.GetComponent<Image>().sprite = outlineSprite;
                        subSectionFailButton.GetComponent<Image>().color = subSectionFail == currentModule ? failSelectedColor : failColor;
                        buttonList.Add(new DebugButton(subSectionFailButton));
                    }
                }
                if(section.hasSuccess)
                {
                    Module sectionSuccess = section.success;
                    Button sectionSuccessButton = Instantiate(sectionButtonPrefab, listContainer.transform);
                    sectionSuccessButton.transform.Find("successImage").gameObject.SetActive(true);
                    Text sectionSuccessButtonText = sectionSuccessButton.transform.GetChild(0).GetComponent<Text>();
                    sectionSuccessButtonText.text = sectionSuccess.name;
                    sectionSuccessButton.GetComponent<Image>().sprite = outlineSprite;
                    sectionSuccessButton.GetComponent<Image>().color = sectionSuccess == currentModule ? successSelectedColor : successColor;
                    buttonList.Add(new DebugButton(sectionSuccessButton));
                }
                if(section.hasFail)
                {
                    Module sectionFail = section.fail;
                    Button sectionFailButton = Instantiate(sectionButtonPrefab, listContainer.transform);
                    sectionFailButton.transform.Find("failImage").gameObject.SetActive(true);
                    Text sectionFailButtonText = sectionFailButton.transform.GetChild(0).GetComponent<Text>();
                    sectionFailButtonText.text = sectionFail.name;
                    sectionFailButton.GetComponent<Image>().sprite = outlineSprite;
                    sectionFailButton.GetComponent<Image>().color = sectionFail == currentModule ? failSelectedColor : failColor;
                    buttonList.Add(new DebugButton(sectionFailButton));
                }
            }
        }


        public void UpdateSubSectionOutcomeButton(int sectionIndex, int subIndex, int moduleIndex, bool success)
        {
            if(success)
            {
                buttonList[sectionIndex].button.Select();
                buttonList[sectionIndex].subList[subIndex].button.Select();
                buttonList[sectionIndex].subList[subIndex].subList[moduleIndex+1].button.Select();
            }
        }

        public IEnumerator Fade(Image image) 
        {
            for (float i = .5f; i >= 0; i -= 0.1f) 
            {
                Color c = new Color(0f, 1f, 0f, .5f);
                c.a = i;
                image.color = c;
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    public class DebugButton
    {
        public List<DebugButton> subList = new List<DebugButton>();
        public Button button;
        public DebugButton(Button button)
        {
            this.button = button;
        }
    }
    
}