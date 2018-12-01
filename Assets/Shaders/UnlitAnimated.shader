
Shader "Unlit/Animated" {
 Properties {
	 _SpeedX ("Scroll Speed X", float) = 0
	 _SpeedY ("Scroll Speed Y", float) = 0
	 _Color ("Tint", Color) = (1,1,1,1)
     _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
 }
 
 SubShader {
     Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
     LOD 100
     
     ZWrite Off
     Blend SrcAlpha OneMinusSrcAlpha 
     
     Pass {  
         CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile_fog
             
             #include "UnityCG.cginc"
 
             struct appdata_t {
                 float4 vertex : POSITION;
                 float2 texcoord : TEXCOORD0;
             };
 
             struct v2f {
                 float4 vertex : SV_POSITION;
                 half2 texcoord : TEXCOORD0;
                 UNITY_FOG_COORDS(1)
             };
 
			 fixed4 _Color;
			 fixed _SpeedX;
			 fixed _SpeedY;
             sampler2D _MainTex;
             float4 _MainTex_ST;
             
             v2f vert (appdata_t v)
             {
                 v2f o;
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				 o.texcoord.x += _Time.y * _SpeedX;
				 o.texcoord.y += _Time.y * _SpeedY;
                 UNITY_TRANSFER_FOG(o,o.vertex);
                 return o;
             }
             
             fixed4 frag (v2f i) : SV_Target
             {
                 fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;
                 UNITY_APPLY_FOG(i.fogCoord, col);
                 return col;
             }
         ENDCG
     }
 }
 
 }