using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;
using UnityEditor;
using BNG;

namespace Sinthetik.MissionControl
{

    public class Mission : MonoBehaviour
    {

        #region Variables

        // mission specific data
        public List<Section> subList = new List<Section>();
        public string missionName;

        // mission specific modules
        public Module endOfGame;

        // gameobjects
        private MissionControl missionControl;
        private GameObject menuPanel;
        private GameObject debugPanel;
        private GameObject dialoguePanel;
        private GameObject timerPanel;
        private GameObject audioPanel;
        private GameObject choicePanel;
        private GameObject instructionalPanel;

        // components
        private AudioPanel audioSystem;
        private MenuPanel menu;
        private DebugPanel debug;
        private DialoguePanel dialogue;
        private TimerPanel timer;
        private ChoicePanel choice;
        private InstructionalPanel instructional;

        // current state tracking
        public Section currentSection;
        public SubSection currentSubSection;
        public Module currentModule;
        public List<Module> currentModuleList;
        public List<SubSection> currentSubSectionList;
        public List<Section> currentSectionList;
        int sectionCount = 0;
        int subSectionCount = 0;
        int moduleCount = 0;

        // player controller
        private BNGPlayerController playerController;
        private LocomotionManager locomotionManager;

        #endregion

        #region Listeners
        private void OnEnable()
        {
            DialoguePanel.dialogueClose += DialogueComplete;
            ChoicePanel.choiceClose += ChoiceComplete;
            MissionActivity.activityComplete += ActivityComplete;
            Trigger.triggerHit += DestinationReached;
            MenuPanel.itemSelected += SetSubSection;
            TimerPanel.timerComplete += ActivateFail;
            InstructionalPanel.instructionalClose += InstructionalComplete;
            AudioPanel.audioComplete += AudioComplete;
        }

        private void OnDisable()
        {
            DialoguePanel.dialogueClose -= DialogueComplete;
            ChoicePanel.choiceClose -= ChoiceComplete;
            MissionActivity.activityComplete -= ActivityComplete;
            Trigger.triggerHit -= DestinationReached;
            MenuPanel.itemSelected -= SetSubSection;
            TimerPanel.timerComplete -= ActivateFail;
            InstructionalPanel.instructionalClose -= InstructionalComplete;
            AudioPanel.audioComplete -= AudioComplete;
        }

        #endregion

        #region Start
        void Start()
        {
            missionControl = (MissionControl)FindObjectOfType(typeof(MissionControl));
            if (missionControl)
                Debug.Log("Mission Control found: " + missionControl.name);
            else
                Debug.Log("A Mission Control not found!");

            // setup panel references for convenience
            menuPanel = GameObject.Find("MenuPanel");
            dialoguePanel = GameObject.Find("DialoguePanel");
            timerPanel = GameObject.Find("TimerPanel");
            debugPanel = GameObject.Find("DebugPanel");
            audioPanel = GameObject.Find("AudioPanel");
            choicePanel = GameObject.Find("ChoicePanel");
            instructionalPanel = GameObject.Find("InstructionalPanel");

            // setup component references for convenience
            menu = menuPanel.GetComponent<MenuPanel>();
            debug = debugPanel.GetComponent<DebugPanel>();
            dialogue = dialoguePanel.GetComponent<DialoguePanel>();
            timer = timerPanel.GetComponent<TimerPanel>();
            audioSystem = audioPanel.GetComponent<AudioPanel>();
            choice = choicePanel.GetComponent<ChoicePanel>();
            instructional = instructionalPanel.GetComponent<InstructionalPanel>();
            //playerController = GameObject.Find("PlayerController").GetComponent<BNGPlayerController>();
            //locomotionManager = GameObject.Find("PlayerController").GetComponent<LocomotionManager>();

            // deactivate panels
            dialoguePanel.SetActive(false);
            menuPanel.SetActive(false);
            timerPanel.SetActive(false);
            choicePanel.SetActive(false);
            instructionalPanel.SetActive(false);
            audioPanel.SetActive(false);

            // create runtime variables for convenience
            UpdateVariables();

            // start
            CheckForMenu();
        }

        #endregion

        #region Update Variables
        private void UpdateVariables()
        {
            currentSectionList = subList;
            currentSubSectionList = subList[sectionCount].subList;
            currentModuleList = subList[sectionCount].subList[subSectionCount].subList;

            currentSection = currentSectionList[sectionCount];
            currentSubSection = currentSubSectionList[subSectionCount];
            currentModule = currentModuleList[moduleCount];
        }

