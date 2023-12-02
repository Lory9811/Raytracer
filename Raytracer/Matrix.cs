﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer {
    public class Matrix {
        public readonly int order;
        private float[,] matrix;
        private bool transposed = false;

        /// <summary>
        /// Creates a square matrix of specified order
        /// </summary>
        /// <param name="order">The order of the square matrix</param>
        public Matrix(int order) {
            this.order = order;
            matrix = new float[order, order];
        }

        public Matrix(int order, float[,] matrix) {
            this.order = order;
            
            if (matrix.Length != order * order) {
                throw new ArgumentException("Incorrect size for input data");
            }

            this.matrix = matrix;
        }

        public static Matrix Eye(int order) {
            Matrix matrix = new Matrix(order);
            for (int i = 0; i < order; i++) {
                matrix[i, i] = 1;
            }
            return matrix;
        }

        public Matrix T() {
            Matrix result = new Matrix(order, matrix);
            result.transposed = true;

            return result;
        }

        public float Det() {
            if (order == 2) {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }

            float det = 0;

            for (int j = 0; j < order; j++) {
                det += this[0, j] * Cofactor(0, j);
            }

            return det;
        }

        public Matrix Submatrix(int i, int j) {
            Matrix result = new Matrix(order - 1);

            for (int y = 0; y < order; y++) {
                for (int x = 0; x < order; x++) {
                    if (y == i || x == j) {
                        continue;
                    }

                    int row = y <= i ? y : y - 1;
                    int column = x <= j ? x : x - 1;

                    result[row, column] = matrix[y, x];
                }
            }

            return result;
        }

        public float Minor(int i, int j) {
            return Submatrix(i, j).Det();
        }

        public float Cofactor(int i, int j) {
            if ((i + j) % 2 == 0) {
                return Minor(i, j);
            } else {
                return -Minor(i, j);
            }
        }

        public bool IsInvertible() {
            return Det() != 0;
        }

        public Matrix Inverse() {
            if (!IsInvertible()) throw new InvalidOperationException("Matrix not invertible");

            Matrix result = new Matrix(order);
            for (int  i = 0; i < order; i++) {
                for (int j = 0; j < order; j++) {
                    result[j, i] = Cofactor(i, j) / Det();
                }
            }

            return result;
        }

        public float this[int i, int j] {
            get {
                if (i < 0 || j < 0 ||
                    i >= order || j >= order) {
                    throw new ArgumentOutOfRangeException();
                }

                if (transposed) return matrix[j, i];
                return matrix[i, j]; 
            }
            set {
                if (i < 0 || j < 0 ||
                    i >= order || j >= order) {
                    throw new ArgumentOutOfRangeException();
                }
                
                if (transposed) {
                    matrix[j, i] = value;
                } else {
                    matrix[i, j] = value; 
                }
            }
        }

        public override bool Equals(object? obj) {
            if (obj == null) return false;
            if (obj is not Matrix) return false;

            Matrix other = (Matrix)obj;
            if (order != other.order) return false;


            for (int i = 0; i < order; i++) {
                for (int j = 0; j < order; j++) {
                    if (this[i, j] != other[i, j]) return false; 
                }
            }
            return true;
        }

        public override int GetHashCode() {
            return HashCode.Combine(matrix, transposed);
        }

        public static Tuple operator *(Matrix left, Tuple right) {
            if (left.order != 4) throw new InvalidOperationException($"Can't multiply matrix of order {left.order} with 4x1 tuple");

            Tuple result = new Tuple(0, 0, 0, 0);

            for (int i = 0; i < left.order; i++) {
                Tuple row = new Tuple(left[i, 0], left[i, 1], left[i, 2], left[i, 3]);

                result[i] = row.Dot(right);
            }

            return result;
        }

        public static Matrix operator *(Matrix left, Matrix right) {
            if (left.order != right.order) throw new InvalidOperationException("Can't multiply matrices of different orders");

            Matrix result = new Matrix(left.order);

            for (int i = 0; i < result.order; i++) {
                for (int j = 0; j < result.order; j++) {
                    Tuple row = new Tuple(left[i, 0], left[i, 1],
                        left[i, 2], left[i, 3]);

                    Tuple col = new Tuple(right[0, j], right[1, j],
                        right[2, j], right[3, j]);

                    result[i, j] = row.Dot(col);
                }
            }

            return result;
        }

        public static bool operator ==(Matrix left, Matrix right) {
            return left.Equals(right);
        }

        public static bool operator !=(Matrix left, Matrix right) {
            return !left.Equals(right);
        }
    }
}
