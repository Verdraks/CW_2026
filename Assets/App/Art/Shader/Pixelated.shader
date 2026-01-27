Shader "Hidden/Pixelated"
{
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"



            float _Pixels = 10;
            
            half4 Frag(Varyings i) : SV_Target
            {
                half2 uv = saturate(floor(i.texcoord * _Pixels) / _Pixels);
                half4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, uv);
                return  color;
            }
            ENDHLSL
        }
    }
}