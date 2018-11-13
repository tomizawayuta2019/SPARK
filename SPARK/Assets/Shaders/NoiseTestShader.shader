Shader "Custom/NoiseTestShader" {
	Properties {
		_Color("Color" , Color) = (1, 1, 1, 1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness" , Range(0, 1)) = 0.5
		_Metallic("Metallic" , Range(0, 1)) = 0.0
	}
	SubShader {
		Tags {
		"Queue" = "Transparent"
		"RenderType" = "Transparent"
		}
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha
		#pragma target 3.0

		fixed4 _Color;
		sampler2D _MainTex;
		half _Glossiness;
		half _Metallic;
		float alpha = 0;

		struct Input {
			float2 uv_MainTex;
		};

		float random (fixed2 p) {
			return frac(sin(dot(p, fixed2(12.9898,78.233)))*43758.5453);
		}
		void surf (Input IN, inout SurfaceOutputStandard o) {
			IN.uv_MainTex.x += 0.1 * _Time;
			float c = random(IN.uv_MainTex); 
			o.Albedo = fixed4(c + _Color.r * 2, c + _Color.g * 2, c + _Color.b * 2, 1);
			o.Alpha = _Color.a;
		}
		ENDCG
	}
		Fallback "Transparent/Diffuse"
}
