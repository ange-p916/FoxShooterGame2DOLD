﻿Shader "Custom/ReflectionShader" 
{
	Properties 
	{
		_MainTex("Base (RGB)", 2D) = "white"{}
		_Color ("Tint", Color) = (1,1,1,1)
		[HideInInspector]_ReflectionTex("",2D) = "white" {}
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Pass {

	
		CGPROGRAM

#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		struct appdata_t {
			float2 uv : TEXCOORD0;
			float4 refl : TEXCOORD1;
			float4 pos : POSITION;
			float4 col : COLOR;
		};

		struct v2f {
			float2 uv : TEXCOORD0;
			float4 refl : TEXCOORD1;
			float4 pos : POSITION;
			fixed4 col : COLOR;
		};

		float4 _MainTex_ST;
		fixed4 _Color;

		sampler2D _MainTex;
		sampler2D _ReflectionTex;

		v2f vert(appdata_t i)
		{
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, i.pos);
			o.uv = TRANSFORM_TEX(i.uv, _MainTex);
			o.col = i.col * _Color;
			o.refl = ComputeScreenPos(o.pos);
			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 tex = tex2D(_MainTex, i.uv) * i.col;
			tex.rgb *= tex.a;
			fixed4 refl = tex2Dproj(_ReflectionTex, UNITY_PROJ_COORD(i.refl));
			return tex * refl;
		}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
