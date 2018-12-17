Shader "Custom/ColorSpecReflexive" 
{
Properties 
{
  _MainTex ("Texture", 2D) = "white" {}
  _SpecTex ("SpecMap", 2D) = "white" {}
  _SpecStr ("Specular", Float ) = 1
  _Shininess ("Shininess", Range (0.03, 10)) = 0.078125
  _BumpMap ("Normalmap", 2D) 	= "bump" {}
  _ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
  _Cube ("Reflection Cubemap", Cube) = "" { TexGen CubeReflect }
  _AO ("Texture", 2D) = "white" {}
  _AOStr ("AO Value", Float ) = 1

}
SubShader 
{
    LOD 400 
    Tags { "RenderType" = "Opaque" }
	CGPROGRAM
	#pragma surface surf ColoredSpecular
	#pragma target 3.0
	#pragma glsl 
	#pragma exclude_renderers d3d11_9x
	 
	struct MySurfaceOutput 
	{
	    half3 Albedo;
	    half3 Normal;
	    half3 Emission;
	    half Specular;
	    half3 GlossColor;
	    half Alpha;
	};
	 
	 
	half4 LightingColoredSpecular (MySurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
	{
		half3 h = normalize (lightDir + viewDir);

		half diff = max (0, dot (s.Normal, lightDir));

			
		float nh = max (0, dot (s.Normal, h));
		float spec = pow (nh, s.Specular * 128.0);
		half3 specCol = spec * s.GlossColor;

		half4 c;
		c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * specCol) * (atten * 2);
		
		
		c.a = s.Alpha;
		return c;
	}
	 
	half4 LightingColoredSpecular_PrePass (MySurfaceOutput s, half4 light)
	{
	    half3 spec = light.a * s.GlossColor;
	   
	    half4 c;
	    c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
	    c.a = s.Alpha + spec * _SpecColor.a;
	    return c;
	}
	 
	 
	struct Input 
	{
		float2 uv_MainTex;
		float2 uv_SpecTex;
		float3 worldRefl;
		INTERNAL_DATA
	};
	
	half _Shininess, _SpecStr, _AOStr;
	fixed4 _ReflectColor;
	
	sampler2D _MainTex, _BumpMap, _SpecTex, _AO;
	samplerCUBE _Cube;
	
	
	void surf (Input IN, inout MySurfaceOutput o)
	{
		half AO = tex2D (_AO, IN.uv_MainTex).r;
		AO = pow(AO, _AOStr);
	
		o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * AO;
		
		half4 spec = tex2D (_SpecTex, IN.uv_SpecTex);
		
		o.GlossColor = spec.rgb * _SpecStr * AO;
		o.Specular =  spec.a * _Shininess;
		
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
		
		float3 worldRefl = WorldReflectionVector (IN, o.Normal);
		fixed3 reflcol = texCUBE (_Cube, worldRefl);
		o.Emission = reflcol * spec.rgb * _ReflectColor.rgb * AO;
	}
ENDCG
}


SubShader 
{

	LOD 200 
    Tags { "RenderType" = "Opaque" }
	CGPROGRAM
	#pragma surface surf ColoredSpecular

	 
	struct MySurfaceOutput 
	{
	    half3 Albedo;
	    half3 Normal;
	    half3 Emission;
	    half Specular;
	    half3 GlossColor;
	    half Alpha;
	};
	 
	 
	half4 LightingColoredSpecular (MySurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
	{
		half3 h = normalize (lightDir + viewDir);

		half diff = max (0, dot (s.Normal, lightDir));

			
		float nh = max (0, dot (s.Normal, h));
		float spec = pow (nh, s.Specular * 128.0);
		half3 specCol = spec * s.GlossColor;

		half4 c;
		c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * specCol) * (atten * 2);
		
		
		c.a = s.Alpha;
		return c;
	}
	 
	half4 LightingColoredSpecular_PrePass (MySurfaceOutput s, half4 light)
	{
	    half3 spec = light.a * s.GlossColor;
	   
	    half4 c;
	    c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
	    c.a = s.Alpha + spec * _SpecColor.a;
	    return c;
	}
	 
	 
	struct Input 
	{
		float2 uv_MainTex;
		float2 uv_SpecTex;
		float2 uv_BumpMap;
	};
	
	half _Shininess, _SpecStr, _AOStr;
	
	sampler2D _MainTex, _BumpMap, _SpecTex, _AO;

	
	
	void surf (Input IN, inout MySurfaceOutput o)
	{
		half3 AO = tex2D (_AO, IN.uv_MainTex).rgb;
		AO = pow(AO, _AOStr);
	
	
		o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * AO;
		
		half4 spec = tex2D (_SpecTex, IN.uv_SpecTex);
		
		o.GlossColor = spec.rgb * _SpecStr * AO;
		o.Specular =  spec.a * _Shininess;
		
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		
		
	}
ENDCG
}

fallback "Diffuse"

}