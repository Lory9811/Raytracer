using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Raytracer.Tests {
    [TestClass]
    public class TupleTests {
        private const float Delta = 0.0001f;

        [TestMethod]
        public void IsPointTest() {
            Tuple a = new Tuple(4.3f, -4.2f, 3.1f, 1.0f);

            Assert.AreEqual(4.3f, a.x, Delta);
            Assert.AreEqual(-4.2f, a.y, Delta);
            Assert.AreEqual(3.1f, a.z, Delta);
            Assert.AreEqual(1.0f, a.w, Delta);

            Assert.IsTrue(a.IsPoint());
            Assert.IsFalse(a.IsVector());
        }

        [TestMethod]
        public void IsVectorTest() {
            Tuple a = new Tuple(4.3f, -4.2f, 3.1f, 0.0f);

            Assert.AreEqual(4.3f, a.x, Delta);
            Assert.AreEqual(-4.2f, a.y, Delta);
            Assert.AreEqual(3.1f, a.z, Delta);
            Assert.AreEqual(0.0f, a.w, Delta);

            Assert.IsFalse(a.IsPoint());
            Assert.IsTrue(a.IsVector());
        }

        [TestMethod]
        public void CreatePointTest() {
            Tuple p = Tuple.CreatePoint(4, -4, 3);

            Assert.AreEqual(4, p.x, Delta);
            Assert.AreEqual(-4, p.y, Delta);
            Assert.AreEqual(3, p.z, Delta);
            Assert.AreEqual(1, p.w, Delta);
        }

        [TestMethod]
        public void CreateVectorTest() {
            Tuple v = Tuple.CreateVector(4, -4, 3);

            Assert.AreEqual(4, v.x, Delta);
            Assert.AreEqual(-4, v.y, Delta);
            Assert.AreEqual(3, v.z, Delta);
            Assert.AreEqual(0, v.w, Delta);
        }

        [TestMethod]
        public void AdditionTest() {
            Tuple a1 = new Tuple(3, -2, 5, 1);
            Tuple a2 = new Tuple(-2, 3, 1, 0);

            Tuple sum = a1 + a2;

            Assert.AreEqual(1, sum.x, Delta);
            Assert.AreEqual(1, sum.y, Delta);
            Assert.AreEqual(6, sum.z, Delta);
            Assert.AreEqual(1, sum.w, Delta);
        }

        static IEnumerable<Tuple[]> SubtractionTestData() {
            return new[] {
                new[] { Tuple.CreatePoint(3, 2, 1), Tuple.CreatePoint(5, 6, 7), Tuple.CreateVector(-2, -4, -6) },
                new[] { Tuple.CreatePoint(3, 2, 1), Tuple.CreateVector(5, 6, 7), Tuple.CreatePoint(-2, -4, -6) },
                new[] { Tuple.CreateVector(3, 2, 1), Tuple.CreateVector(5, 6, 7), Tuple.CreateVector(-2, -4, -6) },
            };
        }

        [DataTestMethod]
        [DynamicData(nameof(SubtractionTestData), DynamicDataSourceType.Method)]
        public void SubtractionTest(Tuple a1, Tuple a2, Tuple expected) {
            Tuple result = a1 - a2;

            Assert.AreEqual(expected.x, result.x, Delta);
            Assert.AreEqual(expected.y, result.y, Delta);
            Assert.AreEqual(expected.z, result.z, Delta);
            Assert.AreEqual(expected.w, result.w, Delta);
        }

        [TestMethod]
        public void NegationTest() {
            Tuple a = new Tuple(1, -2, 3, -4);
            Tuple result = -a;

            Assert.AreEqual(-1, result.x, Delta);
            Assert.AreEqual(2, result.y, Delta);
            Assert.AreEqual(-3, result.z, Delta);
            Assert.AreEqual(4, result.w, Delta);
        }

        static IEnumerable<object[]> ScalarMultiplicationTestData() {
            return new[] {
                new object[] { new Tuple(1, -2, 3, -4), 3.5f, new Tuple(3.5f, -7, 10.5f, -14)}
            };
        }

        [DataTestMethod]
        [DynamicData(nameof(ScalarMultiplicationTestData), DynamicDataSourceType.Method)]
        public void ScalarMultiplicationTest(Tuple a1, float a2, Tuple expected) {
            Tuple result = a1 * a2;

            Assert.AreEqual(expected.x, result.x, Delta);
            Assert.AreEqual(expected.y, result.y, Delta);
            Assert.AreEqual(expected.z, result.z, Delta);
            Assert.AreEqual(expected.w, result.w, Delta);
        }

        [TestMethod]
        public void ScalarDivisionTest() {
            Tuple a = new Tuple(1, -2, 3, -4);
            Tuple result = a / 2;

            Assert.AreEqual(0.5, result.x, Delta);
            Assert.AreEqual(-1, result.y, Delta);
            Assert.AreEqual(1.5, result.z, Delta);
            Assert.AreEqual(-2, result.w, Delta);
        }

        static IEnumerable<object[]> MagnitudeTestData() {
            return new[] {
                new object[] { Tuple.CreateVector(1, 0, 0), 1.0f },
                new object[] { Tuple.CreateVector(0, 1, 0), 1.0f },
                new object[] { Tuple.CreateVector(0, 0, 1), 1.0f },
                new object[] { Tuple.CreateVector(1, 2, 3), (float)Math.Sqrt(14) },
                new object[] { Tuple.CreateVector(-1, -2, -3), (float)Math.Sqrt(14) }
            };
        }

        [DataTestMethod]
        [DynamicData(nameof(MagnitudeTestData), DynamicDataSourceType.Method)]
        public void MagnitudeTest(Tuple tuple, float expected) {
            float l = tuple.Magnitude();

            Assert.AreEqual(expected, l, Delta);
        }

        static IEnumerable<Tuple[]> NormalizationTestData() {
            return new[] {
                new[] { Tuple.CreateVector(4, 0, 0), Tuple.CreateVector(1, 0, 0)},
                new[] { Tuple.CreateVector(1, 2, 3), Tuple.CreateVector(0.26726f, 0.53452f, 0.80178f)}
            };
        }

        [DataTestMethod]
        [DynamicData(nameof(NormalizationTestData), DynamicDataSourceType.Method)]
        public void NormalizationTest(Tuple tuple, Tuple expected) {
            Tuple result = tuple.Normalized();

            Assert.AreEqual(expected.x, result.x, Delta);
            Assert.AreEqual(expected.y, result.y, Delta);
            Assert.AreEqual(expected.z, result.z, Delta);
            Assert.AreEqual(expected.w, result.w, Delta);

            Assert.AreEqual(1.0f, result.Magnitude(), Delta);
        }

        [TestMethod]
        public void DotProductTest() {
            Tuple a = Tuple.CreateVector(1, 2, 3);
            Tuple b = Tuple.CreateVector(2, 3, 4);

            Assert.AreEqual(20.0f, a.Dot(b), Delta);
        }

        static IEnumerable<Tuple[]> CrossProductTestData() {
            return new[] {
                new[] { Tuple.CreateVector(1, 2, 3), Tuple.CreateVector(2, 3, 4), Tuple.CreateVector(-1, 2, -1)},
                new[] { Tuple.CreateVector(2, 3, 4), Tuple.CreateVector(1, 2, 3), Tuple.CreateVector(1, -2, 1)},

            };
        }

        [DataTestMethod]
        [DynamicData(nameof(CrossProductTestData), DynamicDataSourceType.Method)]
        public void CrossProductTest(Tuple a, Tuple b, Tuple expected) {
            Tuple result = a.Cross(b);

            Assert.AreEqual(expected.x, result.x, Delta);
            Assert.AreEqual(expected.y, result.y, Delta);
            Assert.AreEqual(expected.z, result.z, Delta);
            Assert.AreEqual(expected.w, result.w, Delta);
        }
    }
}
