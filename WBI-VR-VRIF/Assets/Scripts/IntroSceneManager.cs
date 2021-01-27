using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    public Sinthetik.Helpers.CameraFadeInController cameraFade;
    
    void Start()
    {
        cameraFade.FadeInCamera();    
    }
}
