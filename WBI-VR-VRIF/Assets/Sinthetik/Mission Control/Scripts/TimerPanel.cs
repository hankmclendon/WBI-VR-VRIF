using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

namespace Sinthetik.MissionControl
{
    public class TimerPanel : MonoBehaviour
    {
        private Text timeText;
        private float timeRemaining;
        private bool isRunning = false;

        public static UnityAction timerComplete;

        void Awake()
        {
            timeText = gameObject.transform.Find("Text").GetComponent<Text>();
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
                }
            }
        }
        void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            
            timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }
}
