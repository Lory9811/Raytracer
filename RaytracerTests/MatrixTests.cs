using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Raytracer.Tests {
    [TestClass()]
    public class MatrixTests {
        private const float Delta = 0.0001f;

        [TestMethod()]
        public void Matrix4x4Test() {
            Matrix matrix = new Matrix(4, new float[,]{
                { 1, 2, 3, 4 },
                { 5.5f, 6.5f, 7.5f, 8.5f },
                { 9, 10, 11, 12 },
                { 13.5f, 14.5f, 15.5f, 16.5f },
            });

            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(4, matrix[0, 3]);
            Assert.AreEqual(5.5f, matrix[1, 0]);
            Assert.AreEqual(7.5f, matrix[1, 2]);
            Assert.AreEqual(11, matrix[2, 2]);
            Assert.AreEqual(13.5f, matrix[3, 0]);
            Assert.AreEqual(15.5f, matrix[3, 2]);
        }

        [TestMethod()]
        public void Matrix3x3Test() {
            Matrix matrix = new Matrix(3, new float[,]{
                { -3, 5, 0 },
                { 1, -2, -7 },
                { 0, 1, 1 },
            });

            Assert.AreEqual(-3, matrix[0, 0]);
            Assert.AreEqual(-2, matrix[1, 1]);
            Assert.AreEqual(1, matrix[2, 2]);
        }

        [TestMethod()]
        public void Matrix2x2Test() {
            Matrix matrix = new Matrix(2, new float[,]{
                { -3, 5 },
                { 1, -2 },
            });

            Assert.AreEqual(-3, matrix[0, 0]);
            Assert.AreEqual(5, matrix[0, 1]);
            Assert.AreEqual(1, matrix[1, 0]);
            Assert.AreEqual(-2, matrix[1, 1]);
        }

        [TestMethod()]
        public void Equals4x4Test() {
            Matrix matrixA = new Matrix(4, new float[,] {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 8, 7, 6 },
                { 5, 4, 3, 2 },
            });

            Matrix matrixB = new Matrix(4, new float[,] {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 8, 7, 6 },
                { 5, 4, 3, 2 },
            });

            Assert.IsTrue(matrixA == matrixB);
        }

        [TestMethod()]
        public void NotEquals4x4Test() {
            Matrix matrixA = new Matrix(4, new float[,] {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 8, 7, 6 },
                { 5, 4, 3, 2 },
            });

            Matrix matrixB = new Matrix(4, new float[,] {
                { 2, 3, 4, 5 },
                { 6, 7, 8, 9 },
                { 8, 7, 6, 5 },
                { 4, 3, 2, 1 },
            });

            Assert.IsTrue(matrixA != matrixB);
        }

        [TestMethod()]
        public void Matrix4x4MultiplicationTest() {
            Matrix matrixA = new Matrix(4, new float[,] {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 8, 7, 6 },
                { 5, 4, 3, 2 },
            });
            Matrix matrixB = new Matrix(4, new float[,] {
                { -2, 1, 2, 3 },
                { 3, 2, 1, -1 },
                { 4, 3, 6, 5 },
                { 1, 2, 7, 8 },
            });

            Matrix expected = new Matrix(4, new float[,] {
                { 20, 22, 50, 48 },
                { 44, 54, 114, 108 },
                { 40, 58, 110, 102 },
                { 16, 26, 46, 42 },
            });

            Matrix result = matrixA * matrixB;

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void MatrixTupleMultiplicationTest() {
            Matrix matrix = new Matrix(4, new float[,] {
                { 1, 2, 3, 4 },
                { 2, 4, 4, 2},
                { 8, 6, 4, 1 },
                { 0, 0, 0, 1 },
            });

            Tuple tuple = new Tuple(1, 2, 3, 1);
        }

        [TestMethod()]
        public void MatrixEyeMultiplicationTest() {
            Matrix matrixA = new Matrix(4, new float[,] {
                { 0, 1, 2, 4 },
                { 1, 2, 4, 8 },
                { 2, 4, 8, 16 },
                { 4, 8, 16 , 32 },
            });

            Assert.AreEqual(matrixA, matrixA * Matrix.Eye(4));
        }

        [TestMethod()]
        public void EyeTupleMultiplicationTest() {
            Tuple tuple = new Tuple(1, 2, 3, 4);

            Assert.AreEqual(tuple, Matrix.Eye(4) * tuple);
        }

        [TestMethod()]
        public void TransposeTest() {
            Matrix matrix = new Matrix(4, new float[,] {
                { 0, 9, 3, 0 },
                { 9, 8, 0, 8 },
                { 1, 8, 5, 3 },
                { 0, 0, 5, 8 },
            });

            Matrix expected = new Matrix(4, new float[,] {
                { 0, 9, 1, 0 },
                { 9, 8, 8, 0 },
                { 3, 0, 5, 5 },
                { 0, 8, 3, 8 },
            });

            Assert.AreEqual(expected, matrix.T());
        }

        [TestMethod()]
        public void TransposeEyeTest() {
            Assert.AreEqual(Matrix.Eye(4), Matrix.Eye(4).T());
        }

        [TestMethod()]
        public void Determinant2x2Test() {
            Matrix matrix = new Matrix(2, new float[,] {
                { 1, 5 },
                { -3, 2 },
            });

            Assert.AreEqual(17, matrix.Det());
        }

        [TestMethod()]
        public void Submatrix3x3Test() {
            Matrix matrix = new Matrix(3, new float[,] {
                { 1, 5, 0 },
                { -3, 2, 7 },
                { 0, 6, -3 },
            });

            Matrix expected = new Matrix(2, new float[,] {
                { -3, 2},
                { 0, 6 },
            });

            Assert.AreEqual(expected, matrix.Submatrix(0, 2));
        }

        [TestMethod()]
        public void Submatrix4x4Test() {
            Matrix matrix = new Matrix(4, new float[,] {
                { -6, 1, 1, 6 },
                { -8, 5, 8, 6 },
                { -1, 0, 8, 2 },
                { -7, 1, -1, 1 },
            });

            Matrix expected = new Matrix(3, new float[,] {
                { -6, 1, 6 },
                { -8, 8, 6 },
                { -7, -1, 1 },
            });

            Assert.AreEqual(expected, matrix.Submatrix(2, 1));
        }

        [TestMethod()]
        public void MinorTest() {
            Matrix matrixA = new Matrix(3, new float[,] {
                { 3, 5, 0 },
                { 2, -1, -7 },
                { 6, -1, 5 },
            });
            Matrix matrixB = matrixA.Submatrix(1, 0);

            Assert.AreEqual(25, matrixB.Det());
            Assert.AreEqual(25, matrixA.Minor(1, 0));
        }

        [TestMethod()]
        public void CofactorTest() {
            Matrix matrixA = new Matrix(3, new float[,] {
                { 3, 5, 0 },
                { 2, -1, -7 },
                { 6, -1, 5 },
            });

            
            Assert.AreEqual(-12, matrixA.Minor(0, 0));
            Assert.AreEqual(-12, matrixA.Cofactor(0, 0));
            Assert.AreEqual(25, matrixA.Minor(1, 0));
            Assert.AreEqual(-25, matrixA.Cofactor(1, 0));
        }

        [TestMethod()]
        public void Det3x3Test() {
            Matrix matrix = new Matrix(3, new float[,] {
                { 1, 2, 6 },
                { -5, 8, -4 },
                { 2, 6, 4 },
            });

            Assert.AreEqual(56, matrix.Cofactor(0, 0));
            Assert.AreEqual(12, matrix.Cofactor(0, 1));
            Assert.AreEqual(-46, matrix.Cofactor(0, 2));
            Assert.AreEqual(-196, matrix.Det());
        }

        [TestMethod()]
        public void Det4x4Test() {
            Matrix matrix = new Matrix(4, new float[,] {
                { -2, -8, 3, 5 },
                { -3, 1, 7, 3 },
                { 1, 2, -9, 6 },
                { -6, 7, 7, -9 },
            });

            Assert.AreEqual(690, matrix.Cofactor(0, 0));
            Assert.AreEqual(447, matrix.Cofactor(0, 1));
            Assert.AreEqual(210, matrix.Cofactor(0, 2));
            Assert.AreEqual(51, matrix.Cofactor(0, 3));
            Assert.AreEqual(-4071, matrix.Det());
        }

        [TestMethod]
        public void InversionTest() {
            Matrix matrixA = new Matrix(4, new float[,] {
                { -5, 2, 6, -8 },
                { 1, -5, 1, 8 },
                { 7, 7, -6, -7 },
                { 1, -3, 7, 4 },
            });
            Matrix matrixB = matrixA.Inverse();

            Matrix expected = new Matrix(4, new float[,] {
                { 0.21805f, 0.45113f, 0.24060f, -0.04511f },
                { -0.80827f, -1.45677f, -0.44361f, 0.52068f },
                { -0.07895f, -0.22368f, -0.05263f, 0.19737f },
                { -0.52256f, -0.81391f, -0.30075f, 0.30639f },
            });

            Assert.AreEqual(532, matrixA.Det());
            Assert.AreEqual(-160, matrixA.Cofactor(2, 3));
            Assert.AreEqual(-160.0f / 532.0f, matrixB[3, 2]);
            Assert.AreEqual(105, matrixA.Cofactor(3, 2));
            Assert.AreEqual(105.0f / 532.0f, matrixB[2, 3]);

            for (int i = 0; i < expected.order; i++) {
                for (int j = 0; j < expected.order; j++) {
                    Assert.AreEqual(expected[i, j], matrixB[i, j], Delta);
                }
            }
        }

        [TestMethod]
        public void InversionTest2() {
            Matrix matrixA = new Matrix(4, new float[,] {
                { 8, -5, 9, 2 },
                { 7, 5, 6, 1 },
                { -6, 0, 9, 6 },
                { -3, 0, -9, -4 },
            });
            Matrix matrixB = matrixA.Inverse();

            Matrix expected = new Matrix(4, new float[,] {
                { -0.15385f, -0.15385f, -0.28205f, -0.53846f },
                { -0.07692f, 0.12308f, 0.02564f, 0.03077f },
                { 0.35897f, 0.35897f, 0.43590f, 0.92308f },
                { -0.69231f, -0.69231f, -0.76923f, -1.92308f },
            });

            for (int i = 0; i < expected.order; i++) {
                for (int j = 0; j < expected.order; j++) {
                    Assert.AreEqual(expected[i, j], matrixB[i, j], Delta);
                }
            }
        }

        [TestMethod]
        public void InversionTest3() {
            Matrix matrixA = new Matrix(4, new float[,] {
                { 9, 3, 0, 9 },
                { -5, -2, -6, -3 },
                { -4, 9, 6, 4 },
                { -7, 6, 6, 2 },
            });
            Matrix matrixB = matrixA.Inverse();

            Matrix expected = new Matrix(4, new float[,] {
                { -0.04074f, -0.07778f, 0.14444f, -0.22222f },
                { -0.07778f, 0.03333f, 0.36667f, -0.33333f },
                { -0.02901f, -0.14630f, -0.10926f, 0.12963f },
                { 0.17778f, 0.06667f, -0.26667f, 0.33333f },
            });

            for (int i = 0; i < expected.order; i++) {
                for (int j = 0; j < expected.order; j++) {
                    Assert.AreEqual(expected[i, j], matrixB[i, j], Delta);
                }
            }
        }

        [TestMethod]
        public void InversionMultiplicationTest4() {
            Matrix matrixA = new Matrix(4, new float[,] {
                { 3, -9, 7, 3 },
                { 3, -8, 2, -9 },
                { -4, 4, 4, 1 },
                { -6, 5, -1, 1 },
            });
            Matrix matrixB = new Matrix(4, new float[,] {
                { 8, 2, 2, 2 },
                { 3, -1, 7, 0 },
                { 7, 0, 5, 4 },
                { 6, -2, 0, 5 },
            });

            Matrix matrixC = matrixA * matrixB;

            Matrix result = matrixC * matrixB.Inverse();

            for (int i = 0; i < matrixA.order; i++) {
                for (int j = 0; j < matrixA.order; j++) {
                    Assert.AreEqual(matrixA[i, j], result[i, j], Delta);
                }
            }
        }

        [TestMethod]
        public void TranslationTest() {
            Matrix transform = Matrix.Translation(5, -3, 2);
            Tuple point = Tuple.CreatePoint(-3, 4, 5);
            Tuple expected = Tuple.CreatePoint(2, 1, 7);

            Assert.AreEqual(expected, transform * point);
        }

        [TestMethod]
        public void TranslationTest2() {
            Matrix transform = Matrix.Translation(5, -3, 2);
            Matrix inverse = transform.Inverse();
            Tuple point = Tuple.CreatePoint(-3, 4, 5);
            Tuple expected = Tuple.CreatePoint(-8, 7, 3);

            Assert.AreEqual(expected, inverse * point);
        }

        [TestMethod]
        public void TranslationTest3() {
            Matrix transform = Matrix.Translation(5, -3, 2);
            Tuple vector = Tuple.CreateVector(-3, 4, 5);

            Assert.AreEqual(vector, transform * vector);
        }

        [TestMethod]
        public void ScaleTest() {
            Matrix transform = Matrix.Scale(2, 3, 4);
            Tuple point = Tuple.CreatePoint(-4, 6, 8);
            Tuple expected = Tuple.CreatePoint(-8, 18, 32);

            Assert.AreEqual(expected, transform * point);
        }

        [TestMethod]
        public void ScaleTest2() {
            Matrix transform = Matrix.Scale(2, 3, 4);
            Tuple vector = Tuple.CreateVector(-4, 6, 8);
            Tuple expected = Tuple.CreateVector(-8, 18, 32);

            Assert.AreEqual(expected, transform * vector);
        }

        [TestMethod]
        public void ScaleTest3() {
            Matrix transform = Matrix.Scale(2, 3, 4);
            Matrix inverse = transform.Inverse();
            Tuple vector = Tuple.CreateVector(-4, 6, 8);
            Tuple expected = Tuple.CreateVector(-2, 2, 2);

            Assert.AreEqual(expected, inverse * vector);
        }

        [TestMethod]
        public void ScaleTest4() {
            Matrix transform = Matrix.Scale(-1, 1, 1);
            Tuple point = Tuple.CreatePoint(2, 3, 4);
            Tuple expected = Tuple.CreatePoint(-2, 3, 4);

            Assert.AreEqual(expected, transform * point);
        }

        [TestMethod]
        public void RotationXTest() {
            Tuple point = Tuple.CreatePoint(0, 1, 0);
            Matrix halfQuarter = Matrix.RotationX(MathF.PI / 4.0f);
            Matrix fullQuarter = Matrix.RotationX(MathF.PI / 2.0f);

            Tuple expected1 = Tuple.CreatePoint(0, MathF.Sqrt(2.0f) / 2.0f, MathF.Sqrt(2.0f) / 2.0f);
            Tuple expected2 = Tuple.CreatePoint(0, 0, 1);

            Tuple result1 = halfQuarter * point;
            Tuple result2 = fullQuarter * point;

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected1[i], result1[i], Delta);
                Assert.AreEqual(expected2[i], result2[i], Delta);
            }
        }

        [TestMethod]
        public void RotationXTest2() {
            Tuple point = Tuple.CreatePoint(0, 1, 0);
            Matrix halfQuarter = Matrix.RotationX(MathF.PI / 4.0f);
            Matrix inverse = halfQuarter.Inverse();

            Tuple expected = Tuple.CreatePoint(0, MathF.Sqrt(2.0f) / 2.0f, -MathF.Sqrt(2.0f) / 2.0f);

            Tuple result = inverse * point;

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected[i], result[i], Delta);
            }
        }

        [TestMethod]
        public void RotationYTest() {
            Tuple point = Tuple.CreatePoint(0, 0, 1);
            Matrix halfQuarter = Matrix.RotationY(MathF.PI / 4.0f);
            Matrix fullQuarter = Matrix.RotationY(MathF.PI / 2.0f);

            Tuple expected1 = Tuple.CreatePoint(MathF.Sqrt(2.0f) / 2.0f, 0, MathF.Sqrt(2.0f) / 2.0f);
            Tuple expected2 = Tuple.CreatePoint(1, 0, 0);

            Tuple result1 = halfQuarter * point;
            Tuple result2 = fullQuarter * point;

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected1[i], result1[i], Delta);
                Assert.AreEqual(expected2[i], result2[i], Delta);
            }
        }

        [TestMethod]
        public void RotationZTest() {
            Tuple point = Tuple.CreatePoint(0, 1, 0);
            Matrix halfQuarter = Matrix.RotationZ(MathF.PI / 4.0f);
            Matrix fullQuarter = Matrix.RotationZ(MathF.PI / 2.0f);

            Tuple expected1 = Tuple.CreatePoint(-MathF.Sqrt(2.0f) / 2.0f, MathF.Sqrt(2.0f) / 2.0f, 0);
            Tuple expected2 = Tuple.CreatePoint(-1, 0, 0);

            Tuple result1 = halfQuarter * point;
            Tuple result2 = fullQuarter * point;

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected1[i], result1[i], Delta);
                Assert.AreEqual(expected2[i], result2[i], Delta);
            }
        }

        [TestMethod]
        public void ShearXYTest() {
            Matrix transform = Matrix.Shear(1, 0, 0, 0, 0, 0);
            Tuple point = Tuple.CreatePoint(2, 3, 4);
            Tuple expected = Tuple.CreatePoint(5, 3, 4);

            Tuple result = transform * point;

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected[i], result[i], Delta);
            }
        }

        [TestMethod]
        public void ShearXZTest() {
            Matrix transform = Matrix.Shear(0, 1, 0, 0, 0, 0);
            Tuple point = Tuple.CreatePoint(2, 3, 4);
            Tuple expected = Tuple.CreatePoint(6, 3, 4);

            Tuple result = transform * point;

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected[i], result[i], Delta);
            }
        }

        [TestMethod]
        public void ShearYXTest() {
            Matrix transform = Matrix.Shear(0, 0, 1, 0, 0, 0);
            Tuple point = Tuple.CreatePoint(2, 3, 4);
            Tuple expected = Tuple.CreatePoint(2, 5, 4);

            Tuple result = transform * point;

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected[i], result[i], Delta);
            }
        }

        [TestMethod]
        public void ShearYZTest() {
            Matrix transform = Matrix.Shear(0, 0, 0, 1, 0, 0);
            Tuple point = Tuple.CreatePoint(2, 3, 4);
            Tuple expected = Tuple.CreatePoint(2, 7, 4);

            Tuple result = transform * point;

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected[i], result[i], Delta);
            }
        }

        [TestMethod]
        public void ShearZXTest() {
            Matrix transform = Matrix.Shear(0, 0, 0, 0, 1, 0);
            Tuple point = Tuple.CreatePoint(2, 3, 4);
            Tuple expected = Tuple.CreatePoint(2, 3, 6);

            Tuple result = transform * point;

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected[i], result[i], Delta);
            }
        }

        [TestMethod]
        public void ShearZYTest() {
            Matrix transform = Matrix.Shear(0, 0, 0, 0, 0, 1);
            Tuple point = Tuple.CreatePoint(2, 3, 4);
            Tuple expected = Tuple.CreatePoint(2, 3, 7);

            Tuple result = transform * point;

            for (int i = 0; i < 4; i++) {
                Assert.AreEqual(expected[i], result[i], Delta);
            }
        }
    }
}
