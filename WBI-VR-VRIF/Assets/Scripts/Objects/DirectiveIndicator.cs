using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectiveIndicator : MonoBehaviour
{

    private Animator anim;
    private MeshRenderer meshRenderer;
    private Material material;

    void Start()
    {
        anim = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        
    }

    public void Select()
    {
        anim.SetBool("isActive", true);
    }

    public void Deselect()
    {
        anim.SetBool("isActive", false);
    }
}
