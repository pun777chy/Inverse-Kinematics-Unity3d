Shader "Effects/Dissolve" {
	Properties {
        _Color ("Color", Color) = (1,1,1,1)
		_CoreColor ("Core Color", Color) = (1,1,1,1)
		_Core("Core Texture", 2D) = "white" {}
        _Mask("Mask To Dissolve", 2D) = "white" {}
		_ColorStrength("Core Color Strength", Float) = 100
        _Range ("Range [0-2]", Range(0, 2)) = 0
    }
    
    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 300
        CGPROGRAM 
        #pragma surface surf Lambert alpha 

		sampler2D _Core;
		sampler2D _Mask;
        sampler2D _BumpMap;
        half4 _Color;
		half4 _CoreColor;
        float _Range;
		float _ColorStrength;
                   
        struct Input {
			float2 uv_Core;
			float2 uv_Mask;
            float2 uv_BumpMap;
        };
            
        void surf (Input IN, inout SurfaceOutput o) {
			half4 core = tex2D (_Core, IN.uv_Core) * _CoreColor * _ColorStrength;
            half4 mask = tex2D (_Mask, IN.uv_Mask);
             
			fixed delta = 2 - _Range;
			fixed4 col = core * mask.a - delta;
			fixed4 res = mask.a >= _Range - 2? core/_ColorStrength + col : 0;
			o.Emission = res < 0 ? 0 : res;
            //o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Alpha = saturate(core.a * _Range);
           
        }
        ENDCG
    } 
    Fallback "Diffuse"
}