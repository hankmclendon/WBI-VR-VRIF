using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Sinthetik.Helpers
{
    public class CameraRaycastDetector : MonoBehaviour
    {
        public Camera camera;
        public Transform targetPoint;
        public float holdGazeTimeInSeconds = 2;
        private float currentGazeTimeInSeconds = 0;

        public UnityEvent hitDetected = new UnityEvent();

        private void Start()
        {
            //camera.fieldOfView = 16f;
        }
        void Update()
        {
            Vector3 screenPoint = camera.WorldToViewportPoint(targetPoint.position);
            if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
            {
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
            }
        }
    }
}
