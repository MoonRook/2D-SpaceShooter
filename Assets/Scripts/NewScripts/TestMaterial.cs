using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestMaterial : MonoBehaviour
{
    public MeshRenderer Renderer;

    public Material m_Mat;

    public float t;

    public void Start()
    {
        m_Mat = Renderer.material;
    }

    public void Update()
    {
        m_Mat.mainTextureOffset += new Vector2(t, t);
    }
}
