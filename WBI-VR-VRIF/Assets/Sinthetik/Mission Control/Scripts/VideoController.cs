using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    public class VideoController : MonoBehaviour
    {
        private VideoPlayer videoPlayer;
        private VideoClip videoClip;
        public UnityEvent videoStartEvent;
        public UnityEvent videoStopEvent;

        void Start()
        {
            videoPlayer = GetComponent<VideoPlayer>();
            videoClip = videoPlayer.clip;
            videoPlayer.loopPointReached += EndReached;

            if (videoStartEvent == null)
                videoStartEvent = new UnityEvent();

            if (videoStopEvent == null)
                videoStopEvent = new UnityEvent();
        }

        public void PlayVideo()
        {
            videoStartEvent?.Invoke();
            videoPlayer.Play();
        }

        public void StopVideo()
        {
            if(videoPlayer.isPlaying)   
                videoPlayer.Stop();
        }

        private void EndReached(UnityEngine.Video.VideoPlayer vp)
        {
            if(vp == videoPlayer)
                videoStopEvent?.Invoke();
        }
    }
}
