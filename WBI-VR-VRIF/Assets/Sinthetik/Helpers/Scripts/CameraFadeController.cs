using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sinthetik.Helpers
{
    [RequireComponent(typeof(Animator))]
    public class CameraFadeController : MonoBehaviour
    {
        public float initialWaitTime;
        private Animator anim;
        bool faded = false;

        public UnityEvent fadeComplete = new UnityEvent();

        void Start()
        {
            anim = GetComponent<Animator>();
            StartCoroutine(DelayCoroutine());
        }

        public void FadeCameraIn()
        {
            anim.SetBool("FadeIn", true);
            faded = true;
        }

        public void FadeCameraOut()
        {
            anim.SetBool("FadeIn", false);
            faded = false;
        }

        public void FadeComplete()
        {
            fadeComplete?.Invoke();
        }

        IEnumerator DelayCoroutine()
        {
            yield return new WaitForSeconds(initialWaitTime);
            FadeCameraIn();
        }

        void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                if (faded)
                    FadeCameraOut();
                else
                    FadeCameraIn();
            }
        }

    }
}
