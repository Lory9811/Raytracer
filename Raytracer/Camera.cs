using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer {
    public class Camera {
        private int hSize;
        private int vSize;
        private float fov;
        private Matrix transform;

        private float pixelSize;
        private float halfWidth;
        private float halfHeight;

        public int HorizontalSize {
            get {
                return hSize;
            }
        }

        public int VerticalSize {
            get {
                return vSize;
            }
        }

        public float Fov {
            get {
                return fov;
            }
        }

        public Matrix Transform {
            get { 
                return transform; 
            }
            set { 
                transform = value; 
            }
        }

        public float PixelSize {
            get {
                return pixelSize;
            }
        }

        public Camera(int hSize, int vSize, float fov) {
            this.hSize = hSize;
            this.vSize = vSize;
            this.fov = fov;
            transform = Matrix.Eye(4);

            float halfView = MathF.Tan(fov / 2.0f);
            float aspectRatio = (float)hSize / vSize;

            halfWidth = aspectRatio > 1.0f ? halfView : halfView * aspectRatio;
            halfHeight = aspectRatio > 1.0f ? halfView / aspectRatio : halfView;

            pixelSize = (halfWidth * 2.0f) / hSize;
        }

        public Ray RayForPixel(int x, int y) {
            float xOffset = (x + 0.5f) * pixelSize;
            float yOffset = (y + 0.5f) * pixelSize;

            float worldX = halfWidth - xOffset;
            float worldY = halfHeight - yOffset;

            Tuple pixel = transform.Inverse() * Tuple.CreatePoint(worldX, worldY, -1);
            Tuple origin = transform.Inverse() * Tuple.CreatePoint(0, 0, 0);
            Tuple direction = (pixel - origin).Normalized();

            return new Ray(origin, direction);
        }

        public Canvas Render(World world) {
            Canvas image = new Canvas(hSize, vSize);

            Parallel.For(0, vSize, (y) => {
                for (int x = 0; x < hSize; x++) {
                    Ray ray = RayForPixel(x, y);
                    image.SetPixel(x, y, world.ColorAt(ray));
                }
            });

            return image;
        }
    }
}
