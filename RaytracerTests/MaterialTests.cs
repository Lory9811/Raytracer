using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raytracer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Tests {
    [TestClass()]
    public class MaterialTests {
        private const float Delta = 0.0001f;
        private Material material;
        private Tuple position;

        [TestMethod()]
        public void MaterialTest() {
            Material material = new Material();

            Assert.AreEqual(new Color(1, 1, 1), material.color);
            Assert.AreEqual(0.1f, material.ambient);
            Assert.AreEqual(0.9f, material.diffuse);
            Assert.AreEqual(0.9f, material.specular);
            Assert.AreEqual(200.0f, material.shininess);
        }

        [TestInitialize()]
        public void TestInitialize() {
            material = new Material();
            position = Tuple.CreatePoint(0, 0, 0);
        }

        [TestMethod()]
        public void MaterialLightingTest() {
            Tuple eyeDirection = Tuple.CreateVector(0, 0, -1);
            Tuple normal = Tuple.CreateVector(0, 0, -1);
            PointLight light = new PointLight(Tuple.CreatePoint(0, 0, -10), new Color(1, 1, 1));

            Color result = material.Lighting(light, position, eyeDirection, normal);

            Assert.AreEqual(1.9f, result.Red, Delta);
            Assert.AreEqual(1.9f, result.Green, Delta);
            Assert.AreEqual(1.9f, result.Blue, Delta);
        }

        [TestMethod()]
        public void MaterialLightingTest2() {
            Tuple eyeDirection = Tuple.CreateVector(0, MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2);
            Tuple normal = Tuple.CreateVector(0, 0, -1);
            PointLight light = new PointLight(Tuple.CreatePoint(0, 0, -10), new Color(1, 1, 1));

            Color result = material.Lighting(light, position, eyeDirection, normal);

            Assert.AreEqual(1.0f, result.Red, Delta);
            Assert.AreEqual(1.0f, result.Green, Delta);
            Assert.AreEqual(1.0f, result.Blue, Delta);
        }

        [TestMethod()]
        public void MaterialLightingTest3() {
            Tuple eyeDirection = Tuple.CreateVector(0, -MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2);
            Tuple normal = Tuple.CreateVector(0, 0, -1);
            PointLight light = new PointLight(Tuple.CreatePoint(0, 10, -10), new Color(1, 1, 1));

            Color result = material.Lighting(light, position, eyeDirection, normal);

            Assert.AreEqual(1.6364f, result.Red, Delta);
            Assert.AreEqual(1.6364f, result.Green, Delta);
            Assert.AreEqual(1.6364f, result.Blue, Delta);
        }

        [TestMethod()]
        public void MaterialLightingTest4() {
            Tuple eyeDirection = Tuple.CreateVector(0, 0, -1);
            Tuple normal = Tuple.CreateVector(0, 0, -1);
            PointLight light = new PointLight(Tuple.CreatePoint(0, 0, 10), new Color(1, 1, 1));

            Color result = material.Lighting(light, position, eyeDirection, normal);

            Assert.AreEqual(0.1f, result.Red, Delta);
            Assert.AreEqual(0.1f, result.Green, Delta);
            Assert.AreEqual(0.1f, result.Blue, Delta);
        }
    }
}