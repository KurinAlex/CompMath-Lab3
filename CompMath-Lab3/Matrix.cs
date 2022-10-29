namespace CompMath_Lab3
{
    public struct Matrix
    {
        private readonly double[][] _matrix;
        private readonly int _height;
        private readonly int _width;

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

        public int Height => _height;
        public int Width => _width;
        public double Norm => ArrayHelper.GetNorm(_matrix);
        public double this[int i, int j] => _matrix[i][j];

        public Matrix GetDiagonal()
        {
            return new(_matrix
                .Select((row, i) =>
                    Enumerable.Repeat(0.0, i)
                    .Append(row[i])
                    .Concat(Enumerable.Repeat(0.0, row.Length - i - 1))
                    .ToArray())
                .ToArray());
        }
        public Matrix GetDiagonalInverse()
        {
            return new(_matrix
                .Select((row, i) =>
                    Enumerable.Repeat(0.0, i)
                    .Append(1 / row[i])
                    .Concat(Enumerable.Repeat(0.0, row.Length - i - 1))
                    .ToArray())
                .ToArray());
        }
        public override string ToString()
        {
            return ArrayHelper.ToString(_matrix);
        }
        public string ToString(bool exponential)
        {
            return ArrayHelper.ToString(_matrix, exponential);
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

    }
}
