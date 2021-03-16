void GetLightingInformation_float(float3 ObjPos, out float3 Direction, out float3 Color, out float Attenuation)
{
#ifdef LIGHTWEIGHT_LIGHTING_INCLUDED

	Light light = GetMainLight(GetShadowCoord(GetVertexPositionInputs(ObjPos)));
	Direction = light.direction;
	Attenuation = light.distanceAttenuation;
	Color = light.color;

#else

	Direction = float3(-0.5, 0.5, -0.5);
	Color = float3(1, 1, 1);
	Attenuation = 0.4;

#endif
}