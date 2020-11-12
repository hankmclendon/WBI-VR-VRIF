using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoDoor : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();    
    }

    public void OpenDoor()
    {
        anim.SetTrigger("Open");
    }
}
