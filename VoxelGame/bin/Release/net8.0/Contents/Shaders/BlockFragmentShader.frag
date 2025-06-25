#version 330 core

in vec2 fTexCoords;

uniform sampler2D textureUnit;

out vec4 outColor;

void main()
{
	vec4 color = texture(textureUnit, fTexCoords);
	//vec4 color = vec4(1.0, 0.5, 0.25, 1.0);

	outColor = color;
}