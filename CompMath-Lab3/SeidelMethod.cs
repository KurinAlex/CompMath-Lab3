namespace CompMath_Lab3
{
    public class SeidelMethod : IIterationalMethod
    {
        public string Name => "Seidel";

        public Matrix Solve(Matrix A, Matrix B, double error, Writer? writer = null)
        {
            if (A.Height != A.Width)
            {
                throw new ArgumentException("Matrix is not square");
            }

            if (A.Height != B.Height)
            {
                throw new ArgumentException("Matrixes have different number of rows");
            }

            int m = B.Height;
            int n = B.Width;

            double[][] X = new double[m][];
            for (int i = 0; i < m; i++)
            {
                X[i] = new double[n];
                Array.Fill(X[i], 0.0);
            }

            Matrix e = B;

            void Write(string message, int i)
            {
                writer?.WriteLine(message);
                writer?.WriteDivider();
                writer?.WriteLine($"X_{i}:");
                writer?.WriteLine(ArrayHelper.ToString(X));
                writer?.WriteDivider();
                writer?.WriteLine($"e_{i} = B - A * X_{i}:");
                writer?.WriteLine(e.ToString(true));
                writer?.WriteDivider();
                writer?.WriteLine($"||e_{i}|| = {e.Norm}");
                writer?.WriteDivider();
                writer?.WriteDivider();
            }

            Write("Start approximation:", 0);

            for (int iteration = 1; e.Norm >= error; iteration++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int i = 0; i < m; i++)
                    {
                        double s = Enumerable
                            .Range(0, m)
                            .Select(k => k == i ? 0.0 : A[i, k] * X[k][j])
                            .Sum();
                        X[i][j] = (B[i, j] - s) / A[i, i];
                    }
                }
                e = B - A * new Matrix(X);
                Write($"{iteration} iteration:", iteration);
            }
            return new(X);
        }
    }
}
