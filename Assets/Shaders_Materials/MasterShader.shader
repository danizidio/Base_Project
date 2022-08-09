Shader "SpriteShader/ComboFX"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull mode", Float) = 0
        [Toggle]
        _ZWrite("ZWrite", float) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 0
        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcBlend("SrcBlend Mode", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstBlend("DstBlend Mode", Float) = 10
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]_Color("Color", Color) = (1, 1, 1, 1)

    // -- Outline --

[Toggle]
_OutlineEnabled("Outline", Int) = 0
[Toggle]
_OutlineOnly("Outline only", Int) = 0
[HDR]_OutlineColor("Outline color", Color) = (1, 1, 1, 1)
_OutlineSize("Outline size", Range(0, 0.05)) = 0.02
_OutlineTex("Outline texture", 2D) = "white" {}
_OutlineXSpeed("Outline X Speed", Range(0, 1)) = 0
_OutlineYSpeed("Outline Y Speed", Range(0, 1)) = 0

// -- Color multiply -- 
[Toggle]
_ColorToMultiplyEnabled("Color to multiply", Int) = 0
[HDR]_ColorToMultiply("Color to multiply", Color) = (1, 1, 1, 1)

// -- Dissolve -- 
[Toggle]
_DissolveEnabled("Dissolve", Int) = 0
_DissolveTex("Dissolve texture", 2D) = "white" {}
_DissolveAmount("Dissolve amount", Range(0, 1)) = 0
[HDR]_DissolveColor("Dissolve color", Color) = (1, 1, 1, 1)
_DissolveGlow("Dissolve glow", Range(0, 0.3)) = 0

// -- yColor -- 
[Toggle]
_HeightColorTransitionEnabled("Height color to transite", Int) = 0
[HDR]_HeightColorTransition("Height color to transite", Color) = (1, 1, 1, 1)
_HeightColorIntensity("Height color intensity", Range(0, 1)) = 1
_HeightColorPropagation("Height color propagation", Range(0.1, 10)) = 10

// -- Hologram -- 
[Toggle]
_HologramEnabled("Hologram", Int) = 0
[HDR]_HologramColor("Hologram color", Color) = (1, 1, 1, 1)
_HologramStripAmount("Hologram strip size", Range(0, 10)) = 5
_HologramSpeed("Hologram speed", Range(-1, 1)) = 0.4

// -- Distortion -- 
[Toggle]
_DistortionEnabled("Distortion", Int) = 0
_DistortionNoise("Distortion noise", 2D) = "white" {}
_DistortionIntensity("Distortion intensity", Range(0, 0.2)) = 0.01
_DistortionSpeed("Distortion speed", Range(-0.5, 0.5)) = 0.2

// -- GrayScale -- 
[Toggle]
_GrayScaleEnabled("Gray scale", Int) = 0
_GrayScaleIntensity("Gray scale intensity", Range(0, 1)) = 1

// -- Negative -- 
[Toggle]
_NegativeEnabled("Negative", Int) = 0
_NegativeIntensity("Negative intensity", Range(0, 1)) = 1

// -- DropShadow -- 
[Toggle]
_DropShadowEnabled("Drop shadow", Int) = 0
_DropShadowOffset_X("Drop shadow offset (X)", Range(-1, 1)) = -0.01
_DropShadowOffset_Y("Drop shadow offset (Y)", Range(-1, 1)) = -0.01
[HDR]_DropShadowColor("Drop shadow color", Color) = (1, 1, 1, 1)

// -- Offset -- 
[Toggle]
_OffsetEnabled("UV Offset", Int) = 0
_Offset_X("Offset X", Range(-1, 1)) = 0
_Offset_Y("Offset Y", Range(-1, 1)) = 0
_OffsetSpeed_X("Speed X", Range(-1, 1)) = 0
_OffsetSpeed_Y("Speed Y", Range(-1, 1)) = 0

// -- Pixelate
[Toggle]
_PixelateEnabled("Pixelate", Int) = 0
_PixelDensity("Pixel density", Range(3, 100)) = 10

// -- Vibration
[Toggle]
_VibrationEnabled("Vibration", Int) = 0
_VibrationNoise("Vibration noise", 2D) = "white" {}
_VibrationIntensity("Vibration intensity", Range(0, 1)) = 0
_VibrationSpeed("Vibration speed", Float) = 0

