using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raytracer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Tests {
    [TestClass()]
    public class ColorTests {
        private const float Delta = 0.0001f;

        [TestMethod()]
        public void ColorTest() {
            Color color = new Color(-0.5f, 0.4f, 1.7f);

            Assert.AreEqual(-0.5f, color.Red, Delta);
            Assert.AreEqual(0.4f, color.Green, Delta);
            Assert.AreEqual(1.7f, color.Blue, Delta);
        }

        [TestMethod()]
        public void AdditionTest() {
            Color c1 = new Color(0.9f, 0.6f, 0.75f);
            Color c2 = new Color(0.7f, 0.1f, 0.25f);
            Color result = c1 + c2;

            Assert.AreEqual(1.6f, result.Red, Delta);
            Assert.AreEqual(0.7f, result.Green, Delta);
            Assert.AreEqual(1.0f, result.Blue, Delta);
        }

        [TestMethod()]
        public void SubtractionTest() {
            Color c1 = new Color(0.9f, 0.6f, 0.75f);
            Color c2 = new Color(0.7f, 0.1f, 0.25f);
            Color result = c1 - c2;

            Assert.AreEqual(0.2f, result.Red, Delta);
            Assert.AreEqual(0.5f, result.Green, Delta);
            Assert.AreEqual(0.5f, result.Blue, Delta);
        }

        [TestMethod()]
        public void ScalarMultiplicationTest() {
            Color c = new Color(0.2f, 0.3f, 0.4f);
            Color result = c * 2;

            Assert.AreEqual(0.4f, result.Red, Delta);
            Assert.AreEqual(0.6f, result.Green, Delta);
            Assert.AreEqual(0.8f, result.Blue, Delta);
        }

        [TestMethod()]
        public void HadamardProductTest() {
            Color c1 = new Color(1, 0.2f, 0.4f);
            Color c2 = new Color(0.9f, 1, 0.1f);
            Color result = c1.Hadamard(c2);

            Assert.AreEqual(0.9f, result.Red, Delta);
            Assert.AreEqual(0.2f, result.Green, Delta);
            Assert.AreEqual(0.04f, result.Blue, Delta);
        }
    }
}
