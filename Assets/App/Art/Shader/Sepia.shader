Shader "Hidden/Sepia"
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        ZWrite Off
        Cull Off
        ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
            
            float _Intensity;

            half4 frag(Varyings input) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, input.texcoord);
                half3 gammaColor = pow(max(color.rgb, 0.0), 1.0 / 2.2);
                half y = dot(gammaColor, half3(0.30, 0.59, 0.11));
                half3 sepiaGamma = saturate(y + half3(0.19, -0.05, -0.22));
                half3 sepiaLinear = pow(sepiaGamma, 2.2);
                return half4(lerp(color.rgb, sepiaLinear.rgb, _Intensity), color.a);
            }
            ENDHLSL
        }
    }
}

