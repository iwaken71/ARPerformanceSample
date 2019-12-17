using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Camera))]
public class OcculusionSample : MonoBehaviour
{
    [SerializeField]
    AROcclusionManager occlusionManager;

    [SerializeField]
    Shader shader;

    [SerializeField]
    Texture2D debugTex;
    Material material;

    void Awake()
    {
        material = new Material(shader);
    }
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (occlusionManager != null)
        {
            material.SetTexture("_StencilTex", occlusionManager.humanStencilTexture);
        }
        Graphics.Blit(src, dest, material);
    }
}