        #endregion

        #region Check For Menu
        private void CheckForMenu()
        {
            // determine whether to start with menu or linear sequence
            if (currentSection.hasMenu)
            {
                ActivateMenu();
            }
            else
            {
                ActivateModule();
            }
        }

        #endregion

        #region Activation Methods
        // this is the primary mode of progressing through the game
        // if a Section !hasMenu, then ActivateModule always moves us through to the next SubSection
        private void ActivateModule()
        {
            // set mission name for debug panel
            if(debug.showPanel)
                debug.UpdateMission(missionName);
            // update the heirarchy for the debug panel display
            if (debug.showPanel)
                debug.DrawList(currentSectionList, currentSection, currentSubSection, currentModule);

            // Dialogue
            if (currentModule.moduleType == Module.ModuleType.Dialogue)
            {
                if (currentModule.moduleData != null)
                    dialogue.OpenPanel(currentModule.moduleData);
                else
                    dialogue.OpenPanel();
            }
            // Audio
            else if (currentModule.moduleType == Module.ModuleType.Audio)
            {
                audioSystem.PlayAudio(currentModule.audioClip);
            }
            // Trigger
            else if (currentModule.moduleType == Module.ModuleType.Trigger)
            {
                Trigger currentTrigger = currentModule.destinationTrigger.GetComponent<Trigger>();
                currentTrigger.Activate();
            }
            // Activity
            else if (currentModule.moduleType == Module.ModuleType.Activity)
            {
                currentModule.missionActivity.GetComponent<MissionActivity>().Activate();
            }
            // Timer
            else if (currentModule.moduleType == Module.ModuleType.Timer)
            {
                timer.StartTimer(currentModule.timeout);
                NextModule();
            }
            // Choice
            else if (currentModule.moduleType == Module.ModuleType.Choice)
            {
                if (currentModule.moduleData != null)
                    choice.OpenPanel(currentModule.moduleData);
                else
                    choice.OpenPanel();
            }
            // Instructional
            else if (currentModule.moduleType == Module.ModuleType.Instructional)
            {
                if (currentModule.moduleData != null)
                    instructional.OpenPanel(currentModule.missionActivity, currentModule.moduleData);
                else
                    instructional.OpenPanel(currentModule.missionActivity);
            }
        }
        // however, if a Section hasMenu then we open a menu between each SubSection
        private void ActivateMenu()
        {
            menuPanel.SetActive(true);
            menu.BuildMenu(currentSubSectionList);
            //locomotionManager.enabled = false;
        }
        // then that menu will use this method to set the desired SubSection
        private void SetSubSection(SubSection subSection)
        {
            for (int i = 0; i < currentSubSectionList.Count; i++)
            {
                if (currentSubSectionList[i] == subSection)
                {
                    subSectionCount = i;
                    moduleCount = 0;
                    UpdateVariables();
                    ActivateModule();
                }
            }

        }
        private void ActivateFail()
        {
            currentModule = currentSubSection.fail;
            currentSubSection.isComplete = true;
            currentSubSection.deactivated = true;
            ActivateModule();
        }
        private void ActivateSubSectionSuccess()
        {
            timer.StopTimer();
            currentModule = currentSubSection.success;
            currentSubSection.isComplete = true;
            ActivateModule();
        }
        private void ActivateSectionSuccess()
        {
            currentModule = currentSection.success;
            currentSection.isComplete = true;
            ActivateModule();
        }
        #endregion

        #region Completion Methods
        // these primarily exist to do any needed processing before moving to the next module
        // for the most part processing the objects has moved to the objects themselves (mainly deactivating them)
        private void DialogueComplete()
        {
            Debug.Log("Dialogue Complete: Module = " + currentModule.name);
            NextModule();
        }
        private void AudioComplete(AudioClip audioClip)
        {
            if(currentModule.moduleType == Module.ModuleType.Audio)
            {
                Debug.Log("Audio Complete: Module = " + currentModule.name);
                NextModule();
            }
        }
        private void ActivityComplete(GameObject _activity)
        {
            // the Instructional module also looks for activity complete so make sure this is an activity module
            if(currentModule.moduleType == Module.ModuleType.Activity)
            {
                Debug.Log("Activity Complete: Module = " + currentModule.name);
                NextModule();
            }
        }
        private void InstructionalComplete()
        {
            Debug.Log("Instructional Complete: Module = " + currentModule.name);
            NextModule();
        }
        private void DestinationReached(GameObject _trigger)
        {
            Debug.Log("Destination Complete: Module = " + currentModule.name);
            // we recieve the trigger gameobject just in case we need to do anything to it
            NextModule();
        }
        private void ChoiceComplete(bool choice)
        {
            Debug.Log("Choice Complete: Module = " + currentModule.name);
            // this works for one choice box only. in the future the outcomes of the choice needs to move to the data scriptable object (I think)
            if (choice)
                NextModule();
            else
                End();
        }

