using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockpitDoor : MonoBehaviour
{
    [SerializeField]
    private Material hoverMaterial;

    private Material initialMaterial;
    private MeshRenderer m_MeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        initialMaterial = m_MeshRenderer.material;
    }

    public void ChangeToHoverMaterial()
    {
        m_MeshRenderer.material = hoverMaterial;
    }

    public void ChangeToInitialMaterial()
    {
        m_MeshRenderer.material = initialMaterial;
    }
}
