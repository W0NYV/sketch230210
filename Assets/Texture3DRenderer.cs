using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Texture3DGenerator))]
public class Texture3DRenderer : MonoBehaviour
{

    [SerializeField] private Material _material; 
    private Texture3DGenerator _texture3DGenerator;

    private void Start() {
        TryGetComponent<Texture3DGenerator>(out _texture3DGenerator);
    }

    private void Update()
    {
        _material.SetTexture("_MainTex", _texture3DGenerator.RenderTexture3D);
    }
}
