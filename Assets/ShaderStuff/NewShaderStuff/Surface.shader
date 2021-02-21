// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Splatter/Surface" {
	Properties{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_AlphaCutoff("Alpha cutoff", Range(0.01,1.0)) = 0.01
	}
	SubShader {
		Tags{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass{
		Stencil{
			Ref 5
			Comp Always
			Pass Replace
		}
		

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile _ PIXELSNAP_ON
#include "UnityCG.cginc"

		struct appdata_t
		{
			float4 vertex : POSITION;
			float4 color : COLOR;
			float2 texcoord : TEXCOORD0;
		};

		struct v2f {
			float4 vertex : SV_POSITION;
			fixed4 color : COLOR;
			half2 texcoord : TEXCOORD0;
		};

		fixed4 _Color;
		fixed _AlphaCutoff;

		v2f vert(appdata_t i)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(i.vertex);
			o.texcoord = i.texcoord;
			o.color = i.color * _Color;
#ifdef PIXELSNAP_ON
			o.vertex = UnityPixelSnap(o.vertex);
#endif
			return o;
		}

		sampler2D _MainTex;
		sampler2D _AlphaTex;
		float _AlphaSplitEnabled;

		fixed4 SampleSpriteTexture(float2 uv)
		{
			fixed4 color = tex2D(_MainTex, uv);
			if (_AlphaSplitEnabled)
				color.a = tex2D(_AlphaTex, uv).r;

			return color;
		}

		fixed4 frag(v2f i) : SV_TARGET
		{
			fixed4 c = SampleSpriteTexture(i.texcoord) * i.color;
			c.rgb *= c.a;

			//discard pixels below cutoff so that stencil is only updated for visible pixels
			clip(c.a - _AlphaCutoff);

			return c;

		}
		ENDCG
	}
		}

	FallBack "Diffuse"
}
