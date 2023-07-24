////////////////////////////////////////////////////////////////////////////////
// Filename: color-vs.hlsl
////////////////////////////////////////////////////////////////////////////////


/////////////
// GLOBALS //
/////////////
cbuffer MatrixBuffer
{
	matrix worldMatrix;
	matrix viewMatrix;
	matrix projectionMatrix;
    float4 rMinMax;
    float4 gMinMax;
    float4 bMinMax;
};


//////////////
// TYPEDEFS //
//////////////
struct VertexInputType
{
    float4 position : POSITION;
    float4 color : COLOR;
};

struct PixelInputType
{
    float4 position : SV_POSITION;
    float4 color : COLOR;
};

////////////////////////////////////////////////////////////////////////////////
// Vertex Shader
////////////////////////////////////////////////////////////////////////////////
PixelInputType ColorVertexShader(VertexInputType input)
{
    PixelInputType output;
    // Store the input color for the pixel shader to use.
    output.color = float4(0, 0, 0, 0);
	// Change the position vector to be 4 units for proper matrix calculations.
    input.position.w = 1.0f;
    if ((input.color.x >= rMinMax.x))
    {
        float r = 1 / (rMinMax.y - rMinMax.x);
        input.color.x *= r;
        input.color.w = (input.color.x * input.color.y * input.color.z) * (1 / rMinMax.w);
    }
    else
    {
        input.color.x = 0;
    }
    if (input.color.y >= gMinMax.x)
    {
        float g = 1 / (gMinMax.y - gMinMax.x);
        input.color.y *= g;
        input.color.w = (input.color.x * input.color.y * input.color.z) * (1 / rMinMax.w);
    }
    else
    {
        input.color.y = 0;
    }
    if (input.color.z >= bMinMax.x)
    {
        float b = 1 / (bMinMax.y - bMinMax.x);
        input.color.z *= b;
        input.color.w = (input.color.x * input.color.y * input.color.z) * (1 / rMinMax.w);
    }
    else
    {
        input.color.z = 0;
    }
    //if (!(input.color.x >= rMinMax.x) && !(input.color.y >= gMinMax.x) && !(input.color.z >= bMinMax.x))
    //    input.color.w = 0.0f;

    input.position.z = input.position.z * rMinMax.z;
    //Calculate the position of the vertex against the world, view, and projection matrices.
    output.position = mul(input.position, worldMatrix);
    output.position = mul(output.position, viewMatrix);
    output.position = mul(output.position, projectionMatrix);
    output.color = input.color;
    
    return output;
}