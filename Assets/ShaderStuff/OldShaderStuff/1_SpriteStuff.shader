Shader "Custom/MySpriteShader"{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Sprite Texture", 2D) = "white"{}
	}

		SubShader
	{

		Tags
		{
			"Queue" = "Geometry"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
		}

		Cull Off
		Lighting Off
		ZWrite On
		Blend One OneMinusSrcAlpha

		Pass
		{


			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			sampler2D _MainTex;

				struct vertInput
				{
					float4 pos : POSITION;
					fixed4 color : COLOR;
					half2 texcoord : TEXCOORD0;
					
				};

				struct vertOutput
				{
					float4 pos : SV_POSITION;
					fixed4 color : COLOR;
					half2 texcoord : TEXCOORD0;
					float4 worldPos : TEXCOORD1;
				};

				vertOutput vert(vertInput input)
				{
					vertOutput o;
					o.worldPos = input.pos;
					o.pos = mul(UNITY_MATRIX_MVP, o.worldPos);
					o.texcoord = input.texcoord;
					o.color = input.color * _Color;

					return o;
				}

				half4 frag(vertInput IN) : SV_TARGET
				{
					fixed4 color = (tex2D(_MainTex, IN.texcoord)) * IN.color * sin(_Time[1]);
					
					return color;
				}

				ENDCG
			}
	}
}