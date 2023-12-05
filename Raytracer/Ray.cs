using Microsoft.VisualBasic;
using System.Collections;

namespace Raytracer {
    public class Ray {
        private Tuple origin;
        private Tuple direction;

        public Ray(Tuple origin, Tuple direction) {
            if (!origin.IsPoint())
                throw new ArgumentException("Origin is not a point");

            if (!direction.IsVector())
                throw new ArgumentException("Direction is not a vector");

            this.origin = origin;
            this.direction = direction;
        }

        public Tuple Origin {
            get { return origin; }
        }

        public Tuple Direction {
            get { return direction; }
        }

        public Tuple Position(float t) {
            return origin + t * direction;
        }

        public Ray Transform(Matrix transform) {
            return new Ray(transform * origin, transform * direction);
        }
    }

    public class Intersection {
        public readonly float t;
        public readonly Entity entity;

        public Intersection(float t, Entity entity) {
            this.t = t;
            this.entity = entity;
        }

        public class HitData {
            public readonly float t;
            public readonly Entity entity;
            public readonly Tuple point;
            public readonly Tuple eye;
            public readonly Tuple normal;
            public readonly bool inside;

            public HitData(float t, Entity entity, Tuple point, Tuple eye, Tuple normal, bool inside) {
                this.t = t;
                this.entity = entity;
                this.point = point;
                this.eye = eye;
                this.normal = normal;
                this.inside = inside;
            }
        }

        public HitData ComputeHitData(Ray ray) {
            Tuple point = ray.Position(t);
            Tuple normal = entity.NormalAt(point);
            Tuple eye = -ray.Direction;
            bool inside = normal.Dot(eye) < 0;

            if (inside)
                normal = -normal;

            return new HitData(t, entity, point, -ray.Direction, normal, inside);
        }

        public override string ToString() {
            return $"{{t={t},entity={entity}}}";
        }
    }

    public class Intersections {
        private Intersection[] intersections;
        private Intersection[] potentialHits;

        public int Count {
            get { return intersections.Length; }
        }

        public Intersections(params Intersection[] intersections) {
            this.intersections = intersections;
            Array.Sort(this.intersections, (x, y) => x.t.CompareTo(y.t));
            potentialHits = intersections.Where(x => x.t > 0).ToArray();
        }

        public Intersection this[int index] {
            get { return intersections[index]; }
        }

        public Intersection? Hit() {
            if (potentialHits.Length == 0) return null;
            return potentialHits[0];
        }
    }
}
