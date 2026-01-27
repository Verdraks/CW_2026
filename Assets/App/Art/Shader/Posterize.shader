Shader "Hidden/Posterize"
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        Cull Off
        ZWrite Off
        ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
            
            float _Level;

            half4 Frag(Varyings i) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, i.texcoord);
                half greyscale = max(color.r, max(color.g, color.b));
                half lower = floor(greyscale * _Level) / _Level;
                half lowerDiff = abs(greyscale - lower);
                half upper = ceil(greyscale * _Level) / _Level;
                half upperDiff = abs(greyscale - upper);
                half level = lowerDiff <= upperDiff ? lower : upper;
                half adjustment = level / greyscale;
                color.rgb *= adjustment;
                return color;
            }
            ENDHLSL
        }
    }
}