        #endregion

        #region Next Methods
        private void NextModule()
        {
            // if we're in a Section success or fail module then we need to advance the Section
            if (currentModule == currentSection.fail || currentModule == currentSection.success)
            {
                NextSection();
            }
            // if we're in a SubSection success or fail module then we need to advance the SubSection
            else if (currentModule == currentSubSection.fail || currentModule == currentSubSection.success)
            {
                NextSubSection();
            }
            // otherwise advance the module if we're not at the end of the module list OR if the module has not been deactivated (due to fail)
            else if (moduleCount < currentModuleList.Count - 1 && currentSubSection.deactivated != true)
            {
                moduleCount += 1;
                UpdateVariables();
                ActivateModule();
            }
            // if we are at the end and the module hasSuccess, move to success module
            else if (currentSubSection.hasSuccess)
            {
                ActivateSubSectionSuccess();
            }
            // else move on to next SubSection
            else
            {
                NextSubSection();
            }
        }
        private void NextSubSection()
        {

            // we must 0 out all of the lists below this one
            moduleCount = 0;

            // here we check to see if the current section hasMenu, if so follow menu rules.
            if (currentSection.hasMenu)
            {
                // if any SubSections are incomplete then show the menu
                // SubSections are marked complete by either finishing all the modules, or timing out
                if (currentSubSectionList.Any(c => c.isComplete == false))
                {
                    ActivateMenu();
                }
                // if all SubSections are complete (first gate above) but any have been deactivated, then we failed the SubSection
                else if (currentSubSectionList.Any(c => c.deactivated == true))
                {
                    currentModule = currentSection.fail;
                    ActivateModule();
                }
                // if all sections are complete and none have been deactivated, and hasSuccess is true, then show success module
                else if (currentSection.hasSuccess)
                {
                    currentModule = currentSection.success;
                    ActivateModule();
                }
                // else move on to the next section
                else
                {
                    NextSection();
                }
            }
            // if Section !hasMenu, follow linear rules
            else if (subSectionCount < currentSubSectionList.Count - 1)
            {
                subSectionCount += 1;
                UpdateVariables();
                ActivateModule();
            }
            else if (currentSection.hasSuccess)
            {
                ActivateSectionSuccess();
            }
            else
            {
                NextSection();
            }
        }
        private void NextSection()
        {
            // we must 0 out all of the lists below this one
            moduleCount = 0;
            subSectionCount = 0;

            if (sectionCount < currentSectionList.Count - 1)
            {
                sectionCount += 1;
                UpdateVariables();
                CheckForMenu();
            }
            else
            {
                sectionCount = 0;
                End();
            }
        }

        #endregion

        #region End Of Game
        private void End()
        {
            currentModule = endOfGame;
            ActivateModule();
        }

        #endregion

        #region Skip Forward

        void Update()
        {
            {
                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    // PreviousModule();
                }
                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    SkipModule();
                }
            }
        }

        public void SkipModule()
        {
            if (currentModule.moduleType == Module.ModuleType.Dialogue)
            {
                dialogue.Skip();
            }
            else if (currentModule.moduleType == Module.ModuleType.Audio)
            {
                audioSystem.Skip();
            }
            else if (currentModule.moduleType == Module.ModuleType.Trigger)
            {
                currentModule.destinationTrigger.GetComponent<Trigger>().Skip();
            }
            else if (currentModule.moduleType == Module.ModuleType.Activity)
            {
                currentModule.missionActivity.GetComponent<MissionActivity>().Skip();
            }
            else if (currentModule.moduleType == Module.ModuleType.Instructional)
            {
                instructional.Skip();
            }
            else if (currentModule.moduleType == Module.ModuleType.Choice)
            {
                choice.Skip();
            }
        }

        #endregion

        #region Trigger Methods
        public void RemoveTrigger(GameObject trigger)
        {
            DestroyImmediate(trigger);
        }

        #endregion
    }
}



