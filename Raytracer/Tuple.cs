namespace Raytracer {
    public class Tuple {
        public float x;
        public float y;
        public float z;
        public float w;

        public Tuple(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public bool IsPoint() {
            return Math.Abs(w - 1.0f) < Constants.Epsilon;
        }

        public bool IsVector() {
            return Math.Abs(w - 0.0f) < Constants.Epsilon;
        }

        public float Magnitude() {
            return MathF.Sqrt(x * x + y * y + z * z + w * w);
        }

        public Tuple Normalized() {
            return this / Magnitude();
        }

        public float Dot(Tuple other) {
            return x * other.x + y * other.y + z * other.z + w * other.w;
        }

        public Tuple Cross(Tuple other) {
            return CreateVector(
                y * other.z - z * other.y,
                z * other.x - x * other.z,
                x * other.y - y * other.x
            );
        }

        public static Tuple CreatePoint(float x, float y, float z) {
            return new Tuple(x, y, z, 1.0f);
        }

        public static Tuple CreateVector(float x, float y, float z) {
            return new Tuple(x, y, z, 0.0f);
        }

        public static Tuple operator +(Tuple left, Tuple right) {
            return new Tuple(left.x + right.x, left.y + right.y,
                left.z + right.z, left.w + right.w);
        }

        public static Tuple operator -(Tuple left, Tuple right) {
            return new Tuple(left.x - right.x, left.y - right.y,
                left.z - right.z, left.w - right.w);
        }

        public static Tuple operator -(Tuple tuple) {
            return new Tuple(-tuple.x, -tuple.y, -tuple.z, -tuple.w);
        }

        public static Tuple operator *(Tuple tuple, float scalar) {
            return new Tuple(scalar * tuple.x, scalar * tuple.y,
                scalar * tuple.z, scalar * tuple.w);
        }

        public static Tuple operator *(float scalar, Tuple tuple) {
            return tuple * scalar;
        }

        public static Tuple operator /(Tuple tuple, float scalar) {
            return new Tuple(tuple.x / scalar, tuple.y / scalar,
                tuple.z / scalar, tuple.w / scalar);
        }

        public float this[int i] {
            get {
                switch (i) {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    case 3:
                        return w;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set {
                switch (i) {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    case 3:
                        w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public static bool operator ==(Tuple left, Tuple right) {
            return left.Equals(right);
        }

        public static bool operator !=(Tuple left, Tuple right) {
            return !left.Equals(right);
        }

        public override bool Equals(object? obj) {
            if (obj is Tuple) {
                Tuple other = (Tuple)obj;

                return x == other.x && y == other.y &&
                    z == other.z && w == other.w;
            }

            return base.Equals(obj);
        }

        public override string ToString() {
            return $"{{{x},{y},{z},{w}}}";
        }
    }
}
