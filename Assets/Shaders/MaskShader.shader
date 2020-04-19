Shader "MASK/MaskShader"
{
	SubShader
	{
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry-3" }

		ColorMask 0
		ZWrite on

		CGINCLUDE
#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		UNITY_VERTEX_INPUT_INSTANCE_ID //Insert
	};

	struct v2f
	{
		float4 pos : SV_POSITION;
		UNITY_VERTEX_OUTPUT_STEREO //Insert
	};

	v2f vert(appdata v)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(v); //Insert
		UNITY_INITIALIZE_OUTPUT(v2f, o); //Insert
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert

		o.pos = UnityObjectToClipPos(v.vertex);
		return o;
	}

	half4 frag(v2f i) : SV_Target
	{
		return half4(0,0,0,0);
	}
		ENDCG

		Pass
	{
		Stencil
		{
			Ref 1
			Comp always
			Pass replace
		}

			Cull Front

			CGPROGRAM
#pragma vertex vert
#pragma fragment frag
			ENDCG
	}
	}
}