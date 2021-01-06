using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CalibrateDial : MonoBehaviour
{
    public Vector2 posValue;
    public Vector2 negValue;
    public bool calibrated = false;

    Renderer rend;
    AudioSource audio;
    Vector2 startingVector;
    Vector2 offsetVector;
    Vector2 finalVector;
    float startingOffset;

    //public UnityEvent onDialCalibrated;

    void Start()
    {
        rend = GetComponent<Renderer>();
        audio = GetComponent<AudioSource>();
        startingVector = rend.material.GetTextureOffset("_BaseMap");
        startingOffset = offsetVector.x;
        Debug.Log(gameObject.name + " Offset: " + startingVector);
    }
    public void UpdateDial(float dialValue)
    {
        dialValue = dialValue / 360;
        float roundedValue = Mathf.Round(dialValue * 100.0f) * 0.01f;
        offsetVector = new Vector2(roundedValue, 0);
        finalVector = startingVector + offsetVector;
        rend.material.SetTextureOffset("_BaseMap", finalVector);

        if(finalVector == negValue || finalVector == posValue)
        {
            audio.Play();
            calibrated = true;
            //onDialCalibrated?.Invoke();
        }
        else
        {
            calibrated = false;
        }
    }
}
