Shader "Custom/Outline"
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
            Name "Silhouette"
            
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            float4 Frag (Varyings input) : SV_Target
            {
                float4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, input.texcoord).rgba;
                float alpha = color.a > 0.0 ? 1.0 : 0.0;
                return float4(0.0, 0.0, 0.0, alpha);
            }
            ENDHLSL
        }

        Pass
        {
            Name "Blur"
            
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            float4 Frag (Varyings input) : SV_Target
            {
                float2 uv = input.texcoord;
                float2 texelSize = 1.0 / float2(_ScreenParams.x, _ScreenParams.y);
                
                float4 sum = float4(0.0, 0.0, 0.0, 0.0);
                
                // Simple 3x3 box blur
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        float2 offset = float2(x, y) * texelSize;
                        sum += SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, uv + offset);
                    }
                }
                
                return sum / 9.0;
            }
            ENDHLSL
        }
        
        Pass
        {
            Name "Outline"

            HLSLPROGRAM
            
            #pragma vertex Vert
            #pragma fragment Frag

            float4 Frag (Varyings input) : SV_Target
            {
                float4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, input.texcoord).rgba;
                return color;
            }
            
            ENDHLSL
        }
    }
}
