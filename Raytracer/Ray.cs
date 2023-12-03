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
        public readonly int entity;

        public Intersection(float t, int entityId) { 
            this.t = t;
            this.entity = entityId;
        }

        public Intersection(float t, Entity entity) {
            this.t = t;
            this.entity = entity.Id;
        }

        public override string ToString() {
            return $"{{t={t},entity={entity}}}";
        }
    }

    public class Intersections {
        private Intersection[] intersections;
        private Intersection[] potentialHits;

        public int Length {
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
