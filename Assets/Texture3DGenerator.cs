using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Texture3DGenerator : MonoBehaviour
{

    private int _size = 32;
    private Texture3D _texture;
    public Texture3D Texture { get => _texture; }

    [SerializeField] private ComputeShader _computeShader;

    private RenderTexture _renderTexture;
    public RenderTexture RenderTexture3D { get => _renderTexture; }

    private void Start()
    {
        _renderTexture = new RenderTexture(_size, _size, 0, RenderTextureFormat.ARGB32);
        _renderTexture.enableRandomWrite = true;
        _renderTexture.dimension = TextureDimension.Tex3D;
        _renderTexture.volumeDepth = _size;
        _renderTexture.Create();
    }

    private void Update() {
        CreateTexture3DOnGPU();
    }

    private void CreateTexture3DOnGPU()
    {
        int id = -1;

        id = _computeShader.FindKernel("CreateTex3D");

        _computeShader.SetTexture(id, "_TextureBuffer", _renderTexture);
        _computeShader.SetVector("_Time", Shader.GetGlobalVector("_Time"));
        _computeShader.Dispatch(id, 8, 8, 8);
    }

    //CPU
    private void CreateTexture3D()
    {
        TextureFormat format = TextureFormat.RGBA32;
        TextureWrapMode wrapMode = TextureWrapMode.Clamp;

        _texture = new Texture3D(_size, _size, _size, format, false);
        _texture.wrapMode = wrapMode;

        Color[] colors = new Color[_size * _size * _size];

        float resolution = 1.0f / _size;

        for(int z = 0; z < _size; z++)
        {
            int zOffset = z * _size * _size;
            for(int y = 0; y < _size; y++)
            {
                int yOffset = y * _size;
                for(int x = 0; x < _size; x++)
                {
                    colors[x + yOffset + zOffset] = new Color(x * resolution, y * resolution, z * resolution, 1.0f);
                }
            }
        }

        _texture.SetPixels(colors);
        _texture.Apply();

    }


}
