using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Sinthetik.Helpers
{
    public class CameraFadeOutController : MonoBehaviour
    {
        public MeshRenderer renderer;
        public float fadeSpeed;

        public UnityEvent fadeCompleted = new UnityEvent();

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

    }
}
