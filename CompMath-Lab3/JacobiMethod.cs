﻿namespace CompMath_Lab3
{
    public class JacobiMethod : IIterationalMethod
    {
        public string Name => "Jacobi";

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
    }
}