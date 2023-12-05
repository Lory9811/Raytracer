using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raytracer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Tests {
    [TestClass()]
    public class SphereTests {
        private const float Delta = 0.0001f;

        [TestMethod()]
        public void NormalAtTest() {
            Sphere sphere = new Sphere(Guid.NewGuid());
            Tuple normal = sphere.NormalAt(Tuple.CreatePoint(1, 0, 0));

            Assert.AreEqual(Tuple.CreateVector(1, 0, 0), normal);
        }

        [TestMethod()]
        public void NormalAtTest1() {
            Sphere sphere = new Sphere(Guid.NewGuid());
            Tuple normal = sphere.NormalAt(Tuple.CreatePoint(0, 1, 0));

            Assert.AreEqual(Tuple.CreateVector(0, 1, 0), normal);
        }

        [TestMethod()]
        public void NormalAtTest2() {
            Sphere sphere = new Sphere(Guid.NewGuid());
            Tuple normal = sphere.NormalAt(Tuple.CreatePoint(0, 0, 1));

            Assert.AreEqual(Tuple.CreateVector(0, 0, 1), normal);
        }

        [TestMethod()]
        public void NormalAtTest3() {
            Sphere sphere = new Sphere(Guid.NewGuid());
            Tuple normal = sphere.NormalAt(Tuple.CreatePoint(MathF.Sqrt(3.0f) / 3.0f, MathF.Sqrt(3.0f) / 3.0f, MathF.Sqrt(3.0f) / 3.0f));
            Tuple expected = Tuple.CreateVector(MathF.Sqrt(3.0f) / 3.0f, MathF.Sqrt(3.0f) / 3.0f, MathF.Sqrt(3.0f) / 3.0f);
            
            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected[i], normal[i], Delta);
                Assert.AreEqual(normal[i], normal.Normalized()[i], Delta);
            }
        }

        [TestMethod()]
        public void NormalAtTest4() {
            Sphere sphere = new Sphere(Guid.NewGuid());
            sphere.Transform = Matrix.Translation(0, 1, 0);
            Tuple normal = sphere.NormalAt(Tuple.CreatePoint(0, 1.70711f, -0.70711f));
            Tuple expected = Tuple.CreateVector(0, 0.70711f, -0.70711f);

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected[i], normal[i], Delta);
            }
        }

        [TestMethod()]
        public void NormalAtTest5() {
            Sphere sphere = new Sphere(Guid.NewGuid());
            sphere.Transform = Matrix.Scale(1, 0.5f, 1) * Matrix.RotationZ(MathF.PI / 5.0f);
            Tuple normal = sphere.NormalAt(Tuple.CreatePoint(0, MathF.Sqrt(2.0f) / 2.0f, -MathF.Sqrt(2.0f) / 2.0f));
            Tuple expected = Tuple.CreateVector(0, 0.97014f, -0.24254f);

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected[i], normal[i], Delta);
            }
        }

        [TestMethod()]
        public void MaterialTest() {
            Sphere sphere = new Sphere(Guid.NewGuid());
            Material newMat = new Material();
            Assert.AreEqual(newMat.color, sphere.SurfaceMaterial.color);
            Assert.AreEqual(newMat.shininess, sphere.SurfaceMaterial.shininess);
            Assert.AreEqual(newMat.specular, sphere.SurfaceMaterial.specular);
            Assert.AreEqual(newMat.ambient, sphere.SurfaceMaterial.ambient);
        }

        [TestMethod()]
        public void MaterialTest2() {
            Sphere sphere = new Sphere(Guid.NewGuid());
            Material newMat = new Material();
            newMat.ambient = 1.0f;
            sphere.SurfaceMaterial = newMat;

            Assert.AreEqual(newMat, sphere.SurfaceMaterial);
        }
    }
}