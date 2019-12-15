using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class HumanSeg : MonoBehaviour
{
    private RenderTexture _captureTexture = null;
    bool initialized = false;
    [SerializeField]
    Material mat;

    [SerializeField]
    ARCameraBackground arCamBg;

    [SerializeField]
    AROcclusionManager occulusionManager;

    void Awake()
    {
        _captureTexture = new RenderTexture(Screen.width, Screen.height, 0);
    }

    void Update()
    {
        // var mat = image.GetComponent<RawImage>().material;
        /// var mat = this.mat.GetComponent<Image>().material;
        // var mat = image.GetComponent<Renderer>().material;
        // var mat = image.GetComponentInChildren<Renderer>().material;

        if (arCamBg.material != null)
        {
            Graphics.Blit(null, _captureTexture, arCamBg.material);
        }
        Texture2D stencilTex = occulusionManager.humanStencilTexture;
        if (!initialized)
        {
            // stencilTex(256, 192)
            if (stencilTex != null)
            {
                Debug.LogFormat("stencilTex({0},{1})", stencilTex.width, stencilTex.height);
                Debug.LogFormat("_captureTexture({0},{1})", _captureTexture.width, _captureTexture.height);
                float stencilRatio = (float)stencilTex.width / (float)stencilTex.height;
                float captureRatio = (float)_captureTexture.height / (float)_captureTexture.width;
                var ratio = captureRatio / stencilRatio;
                Debug.Log("Debugg ratio:" + ratio);
                mat.SetFloat("_Ratio", ratio);
                initialized = true;
            }
        }
        mat.SetTexture("_OverTex", _captureTexture);
        mat.SetTexture("_MaskTex", stencilTex);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}
