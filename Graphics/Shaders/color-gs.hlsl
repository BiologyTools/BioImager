////////////////////////////////////////////////////////////////////////////////
// Filename: color-vs.hlsl
////////////////////////////////////////////////////////////////////////////////

/////////////
// GLOBALS //
/////////////
cbuffer MinMaxBuffer
{
    float4 rMinMax;
    float4 gMinMax;
    float4 bMinMax;
};

//////////////
// TYPEDEFS //
//////////////
struct v2g
{
	float4 position : SV_POSITION;
    float4 color : COLOR;
};

struct g2f
{
	float4 position : SV_POSITION;
	float4 color : COLOR;
};

///////////////
// FUNCTIONS //
///////////////
float2 FromClipSpace(float4 clipPos)
{
	return clipPos.xy / clipPos.w;
}

float4 ToClipSpace(float2 screenPos, float originalClipPosW)
{
	return float4( screenPos.xy * originalClipPosW, 0, originalClipPosW);
}

////////////////////////////////////////////////////////////////////////////////
// Geometry Shader
////////////////////////////////////////////////////////////////////////////////
[maxvertexcount(6)]
void ColorGeometryShader(point v2g input[1], inout TriangleStream<g2f> triStream)
{
    g2f v;
    float2 screenPos = FromClipSpace(input[0].position);
    float clipPosW = input[0].position.w;
    v.color = input[0].color;
    float s = 0.02;
    v.position = ToClipSpace(screenPos + float2(-s, -s), clipPosW);
    triStream.Append(v);

    v.position = ToClipSpace(screenPos + float2(-s, s), clipPosW);
    triStream.Append(v);

    v.position = ToClipSpace(screenPos + float2(s, s), clipPosW);
    triStream.Append(v);
    triStream.RestartStrip();

    v.position = ToClipSpace(screenPos + float2(s, -s), clipPosW);
    triStream.Append(v);

    v.position = ToClipSpace(screenPos + float2(-s, -s), clipPosW);
    triStream.Append(v);

    v.position = ToClipSpace(screenPos + float2(s, s), clipPosW);
    triStream.Append(v);
    triStream.RestartStrip();
    
}