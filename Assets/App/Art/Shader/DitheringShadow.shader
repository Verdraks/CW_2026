Shader "Hidden/DitheringShadow"
{   
    SubShader
    {
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
        ENDHLSL

        Tags { "RenderType"="Opaque" }
        LOD 100
        ZWrite Off Cull Off
        Pass
        {
            Name "DitheringShadow"

            HLSLPROGRAM
            
            #pragma vertex Vert
            #pragma fragment Frag


            float _Threshold = 0.95;


            float random(float2 st)
            {
                return frac(sin(dot(st.xy,
                                     float2(12.9898,78.233))) *
                    43758.5453123);
            }
            
            float4 Frag (Varyings input) : SV_Target
            {
                float4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, input.texcoord).rgba;
                float luminance = Luminance(color);
                
                // float closestColor = rsqrt(pow(color.r,2) + pow(color.g,2) + pow(color.b,2));
                
                
                float greyscale = luminance + random(input.texcoord) -0.5 > _Threshold ? 1.0 : 0.0;
                
                return greyscale * color;
            }
            
            ENDHLSL
        }
    }
}
