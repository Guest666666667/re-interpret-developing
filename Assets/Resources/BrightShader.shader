Shader "Unlit/BrightShader"
{
	Properties
	{
		//_MainTex ("Texture", 2D) = "white" {}
		_AlphaScale("Alpha Scale",Range(0,1)) = 1
		_Brightness("Brightness",Range(0,2)) = 0
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_Color("Tint", Color) = (1,1,1,1)
		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
	}
		SubShader
	{
		//Tags { "RenderType"="Opaque" }
		//LOD 100

		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Stencil
	{
		Ref[_Stencil]
		Comp[_StencilComp]
		Pass[_StencilOp]
		ReadMask[_StencilReadMask]
		WriteMask[_StencilWriteMask]
	}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
		// make fog work
#pragma multi_compile_fog

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		UNITY_FOG_COORDS(1)
			float4 vertex : SV_POSITION;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	fixed _Brightness;
	fixed _AlphaScale;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture
		fixed4 col = tex2D(_MainTex, i.uv);
	/*float y = i.uv.y - 0.5;
	float x = i.uv.x - 0.5;
	float dis = sqrt(y*y + x*x);
	float offset = abs(0.08 - 4 * dis%0.04) - 0.04;
	float angle = atan(y / x);
	if (i.uv.x - 0.5 < 0 && i.uv.y - 0.5 < 0)
	{
		angle += UNITY_PI;
	}
	if (i.uv.x - 0.5 < 0 && i.uv.y - 0.5 > 0)
	{
		angle += UNITY_PI;
	}
	if (i.uv.x - 0.5 > 0 && i.uv.y - 0.5 < 0)
	{
		angle += 2 * UNITY_PI;
	}

	//angle = angle < 0 ? angle + 2 * UNITY_PI : angle;
	angle /= (2 * UNITY_PI);
	angle -= (offset / (2 * UNITY_PI*dis)) * 5 * cos(((dis - 0.35) * 4 - 1)*UNITY_PI);//毛笔特效
	float realAngle = _StartAngle + _CurrentAngle;
	bool judge = false;
	if (realAngle < 0 || realAngle > 1)
	{
		judge = true;
		realAngle = realAngle < 0 ? realAngle + 1 : realAngle;
		realAngle = realAngle > 1 ? realAngle - 1 : realAngle;
	}

	if ((!judge && angle<max(_StartAngle, realAngle) && angle>min(_StartAngle, realAngle)) || (judge && (angle > max(_StartAngle, realAngle) || angle < min(_StartAngle, realAngle))))
	{

	}
	else
	{
		col = fixed4(col.rgb, 0);
	}*/
	float y = i.uv.y - 0.5;
	float x = i.uv.x - 0.5;
	float dis = sqrt(y*y + x*x);
	float scale = (_Brightness - dis) / _Brightness;
    col *= scale;
    col = fixed4(col.rgb, 1);
	
	// apply fog
	UNITY_APPLY_FOG(i.fogCoord, col);
	//col = (col.rgb,_Alpha);
	return fixed4(col.rgb, col.a*_AlphaScale);
	}
		ENDCG
	}
	}
}
