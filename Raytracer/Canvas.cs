using System.Text;

namespace Raytracer {
    public class Canvas {
        private readonly Color defaultColor = new Color(0, 0, 0);

        private int width;
        private int height;
        private Color[,] data;

        public Canvas(int width, int height) {
            this.width = width;
            this.height = height;
            data = new Color[width, height];

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    SetPixel(x, y, defaultColor);
                }
            }
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public Color GetPixel(int x, int y) {
            return data[x, y];
        }

        public void SetPixel(int x, int y, Color color) {
            if (x < 0 || y < 0 || x >= width || y >= height) return;

            data[x, y] = color;
        }

        public string ToPpm() {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("P3"); // Magic number
            sb.AppendLine($"{width} {height}");
            sb.AppendLine("255"); // Color Depth

            for (int y = 0; y < height; y++) {
                StringBuilder lineBuilder = new StringBuilder();
                for (int x = 0; x < width; x++) {
                    Color pixel = GetPixel(x, y) * 255;
                    string red = Math.Clamp((int)(pixel.Red + 0.5f), 0, 255).ToString();
                    string green = Math.Clamp((int)(pixel.Green + 0.5f), 0, 255).ToString();
                    string blue = Math.Clamp((int)(pixel.Blue + 0.5f), 0, 255).ToString();


                    if (lineBuilder.Length + red.Length > 70) {
                        sb.AppendLine(lineBuilder.ToString().TrimEnd());
                        lineBuilder.Clear();
                        lineBuilder.Append(red + " ");
                    } else {
                        lineBuilder.Append(red + " ");
                    }

                    if (lineBuilder.Length + green.Length > 70) {
                        sb.AppendLine(lineBuilder.ToString().TrimEnd());
                        lineBuilder.Clear();
                        lineBuilder.Append(green + " ");
                    } else {
                        lineBuilder.Append(green + " ");
                    }

                    if (lineBuilder.Length + blue.Length > 70) {
                        sb.AppendLine(lineBuilder.ToString().TrimEnd());
                        lineBuilder.Clear();
                        lineBuilder.Append(blue + " ");
                    } else {
                        lineBuilder.Append(blue + " ");
                    }
                }
                sb.AppendLine(lineBuilder.ToString().TrimEnd());
            }

            return sb.ToString();
        }

        public void SavePpm(string filename) {

        }
    }
}
