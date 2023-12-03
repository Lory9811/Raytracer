using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer {
    public class Material {
        public Color color;
        public float ambient;
        public float diffuse;
        public float specular;
        public float shininess;

        public Material() {
            color = new Color(1, 1, 1);
            ambient = 0.1f;
            diffuse = 0.9f;
            specular = 0.9f;
            shininess = 200.0f;
        }

        public Color Lighting(PointLight light, Tuple position, Tuple eyeDirection, Tuple normal) {
            Color effectiveColor = color.Hadamard(light.Intensity);
            Tuple surfaceToLight = (light.Position - position).Normalized();
            Color ambient = effectiveColor * this.ambient;
            float lightDotNormal = surfaceToLight.Dot(normal);
            Color diffuse;
            Color specular;

            if (lightDotNormal < 0) {
                diffuse = new Color(0, 0, 0);
                specular = new Color(0, 0, 0);
            } else {
                diffuse = effectiveColor * this.diffuse * lightDotNormal;

                Tuple reflection = (-surfaceToLight).Reflect(normal);
                float reflectDotEye = reflection.Dot(eyeDirection);
                if (reflectDotEye <= 0) {
                    specular = new Color(0, 0, 0);
                } else {
                    float specularFactor = MathF.Pow(reflectDotEye, shininess);
                    specular = light.Intensity * this.specular * specularFactor;
                }
            }

            return ambient + diffuse + specular;
        }
    }
}
