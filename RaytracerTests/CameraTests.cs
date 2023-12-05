using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raytracer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Tests {
    [TestClass()]
    public class CameraTests {
        [TestMethod()]
        public void CameraTest() {
            Camera camera = new Camera(160, 120, MathF.PI / 2);

            Assert.AreEqual(160, camera.HorizontalSize);
            Assert.AreEqual(120, camera.VerticalSize);
            Assert.AreEqual(MathF.PI / 2, camera.Fov, 0.00001f);

            Matrix expected = Matrix.Eye(4);
            for (int i = 0; i < expected.order; i++) {
                for (int j = 0; j < expected.order; j++) {
                    Assert.AreEqual(expected[i, j], camera.Transform[i, j], 0.00001f);
                }
            }
        }

        [TestMethod()]
        public void PixelSizeTest() {
            Camera camera = new Camera(200, 125, MathF.PI / 2);

            Assert.AreEqual(0.01f, camera.PixelSize);
        }

        [TestMethod()]
        public void PixelSizeTest2() {
            Camera camera = new Camera(125, 200, MathF.PI / 2);

            Assert.AreEqual(0.01f, camera.PixelSize);
        }

        [TestMethod()]
        public void RayForPixelTest() {
            Camera camera = new Camera(201, 101, MathF.PI / 2);

            Ray ray = camera.RayForPixel(100, 50);

            Tuple expectedOrigin = Tuple.CreatePoint(0, 0, 0);
            Tuple expectedDirection = Tuple.CreateVector(0, 0, -1);

            for (int i = 0; i < 4;  i++) {
                Assert.AreEqual(expectedOrigin[i], ray.Origin[i], 0.00001f);
                Assert.AreEqual(expectedDirection[i], ray.Direction[i], 0.00001f);
            } 
        }

        [TestMethod()]
        public void RayForPixelTest2() {
            Camera camera = new Camera(201, 101, MathF.PI / 2);

            Ray ray = camera.RayForPixel(0, 0);

            Tuple expectedOrigin = Tuple.CreatePoint(0, 0, 0);
            Tuple expectedDirection = Tuple.CreateVector(0.66519f, 0.33259f, -0.66851f);

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expectedOrigin[i], ray.Origin[i], 0.00001f);
                Assert.AreEqual(expectedDirection[i], ray.Direction[i], 0.00001f);
            }
        }

        [TestMethod()]
        public void RayForPixelTest3() {
            Camera camera = new Camera(201, 101, MathF.PI / 2);
            camera.Transform = Matrix.RotationY(MathF.PI / 4) * Matrix.Translation(0, -2, 5);
            Ray ray = camera.RayForPixel(100, 50);

            Tuple expectedOrigin = Tuple.CreatePoint(0, 2, -5);
            Tuple expectedDirection = Tuple.CreateVector(MathF.Sqrt(2) / 2, 0, -MathF.Sqrt(2) / 2);

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expectedOrigin[i], ray.Origin[i], 0.00001f);
                Assert.AreEqual(expectedDirection[i], ray.Direction[i], 0.00001f);
            }
        }

        [TestMethod()]
        public void RenderWorldTest() {
            World world = new World();

            Entity s1 = world.CreateEntity("sphere", "s1");
            s1.SurfaceMaterial.color = new Color(0.8f, 1.0f, 0.6f);
            s1.SurfaceMaterial.diffuse = 0.7f;
            s1.SurfaceMaterial.specular = 0.2f;

            Entity s2 = world.CreateEntity("sphere", "s2");
            s2.Transform = Matrix.Scale(0.5f, 0.5f, 0.5f);

            Camera camera = new Camera(11, 11, MathF.PI / 2);
            Tuple from = Tuple.CreatePoint(0, 0, -5);
            Tuple to = Tuple.CreatePoint(0, 0, 0);
            Tuple up = Tuple.CreatePoint(0, 1, 0);

            camera.Transform = Matrix.ViewTransform(from, to, up);

            Canvas image = camera.Render(world);

            Color expectedColor = new Color(0.38066f, 0.47583f, 0.2855f);

            Assert.AreEqual(expectedColor.Red, image.GetPixel(5, 5).Red, 0.00001f);
            Assert.AreEqual(expectedColor.Green, image.GetPixel(5, 5).Green, 0.00001f);
            Assert.AreEqual(expectedColor.Blue, image.GetPixel(5, 5).Blue, 0.00001f);
        }
    }
}