// -- Color transition
[Toggle]
_ColorOverlayEnabled("Color overlay", Int) = 0
_ColorOverlayIntensity("Color overlay intensity", Range(0, 1)) = 0.5
[HDR]_ColorOverlayColor("Color overlay color", Color) = (1, 1, 1, 1)


    }
    SubShader
    {
        //Tags { "Queue" = "Transparent" "IgnoreProjector"= "True" "RenderType"="Transparent"}
        //Tags { "RenderType"="Opaque" }
        //LOD 100
                Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "UniversalMaterialType" = "Lit"
            "Queue" = "Transparent"
            "ShaderGraphShader" = "true"
            "ShaderGraphTargetId" = ""
        }

        Cull[_Cull]
        ZWrite[_ZWrite]
        ZTest[_ZTest]

        Pass
        {
            HLSLPROGRAM
            //CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            // -- Outline -- 
            float _OutlineEnabled;
            float4 _OutlineColor;
            float _OutlineSize;
            float _OutlineOnly;
            sampler2D _OutlineTex;
            float4 _OutlineTex_ST;
            float _OutlineXSpeed;
            float _OutlineYSpeed;
            // -- Color multiply -- 
            float _ColorToMultiplyEnabled;
            float4 _ColorToMultiply;
            // -- Dissolve -- 
            float _DissolveEnabled;
            sampler2D _DissolveTex;
            float4 _DissolveTex_ST;
            float _DissolveAmount;
            float4 _DissolveColor;
            float _DissolveGlow;
            // -- yColor -- 
            float _HeightColorTransitionEnabled;
            float4 _HeightColorTransition;
            float _HeightColorIntensity;
            float _HeightColorPropagation;
            // -- Hologram -- 
            float _HologramEnabled;
            float4 _HologramColor;
            float _HologramStripAmount;
            float _HologramSpeed;
            // -- Distortion -- 
            float _DistortionEnabled;
            sampler2D _DistortionNoise;
            float4 _DistortionNoise_ST;

            float _DistortionIntensity;
            float _DistortionSpeed;
            // -- GrayScale -- 
            float _GrayScaleEnabled;
            float _GrayScaleIntensity;
            // -- Negative -- 
            float _NegativeEnabled;
            float _NegativeIntensity;
            // -- DropShadow -- 
            float _DropShadowEnabled;
            float _DropShadowOffset_X;
            float _DropShadowOffset_Y;
            float4 _DropShadowColor;
            // -- Offset -- 
            float _OffsetEnabled;
            float _Offset_X;
            float _Offset_Y;
            float _OffsetSpeed_X;
            float _OffsetSpeed_Y;
            // -- Pixelate
            float _PixelateEnabled;
            float _PixelDensity;
            // -- Vibration
            float _VibrationEnabled;
            sampler2D _VibrationNoise;
            float _VibrationIntensity;
            float _VibrationSpeed;
            // -- Color overlay
            float _ColorOverlayEnabled;
            float _ColorOverlayIntensity;
            float4 _ColorOverlayColor;


            float4 outline(float4 base, float2 uv) {

                float2 outlineUV = TRANSFORM_TEX(uv, _OutlineTex);
                float outlineMap = (tex2D(_MainTex, uv + float2(_OutlineSize, 0)) +
                    tex2D(_MainTex, uv + float2(-_OutlineSize, 0)) +
                    tex2D(_MainTex, uv + float2(0, _OutlineSize)) +
                    tex2D(_MainTex, uv + float2(0, -_OutlineSize))).a;

                outlineMap = saturate(outlineMap);

                float4 outline = saturate(outlineMap - base.a);

                float2 animatedUV = outlineUV + float2(_Time.y * _OutlineXSpeed, _Time.y * _OutlineYSpeed);

                float4 outlineTex = tex2D(_OutlineTex, animatedUV);
                outline = lerp(base * (1 - _OutlineOnly), outlineTex * _OutlineColor, outline);

                float4 finalColor = outline + base;

                return outline;
            }

            float4 colorMultiply(float4 base) {
                return base * _ColorToMultiply;
            }

            float4 dissolve(float4 base, float2 uv) {
                float2 dissolveUV = TRANSFORM_TEX(uv, _DissolveTex);
                fixed dissolveNoise = tex2D(_DissolveTex, dissolveUV).r;

                float innerStep = step(dissolveNoise, 1 - _DissolveAmount);
                float outerStep = step(dissolveNoise + _DissolveGlow, 1 - _DissolveAmount);

                float finalStep = innerStep - outerStep;

                float4 dissolveColor = lerp(finalStep.rrrr * _DissolveColor, base, outerStep);

                dissolveColor *= base.a;

                return dissolveColor;
            }
            float4 yColor(float4 base, float2 uv) {
                float uvHeight = saturate(pow(1 - uv.y, _HeightColorPropagation) * 2.5f);

                float4 finalColor = lerp(base, _HeightColorTransition, uvHeight * _HeightColorIntensity);

                finalColor.a = base.a;

                return finalColor;
            }
            float4 hologram(float4 base, float2 uv) {
                float hologramPattern = frac(uv.y * _HologramStripAmount + _Time.y * _HologramSpeed);

                float4 finalColor = base;
                finalColor.rgb = lerp(base, _HologramColor, 1 - hologramPattern);
                finalColor.a = _HologramColor.a * finalColor.a;

                return finalColor;
            }

            float2 distortion(float2 uv) {
                float2 distortionUV = TRANSFORM_TEX(uv, _DistortionNoise);

                float2 distortionSpeed = float2(0, _Time.y * _DistortionSpeed);

                fixed4 noiseMap_01 = tex2D(_DistortionNoise, distortionUV + distortionSpeed);
                fixed4 noiseMap_02 = tex2D(_DistortionNoise, distortionUV + distortionSpeed * 0.23);

                uv.x += noiseMap_01.r * _DistortionIntensity;
                uv.x -= noiseMap_02.r * _DistortionIntensity;

                return uv;
            }

            float4 grayScale(float4 base) {
                float3 grayScaleColor = dot(base.rgb, float3(0.3, 0.59, 0.11));

                base.rgb = lerp(base.rgb, grayScaleColor, _GrayScaleIntensity);

                return base;
            }

            float4 negative(float4 base) {
                float3 negativeColor = abs(base.rgb - 1);

                base.rgb = lerp(negativeColor, base.rgb, _NegativeIntensity);

                return float4(abs(base.rgb - 1), base.a);
            }

            float4 dropShadow(float4 base, float2 uv) {
                fixed4 dropShadowMap = tex2D(_MainTex, uv + float2(_DropShadowOffset_X * -1, _DropShadowOffset_Y * -1));

                fixed4 dropShadow = (dropShadowMap.a - base.a) * _DropShadowColor;

                float4 finalColor = lerp(base, dropShadow, 1 - base.a);

                return finalColor;
            }

            float2 offset(float2 uv) {
                float xSpeed = abs(_Offset_X) > 0 ? _OffsetSpeed_X : 0;
                float ySpeed = abs(_Offset_Y) > 0 ? _OffsetSpeed_Y : 0;

                uv += float2(_Offset_X + (xSpeed * _OffsetSpeed_X * _Time.y) * sign(_OffsetSpeed_X),
                    _Offset_Y + (ySpeed * _OffsetSpeed_Y * _Time.y) * sign(_OffsetSpeed_Y));

                return uv;
            }

            float2 pixelate(float2 uv)
            {
                float2 horizontalAspectRatio = float2(_ScreenParams.x / _ScreenParams.y, 1);
                float2 verticalAspectRatio = float2(1, _ScreenParams.y / _ScreenParams.x);

                float aspectRatio = _ScreenParams.x - _ScreenParams.y > 0 ? horizontalAspectRatio : verticalAspectRatio;

                float2 pixel = _PixelDensity * aspectRatio;
                uv = round(uv * pixel) / pixel;

                return uv;
            }

            float2 vibrate(float2 uv)
            {
                float2 vibrateUV = uv;
                float vibrateNoise_01 = tex2D(_VibrationNoise, float2(_Time.y * _VibrationSpeed, 0)).r - 0.5;
                float vibrateNoise_02 = tex2D(_VibrationNoise, float2(_Time.y * _VibrationSpeed * 0.7, 0)).r - 0.5;

                float vibrateFactor = vibrateNoise_01 * _VibrationIntensity;

                vibrateUV.x += vibrateFactor;
                vibrateFactor = vibrateNoise_02 * _VibrationIntensity;
                vibrateUV.y -= vibrateFactor;

                return vibrateUV;
            }

            float4 colorOverlay(float4 base)
            {
                return lerp(base, _ColorOverlayColor, _ColorOverlayIntensity) * base.a;
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed2 uv = i.uv;

                if (_OffsetEnabled > 0.1)
                    uv = offset(uv);

                if (_DistortionEnabled > 0.1)
                    uv = distortion(uv);

                if (_PixelateEnabled > 0.1)
                    uv = pixelate(uv);

                if (_VibrationEnabled > 0.1)
                    uv = vibrate(uv);

                fixed4 col = tex2D(_MainTex, uv);

                if (_GrayScaleEnabled > 0.1)
                    col = grayScale(col);

                if (_HologramEnabled > 0.1)
                    col = hologram(col, uv);

                if (_NegativeEnabled > 0.1)
                    col = negative(col);

                if (_ColorToMultiplyEnabled > 0.1)
                    col = colorMultiply(col);

                if (_ColorOverlayEnabled > 0.1)
                    col = colorOverlay(col);

                if (_HeightColorTransitionEnabled > 0.1)
                    col = yColor(col, uv);

                if (_OutlineEnabled > 0.1)
                    col = outline(col, uv);

                if (_DropShadowEnabled > 0.1)
                    col = dropShadow(col, uv);

                if (_DissolveEnabled > 0.1)
                    col = dissolve(col, uv);

                return col;
            }

            ENDHLSL
            //ENDCG
        }
    }
}
