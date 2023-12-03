using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Raytracer.Tests {
    [TestClass()]
    public class RayTests {
        [TestMethod()]
        public void RayTest() {
            Tuple origin = Tuple.CreatePoint(1, 2, 3);
            Tuple direction = Tuple.CreateVector(4, 5, 6);

            Ray ray = new Ray(origin, direction);

            Assert.AreEqual(origin, ray.Origin);
            Assert.AreEqual(direction, ray.Direction);
        }

        [TestMethod()]
        public void PositionTest() {
            Ray ray = new Ray(Tuple.CreatePoint(2, 3, 4), Tuple.CreateVector(1, 0, 0));


            Assert.AreEqual(Tuple.CreatePoint(2, 3, 4), ray.Position(0));
            Assert.AreEqual(Tuple.CreatePoint(3, 3, 4), ray.Position(1));
            Assert.AreEqual(Tuple.CreatePoint(1, 3, 4), ray.Position(-1));
            Assert.AreEqual(Tuple.CreatePoint(4.5f, 3, 4), ray.Position(2.5f));
        }

        [TestMethod()]
        public void SphereIntersectionTest() {
            Ray ray = new Ray(Tuple.CreatePoint(0, 0, -5), Tuple.CreateVector(0, 0, 1));
            Sphere sphere = new Sphere(0);
            
            Intersection[] intersections = sphere.Intersect(ray);

            Assert.AreEqual(2, intersections.Length);
            Assert.AreEqual(4.0f, intersections[0].t);
            Assert.AreEqual(sphere.Id, intersections[0].entity);
            Assert.AreEqual(6.0f, intersections[1].t);
            Assert.AreEqual(sphere.Id, intersections[1].entity);
        }

        [TestMethod()]
        public void SphereIntersectionTest2() {
            Ray ray = new Ray(Tuple.CreatePoint(0, 1, -5), Tuple.CreateVector(0, 0, 1));
            Sphere sphere = new Sphere(0);

            Intersection[] intersections = sphere.Intersect(ray);

            Assert.AreEqual(2, intersections.Length);
            Assert.AreEqual(5.0f, intersections[0].t);
            Assert.AreEqual(sphere.Id, intersections[0].entity);
            Assert.AreEqual(5.0f, intersections[1].t);
            Assert.AreEqual(sphere.Id, intersections[1].entity);
        }

        [TestMethod()]
        public void SphereIntersectionTest3() {
            Ray ray = new Ray(Tuple.CreatePoint(0, 2, -5), Tuple.CreateVector(0, 0, 1));
            Sphere sphere = new Sphere(0);

            Intersection[] intersections = sphere.Intersect(ray);

            Assert.AreEqual(sphere.Id, intersections.Length);
        }

        [TestMethod()]
        public void SphereIntersectionTest4() {
            Ray ray = new Ray(Tuple.CreatePoint(0, 0, 0), Tuple.CreateVector(0, 0, 1));
            Sphere sphere = new Sphere(0);

            Intersection[] intersections = sphere.Intersect(ray);

            Assert.AreEqual(2, intersections.Length);
            Assert.AreEqual(-1.0f, intersections[0].t);
            Assert.AreEqual(sphere.Id, intersections[0].entity);
            Assert.AreEqual(1.0f, intersections[1].t);
            Assert.AreEqual(sphere.Id, intersections[1].entity);
        }

        [TestMethod()]
        public void SphereIntersectionTest5() {
            Ray ray = new Ray(Tuple.CreatePoint(0, 0, 5), Tuple.CreateVector(0, 0, 1));
            Sphere sphere = new Sphere(0);

            Intersection[] intersections = sphere.Intersect(ray);

            Assert.AreEqual(2, intersections.Length);
            Assert.AreEqual(-6.0f, intersections[0].t);
            Assert.AreEqual(sphere.Id, intersections[0].entity);
            Assert.AreEqual(-4.0f, intersections[1].t);
            Assert.AreEqual(sphere.Id, intersections[1].entity);
        }

        [TestMethod()]
        public void IntersectionsTest() {
            Sphere sphere = new Sphere(0);

            Intersection i1 = new Intersection(1, sphere);
            Intersection i2 = new Intersection(2, sphere);

            Intersections intersections = new Intersections(i1, i2);

            Assert.AreEqual(2, intersections.Length);
            Assert.AreEqual(1, intersections[0].t);
            Assert.AreEqual(sphere.Id, intersections[0].entity);
            Assert.AreEqual(2, intersections[1].t);
            Assert.AreEqual(sphere.Id, intersections[1].entity);
        }

        [TestMethod()]
        public void HitsTest() {
            Sphere sphere = new Sphere(0);

            Intersection i1 = new Intersection(1, sphere);
            Intersection i2 = new Intersection(2, sphere);
            Intersections intersections = new Intersections(i2, i1);

            Intersection? hit = intersections.Hit();

            Assert.AreEqual(i1, hit);
        }

        [TestMethod()]
        public void HitsTest2() {
            Sphere sphere = new Sphere(0);

            Intersection i1 = new Intersection(-1, sphere);
            Intersection i2 = new Intersection(1, sphere);
            Intersections intersections = new Intersections(i2, i1);

            Intersection? hit = intersections.Hit();

            Assert.AreEqual(i2, hit);
        }

        [TestMethod()]
        public void HitsTest3() {
            Sphere sphere = new Sphere(0);

            Intersection i1 = new Intersection(-2, sphere);
            Intersection i2 = new Intersection(-1, sphere);
            Intersections intersections = new Intersections(i2, i1);

            Intersection? hit = intersections.Hit();

            Assert.IsNull(hit);
        }

        [TestMethod()]
        public void HitsTest4() {
            Sphere sphere = new Sphere(0);

            Intersection i1 = new Intersection(5, sphere);
            Intersection i2 = new Intersection(7, sphere);
            Intersection i3 = new Intersection(-3, sphere);
            Intersection i4 = new Intersection(2, sphere);

            Intersections intersections = new Intersections(i1, i2, i3, i4);
            Intersection? hit = intersections.Hit();

            Assert.AreEqual(i4, hit);
        }

        [TestMethod()]
        public void RayTransformTest() {
            Ray ray = new Ray(Tuple.CreatePoint(1, 2, 3), Tuple.CreateVector(0, 1, 0));
            Matrix transform = Matrix.Translation(3, 4, 5);

            Ray ray2 = ray.Transform(transform);

            Assert.AreEqual(Tuple.CreatePoint(4, 6, 8), ray2.Origin);
            Assert.AreEqual(Tuple.CreateVector(0, 1, 0), ray2.Direction);
        }

        [TestMethod()]
        public void SphereTransformTest() {
            Sphere sphere = new Sphere(0);

            Assert.AreEqual(sphere.Transform, Matrix.Eye(4));
        }

        [TestMethod()]
        public void SphereTransformTest1() {
            Sphere sphere = new Sphere(0);
            Matrix transform = Matrix.Translation(2, 3, 4);

            sphere.Transform = transform;

            Assert.AreEqual(transform, sphere.Transform);
        }

        [TestMethod()]
        public void SphereTransformRayIntersectionTest() {
            Ray ray = new Ray(Tuple.CreatePoint(0, 0, -5), Tuple.CreateVector(0, 0, 1));
            Sphere sphere = new Sphere(0);
            sphere.Transform = Matrix.Scale(2, 2, 2);
            Intersection[] intersections = sphere.Intersect(ray);

            Assert.AreEqual(2, intersections.Length);
            Assert.AreEqual(3, intersections[0].t);
            Assert.AreEqual(7, intersections[1].t);
        }

        [TestMethod()]
        public void SphereTransformRayIntersectionTest1() {
            Ray ray = new Ray(Tuple.CreatePoint(0, 0, -5), Tuple.CreateVector(0, 0, 1));
            Sphere sphere = new Sphere(0);
            sphere.Transform = Matrix.Translation(5, 0, 0);
            Intersection[] intersections = sphere.Intersect(ray);

            Assert.AreEqual(0, intersections.Length);
        }
    }
}
