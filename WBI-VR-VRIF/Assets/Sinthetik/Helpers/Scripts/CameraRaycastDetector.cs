using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sinthetik.Helpers
{
    public class CameraRaycastDetector : MonoBehaviour
    {
        public Camera camera;
        public Transform targetPoint;
        public Image tracker;
        public float holdGazeTimeInSeconds = 2;
        private float currentGazeTimeInSeconds = 0;

        public UnityEvent hitDetected = new UnityEvent();

        private void Start()
        {
            //camera.fieldOfView = 16f;
            tracker.gameObject.SetActive(false);
        }
        void Update()
        {
            Vector3 screenPoint = camera.WorldToViewportPoint(targetPoint.position);
            if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
            {
                tracker.gameObject.SetActive(true);
                //Debug.Log("Screenpoint = " + screenPoint);
                tracker.transform.localPosition = new Vector3(screenPoint.x*1000, screenPoint.y*-1000, .1f);
                currentGazeTimeInSeconds += Time.deltaTime;
                if (currentGazeTimeInSeconds >= holdGazeTimeInSeconds)
                {
                    currentGazeTimeInSeconds = 0;
                    hitDetected?.Invoke();

                }
            }
            else
            {
                currentGazeTimeInSeconds = 0;
                tracker.gameObject.SetActive(false);
            }
        }
    }
}
