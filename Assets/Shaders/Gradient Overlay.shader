Shader "Custom/Gradient Overlay" 
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		[MaterialToggle] _WorldSpace("World Space", Float) = 1
		_ColorLow("Color Low", COLOR) = (1,1,1,1)
		_ColorHigh("Color High", COLOR) = (1,1,1,1)
		_yPosLow("Y Pos Low", Float) = 0
		_yPosHigh("Y Pos High", Float) = 10
		_GradientStrength("Gradient Strength", Float) = 1
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Geometry"
			"RenderType" = "Transparent"
		}

		CGPROGRAM
		#pragma surface surf Lambert

		fixed4 _ColorLow;
		fixed4 _ColorHigh;
		half _yPosLow;
		half _yPosHigh;
		half _GradientStrength;
		float _WorldSpace;

		struct Input
		{
			float2 uv_MainTex;
			float3 worldPos;
			float3 normal;
		};


		fixed4 LightingNoLighting (SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed3 ambient = 0;
			fixed4 c;
			ambient = UNITY_LIGHTMODEL_AMBIENT;
			c.rgb = s.Albedo * ambient;
			c.a = s.Alpha;
			return c;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			float y_Modded_by_Space = (IN.uv_MainTex.y * (1 - _WorldSpace)) + (IN.worldPos.y * _WorldSpace);
			half3 gradient = lerp(_ColorLow, _ColorHigh,  smoothstep(_yPosLow, _yPosHigh, y_Modded_by_Space)).rgb;
			gradient = lerp(fixed3(1, 1, 1), gradient, _GradientStrength);

			o.Albedo = saturate(gradient);
			o.Alpha = 1;
		}
		ENDCG
	}

	fallback "Diffuse"
}