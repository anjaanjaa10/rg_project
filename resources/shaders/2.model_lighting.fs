#version 330 core
out vec4 FragColor;

in vec2 TexCoords;
in vec3 Normal;
in vec3 FragPos;

uniform vec3 lightPos;

struct Material {
    sampler2D texture_diffuse1;
    sampler2D texture_specular1;
    float shininess;
};

uniform Material material;
uniform vec3 viewPosition;


vec3 CalcPointLight(vec3 lightPosition, vec3 normal, vec3 fragPos, vec3 viewDir)
{
    vec3 lightDir = normalize(lightPosition - fragPos);
    float diff = max(dot(normal, lightDir), 0.0);
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    float distance = length(lightPosition - fragPos);
    float attenuation = 1.0 / (1.0 + 0.09 * distance + 0.032 * distance * distance);
    vec3 ambient = vec3(texture(material.texture_diffuse1, TexCoords)) * 0.1;
    vec3 diffuse = vec3(texture(material.texture_diffuse1, TexCoords)) * 0.6 * diff;
    vec3 specular = vec3(texture(material.texture_specular1, TexCoords)) * 1.0 * spec;
    ambient *= attenuation;
    diffuse *= attenuation;
    specular *= attenuation;
    return (ambient + diffuse + specular);
}

void main()
{
    vec3 normal = normalize(Normal);
    vec3 viewDir = normalize(viewPosition - FragPos);
    vec3 result = CalcPointLight(lightPos, normal, FragPos, viewDir);
    FragColor = vec4(result, 1.0);
}
