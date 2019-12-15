Shader "Unlit/Segmantation"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _OverTex("Base Texture (RGB)", 2D) = "white" {}
        _MaskTex("Mask Texture (RGB)", 2D) = "white" {}
        _Ratio ("Ratio", Float) = 1.66
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MaskTex;
        sampler2D _OverTex;
        float _Ratio;

        struct Input {
            float2 uv_MaskTex;
            float2 uv_OverTex;
        };

        fixed4 _Color;

        void surf(Input IN, inout SurfaceOutputStandard o) {
            fixed2 uv = IN.uv_MaskTex;
            // Flip x y axis.
            // float2 uv2 = uv;
            // uv2.x = uv.y;
            // uv2.y = uv.x;

            // ROTATE 90
            float2 uv2 = uv;
            uv2.x = uv.y;
            uv2.y = 1.0 - uv.x;
            uv2.x = 1.0 - uv2.x;
            
            // 256x192 to 2688x1242
            // float ratio = 1.62;
            // 256x192 to 1792x828
            // float ratio = 1.66;
            float ratio = _Ratio;
            uv2.y /= ratio;
            uv2.y += 1.0 - (ratio * 0.5);

            // ROTATE 270
            // float2 uv2 = uv;
            // uv2.x = 1.0 - uv.y;
            // uv2.y = uv.x;
            
            // uv.x = 1.0 - uv.x;
            // uv.x = 1.0 - uv.x;
            // fixed4 mask = tex2D(_MaskTex, IN.uv_MaskTex);
            fixed4 mask = tex2D(_MaskTex, uv2);
            clip(mask.r - 0.5); // do not draw if mask.r is less than 0.5

            fixed2 center = fixed2(0.5, 0.5);

            fixed4 over = tex2D(_OverTex, IN.uv_OverTex);
            fixed4 c = mask.r * over * _Color;
            o.Emission = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
