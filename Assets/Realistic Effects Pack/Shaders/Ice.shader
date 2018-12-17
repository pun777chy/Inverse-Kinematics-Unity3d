Shader "Effects/Frozen" {
Properties {
	_Color ("Color", Color) = (1,1,1,1)
	_MainTex ("Ice (R) Overlay (GB) Texture", 2D) = "white" {}
	_CutOut ("Cutout", Range (0, 1)) = 0.3
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transperent"}
	LOD 250
	
CGPROGRAM
#pragma surface surf Lambert alpha

sampler2D _MainTex;
sampler2D _BumpMap;
fixed4 _Color;
float _CutOut;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	o.Alpha = _CutOut > (1-tex.b) ? tex.g : saturate(tex.g - (1 - _CutOut));
	o.Albedo = tex.r * _Color*3 + 0.5;

}
ENDCG
}

Fallback "Diffuse"
}
