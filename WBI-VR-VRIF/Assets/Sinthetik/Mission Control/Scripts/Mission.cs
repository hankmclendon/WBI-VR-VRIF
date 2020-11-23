using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace Sinthetik.MissionControl
{
    public class Mission : MonoBehaviour
    {
        #region Variables

        [FoldoutGroup("Panels")]
        [BoxGroup("Panels/Box", false)]
        public GameObject menuPanel;

        [BoxGroup("Panels/Box", false)]
        public GameObject dialoguePanel;

        [BoxGroup("Panels/Box", false)]
        public GameObject audioPanel;

        [BoxGroup("Panels/Box", false)]
        public GameObject timerPanel;

        public List<Section> sections = new List<Section>();

        public List<Module> endOfGameList = new List<Module>();

        public List<Module> customChoiceOneList = new List<Module>();

        public List<Module> customChoiceTwoList = new List<Module>();

        // components
        private AudioPanel audioSystem;
        private MenuPanel menu;
        private DialoguePanel dialogue;
        private TimerPanel timer;

        // current state tracking
        private Section currentSection;
        private Task currentTask;
        private Module currentModule;
        private List<Module> currentModuleList;
        private List<Task> currentTaskList;
        private List<Section> currentSectionList;
        int sectionCount = 0;
        int taskCount = 0;
        int moduleCount = 0;
        private enum ModuleSubList { modules, success, fail };
        private ModuleSubList moduleSubList = ModuleSubList.modules;

        #endregion

        #region Listeners
        private void OnEnable()
        {
            DialoguePanel.dialogueClose += DialogueComplete;
            DialoguePanel.choiceClose += ChoiceComplete;
            Activity.activityComplete += ActivityComplete;
            Trigger.triggerHit += TriggerHit;
            MenuPanel.itemSelected += SetTask;
            TimerPanel.timerComplete += TimerComplete;
            AudioPanel.audioComplete += AudioComplete;
            MenuPanel.menuComplete += MenuComplete;
        }

        private void OnDisable()
        {
            DialoguePanel.dialogueClose -= DialogueComplete;
            DialoguePanel.choiceClose -= ChoiceComplete;
            Activity.activityComplete -= ActivityComplete;
            Trigger.triggerHit -= TriggerHit;
            MenuPanel.itemSelected -= SetTask;
            TimerPanel.timerComplete -= TimerComplete;
            AudioPanel.audioComplete -= AudioComplete;
            MenuPanel.menuComplete -= MenuComplete;
        }

        #endregion

        #region Start
        void Start()
        {
            // setup component references for convenience
            menu = menuPanel.GetComponent<MenuPanel>();
            dialogue = dialoguePanel.GetComponent<DialoguePanel>();
            timer = timerPanel.GetComponent<TimerPanel>();
            audioSystem = audioPanel.GetComponent<AudioPanel>();

            // deactivate panels
            dialogue.HideDisplayPanel();
            menu.HideDisplayPanel();

            timerPanel.SetActive(true);
            timer.HideDisplayPanel();

            audioPanel.SetActive(true);
            audioSystem.HideDisplayPanel();

            // create runtime variables for convenience
            InitializeVariables();

            // start
            CheckForMenu();
            //ActivateModule();
        }

        #endregion

        #region Update Variables
        private void InitializeVariables()
        {
            sectionCount = 0;
            taskCount = 0;
            moduleCount = 0;

            currentSectionList = sections;
            currentTaskList = sections[sectionCount].tasks;
            currentModuleList = currentTaskList[taskCount].modules;
            
            currentSection = currentSectionList[sectionCount];
            currentTask = currentTaskList[taskCount];
            currentModule = currentModuleList[moduleCount];
        }

        #endregion

        #region Menu System
        private void CheckForMenu()
        {
            // determine whether to start new section with menu or linear sequence
            if (currentSection.hasMenu)
            {
                Debug.Log("CheckForMenu Called. CurrentSection.hasMenu");
                menu.OpenPanel(currentTaskList, false); // false flag let's the menu know it hasn't been completed (currently a hack)
            }
            else
            {
                ActivateModule();
            }
        }

        // the "Activate Mission" button on the menu will use this method to set the desired task
        private void SetTask(Task task)
        {
            for (int i = 0; i < currentTaskList.Count; i++)
            {
                if (currentTaskList[i] == task)
                {
                    taskCount = i;
                    moduleCount = 0;
                    currentTask = currentTaskList[taskCount];
                    currentModuleList = currentTask.modules;
                    currentModule = currentModuleList[moduleCount];
                    //UpdateVariables();
                    ActivateModule();
                }
            }
        }
        #endregion

        #region Activation Methods
        // this is the primary mode of progressing through the game
        // if a Section !hasMenu, then ActivateModule always moves us through to the next SubSection
        private void ActivateModule()
        {
            // set mission name for debug panel
            //debug.UpdateMission(missionName);
            // update the heirarchy for the debug panel display
            //debug.DrawList(currentSectionList, currentSection, currentSubSection, currentModule);

            // Dialogue
            if (currentModule.moduleType == Module.ModuleType.dialogue)
            {
                dialogue.OpenPanel(currentModule, false);
                currentModule.CallEntryEvent();
            }
            // Audio
            else if (currentModule.moduleType == Module.ModuleType.audio)
            {
                audioSystem.PlayAudio(currentModule.audioClip);
                currentModule.CallEntryEvent();
            }
            // Trigger
            else if (currentModule.moduleType == Module.ModuleType.trigger)
            {
                Trigger currentTrigger = currentModule.trigger.GetComponent<Trigger>();
                currentTrigger.Activate();
                currentModule.CallEntryEvent();
            }
            // Activity
            else if (currentModule.moduleType == Module.ModuleType.activity)
            {
                Activity currentActivity = currentModule.activity.GetComponent<Activity>();
                currentActivity.Activate();
                currentModule.CallEntryEvent();
            }
            // Timer
            else if (currentModule.moduleType == Module.ModuleType.timer)
            {
                timer.StartTimer(currentModule.timer);
                currentModule.CallEntryEvent();
                NextModule();
            }
            // Choice
            else if (currentModule.moduleType == Module.ModuleType.choice)
            {
                dialogue.OpenPanel(currentModule, true);
            }
            // Choice
            else if (currentModule.moduleType == Module.ModuleType.menu)
            {
                menu.OpenPanel(currentTaskList, true); // true flag let's the menu know it has been completed (currently a hack)
            }
            // Instructional
            //else if (currentModule.moduleType == Module.ModuleType.instructional)
            //{
            //    if (currentModule.moduleData != null)
            //        instructional.OpenPanel(currentModule.missionActivity, currentModule.moduleData);
            //    else
            //        instructional.OpenPanel(currentModule.missionActivity);
            //}

        }
        
        #endregion

        #region Completion Methods
        // these primarily exist to do any needed processing before moving to the next module
        // for the most part processing the objects has moved to the objects themselves (mainly deactivating them)
        private void DialogueComplete()
        {
            currentModule.CallExitEvent();
            NextModule();
        }
        private void AudioComplete(AudioClip audioClip)
        {
            if (currentModule.moduleType == Module.ModuleType.audio)
            {
                currentModule.CallExitEvent();
                NextModule();
            }
        }
        private void ActivityComplete(GameObject _activity)
        {
            // the Instructional module also looks for activity complete so make sure this is an activity module
            if (currentModule.moduleType == Module.ModuleType.activity)
            {
                Debug.Log("Activity Complete (From Mission)");
                currentModule.CallExitEvent();
                NextModule();
            }
        }
        //private void InstructionalComplete()
        //{
        //    Debug.Log("Instructional Complete: Module = " + currentModule.moduleName);
        //    NextModule();
        //}
        private void TriggerHit(GameObject _trigger)
        {
            currentModule.CallExitEvent();
            NextModule();
        }
        private void ChoiceComplete(bool choiceOne)
        {
            if (choiceOne)
            {
                if (currentModule.choiceOne == Module.Choice.NextModule)
                    NextModule();
                else if (currentModule.choiceOne == Module.Choice.NextTask)
                    NextTask();
                else if (currentModule.choiceOne == Module.Choice.NextSection)
                    NextSection();
                else if (currentModule.choiceOne == Module.Choice.End)
                    End();
                else if (currentModule.choiceOne == Module.Choice.Custom)
                    CustomChoiceOne();
            }
            else
            {
                if (currentModule.choiceTwo == Module.Choice.NextModule)
                    NextModule();
                else if (currentModule.choiceTwo == Module.Choice.NextTask)
                    NextTask();
                else if (currentModule.choiceTwo == Module.Choice.NextSection)
                    NextSection();
                else if (currentModule.choiceTwo == Module.Choice.End)
                    End();
                else if (currentModule.choiceTwo == Module.Choice.Custom)
                    CustomChoiceTwo();
            }
        }
        private void TimerComplete()
        {
            currentModuleList = currentTask.failList;
            moduleCount = 0;
            currentModule = currentModuleList[moduleCount];
            currentTask.isComplete = true;
            currentTask.deactivated = true;
            ActivateModule();
        }

        private void MenuComplete()
        {
            Debug.Log("Menu Complete");
            NextModule();
        }

        #endregion

        #region Next Methods
        private void NextModule()
        {
            Debug.Log("NextModule Called. Section = " + currentSection.sectionName + ". Task = " + currentTask.taskName + ". Module = " + currentModule.moduleName);
            // End of List: End of Game
            if (currentModuleList == endOfGameList && moduleCount == currentModuleList.Count - 1)
            {
                Debug.Log("List = endOfGameList");
                End();
            }
            // End of List: Custom Choice One
            else if (currentModuleList == customChoiceOneList && moduleCount == currentModuleList.Count - 1)
            {
                // change this to move to a different part of the list
                Debug.Log("List = customChoiceOneList");
                End();
            }
            // End of List: Custom Choice Two
            else if (currentModuleList == customChoiceTwoList && moduleCount == currentModuleList.Count - 1)
            {
                // change this to move to a different part of the list
                Debug.Log("List = customChoiceTwoList");
                End();
            }
            // End of List: Section Success
            else if (currentModuleList == currentSection.successList && moduleCount == currentModuleList.Count - 1)
            {
                Debug.Log("List = Section.successList");
                currentTask.isComplete = true;
                NextSection();
            }
            // End of List: Section Fail
            else if (currentModuleList == currentSection.failList && moduleCount == currentModuleList.Count - 1)
            {
                Debug.Log("List = Section.failList");
                NextSection();
            }
            // End of List: Task Success
            else if (currentModuleList == currentTask.successList && moduleCount == currentModuleList.Count - 1)
            {
                Debug.Log("List = Task.successList");
                currentTask.isComplete = true;
                NextTask();
            }
            // End of List: Task Fail
            else if (currentModuleList == currentTask.failList && moduleCount == currentModuleList.Count - 1)
            {
                Debug.Log("List = Task.failList");
                NextTask();
            }
            // Any module list that isn't yet completed
            else if (moduleCount < currentModuleList.Count - 1)
            {
                Debug.Log("List = Any List, Still Incrementing");
                moduleCount += 1;
                currentModule = currentModuleList[moduleCount];
                ActivateModule();
            }
            // if we are at the end of the module list, and there are success modules, move to the tasks success list
            else if(currentModuleList == currentTask.modules && moduleCount == currentModuleList.Count-1)
            {
                Debug.Log("End of Module List. Check for Success.");
                if (currentTask.successList.Count != 0)
                {
                    Debug.Log("Succcess List isn't empty so move to success list");
                    if (timer.isRunning)
                        timer.StopTimer();
                    currentModuleList = currentTask.successList;
                    moduleCount = 0;
                    currentModule = currentModuleList[moduleCount];
                    ActivateModule();
                }
                else
                {
                    Debug.Log("Move to Next Task");
                    NextTask();
                }
            }
        }
        private void NextTask()
        {
            Debug.Log("Next Task Called");
            // we must 0 out all child lists below this one
            moduleCount = 0;

            // here we check to see if the current section hasMenu, if so follow menu rules.
            if (currentSection.hasMenu)
            {
                // if any SubSections are incomplete then show the menu
                // SubSections are marked complete by either finishing all the modules, or timing out
                if (currentTaskList.Any(c => c.isComplete == false))
                {
                    menu.OpenPanel(currentTaskList, false);
                }
                // if all Tasks are complete (first gate above) but any have been deactivated, then we failed the task
                else if (currentTaskList.Any(c => c.deactivated == true))
                {
                    currentModuleList = currentSection.failList;
                    currentModule = currentModuleList[moduleCount];
                    ActivateModule();
                }
                // if all sections are complete and none have been deactivated, and hasSuccess is true, then show success module
                else if (currentSection.successList.Count != 0)
                {
                    currentModuleList = currentSection.successList;
                    currentModule = currentModuleList[moduleCount];
                    ActivateModule();
                }
                // else move on to the next section
                else
                {
                    NextSection();
                }
            }
            // if Section !hasMenu, follow linear rules
            else if (taskCount < currentTaskList.Count - 1)
            {
                taskCount += 1;
                // reset module list to new task
                currentModuleList = currentTaskList[taskCount].modules;

                currentTask = currentTaskList[taskCount];
                currentModule = currentModuleList[moduleCount];

                ActivateModule();
            }
            else if (currentSection.successList.Count != 0)
            {
                // reset module list to current task success list
                currentModuleList = currentSection.successList;
                currentModule = currentModuleList[moduleCount];
                ActivateModule();
            }
            else
            {
                NextSection();
            }
        }
        private void NextSection()
        {
            Debug.Log("Next Section Called");
            // we must 0 out all child lists below this one
            moduleCount = 0;
            taskCount = 0;

            if (sectionCount < currentSectionList.Count - 1)
            {
                sectionCount += 1;
                // set new section
                currentTaskList = sections[sectionCount].tasks;
                currentModuleList = currentTaskList[taskCount].modules;

                currentSection = currentSectionList[sectionCount];
                currentTask = currentTaskList[taskCount];
                currentModule = currentModuleList[moduleCount];

                CheckForMenu();
            }
            else
            {
                sectionCount = 0;
                EndSequence();
            }
        }

        #endregion

        #region End Of Game
        private void EndSequence()
        {
            currentModuleList = endOfGameList;
            moduleCount = 0;
            currentModule = endOfGameList[moduleCount];
            ActivateModule();
        }

        private void End()
        {
            Debug.Log("Party's Over");
        }

        #endregion

        #region Custom Choice

        private void CustomChoiceOne()
        {
            currentModuleList = customChoiceOneList;
            moduleCount = 0;
            currentModule = customChoiceOneList[moduleCount];
            ActivateModule();
        }
        private void CustomChoiceTwo()
        {
            currentModuleList = customChoiceTwoList;
            moduleCount = 0;
            currentModule = customChoiceTwoList[moduleCount];
            ActivateModule();
        }

        #endregion

        #region Skip Forward

        void Update()
        {
            {
                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    SkipModule();
                }
            }
        }

        public void SkipModule()
        {
            if (currentModule.moduleType == Module.ModuleType.dialogue)
            {
                dialogue.Skip();
            }
            else if (currentModule.moduleType == Module.ModuleType.audio)
            {
                audioSystem.Skip();
            }
            else if (currentModule.moduleType == Module.ModuleType.trigger)
            {
                currentModule.trigger.GetComponent<Trigger>().Skip();
            }
            else if (currentModule.moduleType == Module.ModuleType.activity)
            {
                currentModule.activity.GetComponent<Activity>().Skip();
            }
            //else if (currentModule.moduleType == Module.ModuleType.instructional)
            //{
            //    instructional.Skip();
            //}
            else if (currentModule.moduleType == Module.ModuleType.choice)
            {
                dialogue.Skip();
            }
            else if (currentModule.moduleType == Module.ModuleType.menu)
            {
                menu.Skip();
            }
        }

        #endregion
    }
}