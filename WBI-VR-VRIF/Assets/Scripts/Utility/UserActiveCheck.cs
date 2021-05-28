using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using UnityEngine.SceneManagement;

public class UserActiveCheck : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CheckUserStatus(2.0f));
    }

    private IEnumerator CheckUserStatus(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print("User Active = " + InputBridge.Instance.HMDActive);
            if (InputBridge.Instance.HMDActive == false)
                SceneManager.LoadScene(0);
        }
    }
}
