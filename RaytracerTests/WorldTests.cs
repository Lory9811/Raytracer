using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raytracer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Tests {
    [TestClass()]
    public class WorldTests {
        World world;

        [TestInitialize()]
        public void Initialize() {
            world = new World();

            Assert.IsNotNull(world.Light);

            Entity s1 = world.CreateEntity("sphere", "s1");
            s1.SurfaceMaterial.color = new Color(0.8f, 1.0f, 0.6f);
            s1.SurfaceMaterial.diffuse = 0.7f;
            s1.SurfaceMaterial.specular = 0.2f;

            Entity s2 = world.CreateEntity("sphere", "s2");
            s2.Transform = Matrix.Scale(0.5f, 0.5f, 0.5f);
        }

        [TestMethod()]
        public void AddEntityTest() {
            Assert.AreEqual(1, world.FindEntitiesByName("s1").Length);
            Assert.AreEqual(1, world.FindEntitiesByName("s2").Length);
            Assert.AreEqual(0, world.FindEntitiesByName("s3").Length);
        }

        [TestMethod()]
        public void RayWorldIntersectTest() {
            Ray ray = new Ray(Tuple.CreatePoint(0, 0, -5), Tuple.CreateVector(0, 0, 1));

            Intersections intersections = world.Intersect(ray);

            Assert.AreEqual(4, intersections.Count);
            Assert.AreEqual(4, intersections[0].t);
            Assert.AreEqual(4.5, intersections[1].t);
            Assert.AreEqual(5.5, intersections[2].t);
            Assert.AreEqual(6, intersections[3].t);
        }

        [TestMethod()]
        public void ShadingTest() {
            Ray ray = new Ray(Tuple.CreatePoint(0, 0, -5), Tuple.CreateVector(0, 0, 1));
            Entity sphere = world.FindEntitiesByName("s1")[0];
            Intersection intersection = new Intersection(4, sphere);

            Intersection.HitData hitData = intersection.ComputeHitData(ray);
            
            Color color = world.ShadeHit(hitData);
            Assert.AreEqual(0.38066f, color.Red, 0.00001f);
            Assert.AreEqual(0.47583f, color.Green, 0.00001f);
            Assert.AreEqual(0.2855f, color.Blue, 0.00001f);
        }

        [TestMethod()]
        public void InsideShadingTest() {
            world.Light = new PointLight(Tuple.CreatePoint(0, 0.25f, 0), new Color(1, 1, 1));
            Ray ray = new Ray(Tuple.CreatePoint(0, 0, 0), Tuple.CreateVector(0, 0, 1));
            Entity sphere = world.FindEntitiesByName("s2")[0];
            Intersection intersection = new Intersection(0.5f, sphere);

            Intersection.HitData hitData = intersection.ComputeHitData(ray);

            Color color = world.ShadeHit(hitData);
            Assert.AreEqual(0.90498f, color.Red, 0.00001f);
            Assert.AreEqual(0.90498f, color.Green, 0.00001f);
            Assert.AreEqual(0.90498f, color.Blue, 0.00001f);
        }

        [TestMethod()]
        public void ColorAtTest() {
            Ray ray = new Ray(Tuple.CreatePoint(0, 0, -5), Tuple.CreateVector(0, 1, 0));

            Color color = world.ColorAt(ray);

            Assert.AreEqual(0, color.Red, 0.00001f);
            Assert.AreEqual(0, color.Green, 0.00001f);
            Assert.AreEqual(0, color.Blue, 0.00001f);
        }

        [TestMethod()]
        public void ColorAtTest2() {
            Ray ray = new Ray(Tuple.CreatePoint(0, 0, -5), Tuple.CreateVector(0, 0, 1));

            Color color = world.ColorAt(ray);

            Assert.AreEqual(0.38066f, color.Red, 0.00001f);
            Assert.AreEqual(0.47583f, color.Green, 0.00001f);
            Assert.AreEqual(0.2855f, color.Blue, 0.00001f);
        }

        [TestMethod()]
        public void ColorAtTest3() {
            Ray ray = new Ray(Tuple.CreatePoint(0, 0, 0.75f), Tuple.CreateVector(0, 0, -1));

            Entity outer = world.FindEntitiesByName("s1")[0];
            outer.SurfaceMaterial.ambient = 1;
            Entity inner = world.FindEntitiesByName("s2")[0];
            inner.SurfaceMaterial.ambient = 1;

            Color color = world.ColorAt(ray);

            Assert.AreEqual(inner.SurfaceMaterial.color, color);
        }
    }
}