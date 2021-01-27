using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTextController : MonoBehaviour
{
    public Animator fadeAnim;
    public void IntroTextComplete()
    {
        fadeAnim.SetBool("FadeIn", true);
        //gameObject.SetActive(false);
    }
}
