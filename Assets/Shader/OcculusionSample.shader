Shader "OcculusionSample"
{
    Properties
    {
        _MainTex ("_MainTex", 2D) = "white" {}
        _StencilTex ("_StencilTex", 2D) = "white" {}
        _CamRatio ("_CamRatio", Float) = 1.66
    }
    SubShader
    {
        Cull Off 
        ZWrite Off 
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _StencilTex;
            float _CamRatio;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float2 GetStencilUV( float2 uv ){

                float2 stencilUV = float2(
                    1.0 - uv.y,
                    1.0 - uv .x
                );

                // float ratio = _CamRatio / (_ScreenParams.x / _ScreenParams.y);
                // stencilUV.y /= ratio;
                // stencilUV.y += 1.0 - (ratio / 2);
                float camTexWidth = 1920;
                float camTexHeight = 1440;
                float aspect = (camTexWidth/camTexHeight) / (_ScreenParams.y/_ScreenParams.x);
                // float aspect = _CamRatio / (_ScreenParams.y/_ScreenParams.x);

                stencilUV.y = stencilUV.y * aspect + (1 - aspect) / 2;
                return stencilUV;

            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 stencil = tex2D(_StencilTex, GetStencilUV(i.uv));

                return lerp( col, fixed4(1,0,0,1), stencil.r);
                // fixed4 over = tex2D(_OverTex, IN.uv_OverTex);
                // fixed4 c = stencil.r * over * _Color;
                // clip(stencil.r - 0.5);
                // return col * stencil.r;
            }
            ENDCG
        }
    }
}

