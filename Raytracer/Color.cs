namespace Raytracer {
    public class Color : Tuple {
        public Color(float red, float green, float blue) : base(red, green, blue, 0.0f) {

        }

        public float Red { get => x; set => x = value; }
        public float Green { get => y; set => y = value; }
        public float Blue { get => z; set => z = value; }

        public Color Hadamard(Color other) {
            return new Color(Red * other.Red, Green * other.Green, Blue * other.Blue);
        }

        public static Color operator +(Color left, Color right) {
            Tuple result = (Tuple)left + right;
            return new Color(result.x, result.y, result.z);
        }

        public static Color operator -(Color left, Color right) {
            Tuple result = (Tuple)left - right;
            return new Color(result.x, result.y, result.z);
        }

        public static Color operator *(Color tuple, float scalar) {
            Tuple result = (Tuple)tuple * scalar;
            return new Color(result.x, result.y, result.z);
        }
    }
}
