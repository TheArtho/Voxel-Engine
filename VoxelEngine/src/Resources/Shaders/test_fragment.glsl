#version 330 core

in vec3 vPosition;

out vec4 FragColor;

void main()
{
    FragColor = vec4(vPosition.rgb, 1.0);
}
