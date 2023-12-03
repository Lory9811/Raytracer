﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raytracer;

using Color = Raytracer.Color;
using Tuple = Raytracer.Tuple;

namespace RaytracerTests {
    [TestClass]
    public class LightTest {
        [TestMethod]
        public void PointLightTest() {
            Color intensity = new Color(1, 1, 1);
            Tuple position = Tuple.CreatePoint(0, 0, 0);

            PointLight light = new PointLight(position, intensity);

            Assert.AreEqual(position, light.Position);
            Assert.AreEqual(intensity, light.Intensity);
        }
    }
}
