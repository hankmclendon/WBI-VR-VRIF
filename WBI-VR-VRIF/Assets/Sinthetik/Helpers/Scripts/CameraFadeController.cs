using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Sinthetik.Helpers
{
    
    public class CameraFadeController : MonoBehaviour
    {
        public MeshRenderer renderer;
        public float fadeSpeed;

        public UnityEvent fadeCompleted = new UnityEvent();
        public IEnumerator FadeInCameraCoroutine()
        {
            float a = renderer.material.GetFloat("_alpha");
            while (a > 0)
            {
                a -= (fadeSpeed * Time.deltaTime);
                renderer.material.SetFloat("_alpha", a);
                yield return null;
            }
            fadeCompleted?.Invoke();
        }

        public IEnumerator FadeOutCameraCoroutine()
        {
            float a = renderer.material.GetFloat("_alpha");
            while (a < 1)
            {
                a += (fadeSpeed * Time.deltaTime);
                renderer.material.SetFloat("_alpha", a);
                yield return null;
            }
            fadeCompleted?.Invoke();
        }

        public void FadeOutCamera()
        {
            StartCoroutine(FadeOutCameraCoroutine());
            
        }

        public void FadeInCamera()
        {
            //Debug.Log("FadeInCamera called");
            StartCoroutine(FadeInCameraCoroutine());
            
        }

        // void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        FadeInCamera();
        //    }
        //}

    }
}
