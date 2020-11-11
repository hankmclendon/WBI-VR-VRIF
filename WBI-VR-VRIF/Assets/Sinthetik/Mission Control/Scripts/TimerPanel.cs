using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using TMPro;

namespace Sinthetik.MissionControl
{
    public class TimerPanel : MonoBehaviour
    {
        private TextMeshProUGUI timeText;
        private float timeRemaining;
        private bool isRunning = false;

        public static UnityAction timerComplete;

        void Awake()
        {
            timeText = gameObject.transform.Find("DisplayPanel").gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        }

        public void StartTimer(float timeout)
        {
            Debug.Log(timeout);
            timeRemaining = timeout;
            isRunning = true;
            gameObject.SetActive(true);
        }
        public void StopTimer()
        {
            isRunning = false;
            gameObject.SetActive(false);
        }

        void Update()
        {
            if (isRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    Debug.Log("Time has run out!");
                    timerComplete?.Invoke();
                    timeRemaining = 0;
                    isRunning = false;
                    gameObject.SetActive(false);
                }
            }
        }
        void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;

            TimeSpan t = TimeSpan.FromSeconds(timeToDisplay);

            //float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
            //float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            
            timeText.text = "00 " + string.Format("{0:00} {1:00}", t.Minutes, t.Seconds);
        }
    }
}
