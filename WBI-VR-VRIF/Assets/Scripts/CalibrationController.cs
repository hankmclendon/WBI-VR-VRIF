using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CalibrationController : MonoBehaviour
{
    public CalibrateDial redDial;
    public CalibrateDial greenDial;
    public CalibrateDial blueDial;
    public AudioSource audio;
    public float holdTimeInSeconds = 2;
    private float currentTimeInSeconds = 0;
    public Renderer screenMaterial;
    public Material redScreen;
    public Material blueScreen;
    public Material greenScreen;

    public UnityEvent taskCompleted = new UnityEvent();

    void Update()
    {
        if (redDial.calibrated == true && greenDial.calibrated == true && blueDial.calibrated == true)
        {
            currentTimeInSeconds += Time.deltaTime;
            if (currentTimeInSeconds >= holdTimeInSeconds)
            {
                currentTimeInSeconds = 0;
                taskCompleted?.Invoke();
                audio.Play();
            }
        }
    }
    public void ChangeToRedScreen()
    {
        screenMaterial.material = redScreen;
    }
    public void ChangeToGreenScreen()
    {
        screenMaterial.material = greenScreen;
    }
    public void ChangeToBlueScreen()
    {
        screenMaterial.material = blueScreen;
    }
}
