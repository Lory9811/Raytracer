using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Raytracer.Tests {
    [TestClass()]
    public class CanvasTests {
        [TestMethod()]
        public void CanvasTest() {
            Canvas c = new Canvas(10, 20);
            Color white = new Color(0, 0, 0);

            Assert.AreEqual(10, c.Width);
            Assert.AreEqual(20, c.Height);

            for (int y = 0; y < c.Height; y++) {
                for (int x = 0; x < c.Width; x++) {
                    Color pixel = c.GetPixel(x, y);
                    Assert.AreEqual(white, pixel);
                }
            }
        }

        [TestMethod()]
        public void SetPixelTest() {
            Canvas c = new Canvas(10, 20);
            Color red = new Color(1, 0, 0);

            c.SetPixel(2, 3, red);

            Assert.AreEqual(red, c.GetPixel(2, 3));
        }

        [TestMethod()]
        public void PpmOutputTest() {
            Canvas canvas = new Canvas(5, 3);
            Color c1 = new Color(1.5f, 0, 0);
            Color c2 = new Color(0, 0.5f, 0);
            Color c3 = new Color(-0.5f, 0, 1);

            canvas.SetPixel(0, 0, c1);
            canvas.SetPixel(2, 1, c2);
            canvas.SetPixel(4, 2, c3);

            StringReader ppm = new StringReader(canvas.ToPpm());

            Assert.AreEqual("P3", ppm.ReadLine());
            Assert.AreEqual("5 3", ppm.ReadLine());
            Assert.AreEqual("255", ppm.ReadLine());

            Assert.AreEqual("255 0 0 0 0 0 0 0 0 0 0 0 0 0 0", ppm.ReadLine());
            Assert.AreEqual("0 0 0 0 0 0 0 128 0 0 0 0 0 0 0", ppm.ReadLine());
            Assert.AreEqual("0 0 0 0 0 0 0 0 0 0 0 0 0 0 255", ppm.ReadLine());

            Assert.IsNull(ppm.ReadLine());
        }

        [TestMethod]
        public void PpmLineLenghtTest() {
            Canvas canvas = new Canvas(10, 2);
            Color color = new Color(1, 0.8f, 0.6f);
            for (int y = 0; y < canvas.Height; y++) {
                for (int x = 0; x < canvas.Width; x++) {
                    canvas.SetPixel(x, y, color);
                }
            }
            StringReader ppm = new StringReader(canvas.ToPpm());

            Assert.AreEqual("P3", ppm.ReadLine());
            Assert.AreEqual("10 2", ppm.ReadLine());
            Assert.AreEqual("255", ppm.ReadLine());

            Assert.AreEqual("255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204", ppm.ReadLine());
            Assert.AreEqual("153 255 204 153 255 204 153 255 204 153 255 204 153", ppm.ReadLine());
            Assert.AreEqual("255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204", ppm.ReadLine());
            Assert.AreEqual("153 255 204 153 255 204 153 255 204 153 255 204 153", ppm.ReadLine());

            Assert.IsNull(ppm.ReadLine());
        }
    }
}
