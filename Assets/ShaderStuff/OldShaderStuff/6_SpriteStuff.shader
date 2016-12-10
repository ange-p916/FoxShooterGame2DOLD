Shader "Custom/6_SpriteStuff" {
	Properties{
		_MainTex("The texture", 2D) = "white"{}
		_Color("The Color", Color) = (1,1,1,1)
	}

	SubShader{
		Tags{"RenderType" = "Opaque"}
		CGPROGRAM

#pragma surface surf Lambert vertex:vert


		struct Input {
			float2 uv_MainTex;
		};

		sampler2D _MainTex;
		fixed4 Color;

		void vert(inout appdata_full v)
		{
			float phase = _Time[1] * 2.0;
			float4 wpos = mul(_Object2World, v.vertex);
			float offset = wpos.x + wpos.y;
			wpos.y = sin(phase + offset) * 0.2;
			v.vertex = mul(_World2Object, wpos);
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * Color;
			o.Alpha = c.a * Color.a;
		}
		


		ENDCG
	}
}
