namespace CompMath_Lab3
{
    public struct Matrix
    {
        public Matrix(double[][] matrix)
        {
            if (matrix.Any(row => row.Length != matrix[0].Length))
            {
                throw new ArgumentException("Matrix has different row dimentions");
            }

            _matrix = ArrayHelper.Copy(matrix);
            _height = matrix.Length;
            _width = matrix[0].Length;
        }
        public Matrix(string filePath) : this(ArrayHelper.ReadFromFile(filePath))
        {
        }
        public Matrix(Matrix matrix) : this(matrix._matrix)
        {
        }

        public double this[int i, int j] => _matrix[i][j];
        public double Norm => ArrayHelper.GetNorm(_matrix);

        public override string ToString()
        {
            return ArrayHelper.ToString(_matrix);
        }
        public string ToString(bool exponential)
        {
            return ArrayHelper.ToString(_matrix, exponential);
        }

        public static Matrix Solve(Matrix A, Matrix B, double error, Writer? writer = null)
        {
            if (A._height != A._width)
            {
                throw new ArgumentException("Matrix is not square");
            }

            if (A._height != B._height)
            {
                throw new ArgumentException("Matrixes have different number of rows");
            }

            Matrix D = A.GetDiagonal();
            Matrix D1 = A.GetDiagonalInverse();
            Matrix R = A - D;

            Matrix b = D1 * B;
            Matrix a = D1 * R;

            Matrix X = new(b);
            Matrix e = B - A * X;

            void Write(string message, int i)
            {
                writer?.WriteLine(message);
                writer?.WriteDivider();
                writer?.WriteLine($"X_{i}:");
                writer?.WriteLine(X.ToString());
                writer?.WriteDivider();
                writer?.WriteLine($"e_{i} = B - A * X_{i}:");
                writer?.WriteLine(e.ToString(true));
                writer?.WriteDivider();
                writer?.WriteLine($"||e_{i}|| = {e.Norm}");
                writer?.WriteDivider();
                writer?.WriteDivider();
            }

            Write("Start approximation:", 0);

            for (int i = 1; e.Norm >= error; i++)
            {
                X = b - a * X;
                e = B - A * X;
                Write($"{i} iteration:", i);
            }
            return X;
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            int height = left._height, width = left._width;

            if (height != right._height || width != right._width)
            {
                throw new ArgumentException("Matrixes dimentions are not equal");
            }

            double[][] res = new double[height][];

            for (int i = 0; i < height; i++)
            {
                res[i] = new double[width];
                for (int j = 0; j < width; j++)
                {
                    res[i][j] = left[i, j] - right[i, j];
                }
            }

            return new Matrix(res);
        }
        public static Matrix operator *(Matrix left, Matrix right)
        {
            int aWidth = left._width;

            if (aWidth != right._height)
            {
                throw new ArgumentException("Matrixes have wrong dimentions for multiplicaion");
            }

            int aHeight = left._height;
            int bWidth = right._width;
            double[][] res = new double[aHeight][];

            for (int i = 0; i < aHeight; i++)
            {
                res[i] = new double[bWidth];
                for (int j = 0; j < bWidth; j++)
                {
                    for (int k = 0; k < aWidth; k++)
                    {
                        res[i][j] += left[i, k] * right[k, j];
                    }
                }
            }

            return new Matrix(res);
        }

        private Matrix GetDiagonal()
        {
            return new(_matrix
                .Select((row, i) =>
                    Enumerable.Repeat(0.0, i)
                    .Append(row[i])
                    .Concat(Enumerable.Repeat(0.0, row.Length - i - 1))
                    .ToArray())
                .ToArray());
        }
        private Matrix GetDiagonalInverse()
        {
            return new(_matrix
                .Select((row, i) =>
                    Enumerable.Repeat(0.0, i)
                    .Append(1 / row[i])
                    .Concat(Enumerable.Repeat(0.0, row.Length - i - 1))
                    .ToArray())
                .ToArray());
        }

        private readonly double[][] _matrix;
        private readonly int _height;
        private readonly int _width;
    }
}
