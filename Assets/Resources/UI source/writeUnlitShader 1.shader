Shader "Unlit/WriteUnlitShader 1"
{
	Properties
	{
		//_MainTex ("Texture", 2D) = "white" {}
		_AlphaScale("Alpha Scale",Range(0,1)) = 1
		_StartAngle("Start Angle", Range(0,1)) = 0
		_CurrentAngle("Current Angle", Range(-1,1)) = 0
		_MaxDis("Max Distance", Range(0,1)) = 0.12//最近样条线点的最远距离
		_UnitOffset("Unit Offset", Range(0,1)) = 0.01//单个像素的偏移量

		_StartCol("Start Color", Range(0,1)) = 1
		_EndCol("End Color", Range(0,1)) = 0

		_SplineTex("Spline Texture", 2D) = "white" {}//样条线图片
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
	sampler2D _SplineTex;
	float4 _MainTex_ST;
	int _Radius;
	fixed _StartAngle;
	fixed _CurrentAngle;
	fixed _AlphaScale;
	fixed _MaxDis;
	fixed _UnitOffset;

	fixed _StartCol;
	fixed _EndCol;


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
		bool judge = false;
	float curPro = 0;
	int t = ceil((_MaxDis / _UnitOffset) - 0.001);
	[unroll(18)]
	for (int k = 0; k < t; k++)
	{
		for (int j = 0; j < 4 * k + 4; j++)
		{
			//col += tex2D(_MainTex, i.uv + float2(k*_Offset*1.0, j*_Offset*1.0));
			float unitAngle = 2 * UNITY_PI / (4 * k + 4);
			fixed4 tmp = tex2D(_SplineTex, i.uv + float2(cos(j*unitAngle)*(k + 1)*_UnitOffset, sin(j*unitAngle)*(k + 1)*_UnitOffset));
			if (tmp.a > 0)
			{
				judge = true;
				curPro = (_StartCol - tmp.r) / (_StartCol - _EndCol);
				curPro -= cos(k*UNITY_PI / 12)*0.05;
				curPro += (abs(8 - 4 * (k % 4)) - 4)*0.01;
				curPro = curPro < 0 ? 0 : curPro;
				curPro = curPro > 1 ? 1 : curPro;
				break;
			}
		}
		if (judge)
		{
			break;
		}
	}
	if (curPro > min(_StartAngle, _StartAngle + _CurrentAngle) && curPro < max(_StartAngle, _StartAngle + _CurrentAngle))
	{

	}
	else
	{
		col = fixed4(col.rgb, 0);
	}




	//float y = i.uv.y - 0.5;
	//float x = i.uv.x - 0.5;
	//float dis = sqrt(y*y + x*x);
	//float offset = abs(0.08 - 4 * dis%0.04) - 0.04;
	//float angle = atan(y / x);
	//if (i.uv.x - 0.5 < 0 && i.uv.y - 0.5 < 0)
	//{
	//	angle += UNITY_PI;
	//}
	//if (i.uv.x - 0.5 < 0 && i.uv.y - 0.5 > 0)
	//{
	//	angle += UNITY_PI;
	//}
	//if (i.uv.x - 0.5 > 0 && i.uv.y - 0.5 < 0)
	//{
	//	angle += 2 * UNITY_PI;
	//}
	//
	////angle = angle < 0 ? angle + 2 * UNITY_PI : angle;
	//angle /= (2 * UNITY_PI);
	//angle -= (offset / (2 * UNITY_PI*dis)) * 5 * cos(((dis - 0.35) * 4 - 1)*UNITY_PI);//毛笔特效
	//float realAngle = _StartAngle + _CurrentAngle;
	//bool judge = false;
	//if (realAngle < 0 || realAngle > 1)
	//{
	//	judge = true;
	//	realAngle = realAngle < 0 ? realAngle + 1 : realAngle;
	//	realAngle = realAngle > 1 ? realAngle - 1 : realAngle;
	//}
	//
	//if ((!judge && angle<max(_StartAngle, realAngle) && angle>min(_StartAngle, realAngle)) || (judge&&(angle > max(_StartAngle, realAngle) || angle < min(_StartAngle, realAngle))))
	//{
	//
	//}
	//else
	//{
	//	col = fixed4(col.rgb, 0);
	//}
	// apply fog
	UNITY_APPLY_FOG(i.fogCoord, col);
	//col = (col.rgb,_Alpha);
	return fixed4(col.rgb, col.a*_AlphaScale);
	}
		ENDCG
	}
	}
}

