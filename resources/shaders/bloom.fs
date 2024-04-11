#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D screenTexture;

void main() {    
    vec4 texColor = texture(screenTexture, TexCoords);
    FragColor = texColor * vec4(1.3, 1.3, 1.3, 1.0); // Povećavamo osvetljenje za 30%
}