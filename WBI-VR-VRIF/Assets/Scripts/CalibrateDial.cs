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
    private Vector2 minPosValue, maxPosValue, minNegValue, maxNegValue;

    //public UnityEvent onDialCalibrated;

    void Start()
    {
        rend = GetComponent<Renderer>();
        audio = GetComponent<AudioSource>();
        startingVector = rend.material.GetTextureOffset("_BaseMap");
        startingOffset = offsetVector.x;
        
        minPosValue = posValue - new Vector2(0.1f, 0);
        maxPosValue = posValue + new Vector2(0.1f, 0);
        minNegValue = negValue - new Vector2(0.1f, 0);
        maxNegValue = negValue + new Vector2(0.1f, 0);

        Debug.Log("minPosValue = " + minPosValue);
        Debug.Log("maxPosValue = " + maxPosValue);
        Debug.Log("minNegValue = " + minNegValue);
        Debug.Log("maxNegValue = " + maxNegValue);
    }
    public void UpdateDial(float dialValue)
    {
        dialValue = dialValue / 360;
        float roundedValue = Mathf.Round(dialValue * 100.0f) * 0.01f;
        offsetVector = new Vector2(roundedValue, 0);
        finalVector = startingVector + offsetVector;

        if (finalVector.x > minNegValue.x && finalVector.x < maxNegValue.x)
            finalVector = negValue;
        if (finalVector.x > minPosValue.x && finalVector.x < maxPosValue.x)
            finalVector = posValue;

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
