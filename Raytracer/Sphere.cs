using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer {
    public class Sphere : Entity {
        public Material material;

        public Sphere(int id) : base(id) {
            material = new Material();
        }

        public Intersection[] Intersect(Ray ray) {
            Ray transformedRay = ray.Transform(transform.Inverse());
            Tuple sphereToRay = transformedRay.Origin - Tuple.CreatePoint(0, 0, 0);

            float a = transformedRay.Direction.Dot(transformedRay.Direction);
            float b = 2.0f * transformedRay.Direction.Dot(sphereToRay);
            float c = sphereToRay.Dot(sphereToRay) - 1.0f;

            float discriminant = (b * b) - 4.0f * a * c;

            if (discriminant < 0.0f) {
                return new Intersection[] { };
            }

            return new Intersection[] {
                new Intersection((-b - MathF.Sqrt(discriminant)) / (2.0f * a), this),
                new Intersection((-b + MathF.Sqrt(discriminant)) / (2.0f * a), this),
            };
        }

        public Tuple NormalAt(Tuple point) {
            Tuple localPoint = transform.Inverse() * point;
            Tuple localNormal = localPoint - Tuple.CreatePoint(0, 0, 0);
            Tuple worldNormal = transform.Inverse().T() * localNormal;
            worldNormal.w = 0.0f;
            
            return worldNormal.Normalized();
        }
    }
}
