Shader "Mobile/Bumped Diffuse with Color"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		[NoScaleOffset] _BumpMap("Normalmap", 2D) = "bump" {}
	}

		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 250

		CGPROGRAM
		#pragma surface surf Lambert noforwardadd

		sampler2D _MainTex;
		sampler2D _BumpMap;
		float4 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _Color;
			o.Alpha = c.a;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
		}
		ENDCG
	}

		FallBack "Mobile/Diffuse"
}