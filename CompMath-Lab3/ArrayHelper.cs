using System.Globalization;

namespace CompMath_Lab3
{
    public static class ArrayHelper
    {
        private const int Precision = 6;
        private const int ExponentialAlignment = Precision + 9;
        private const int FixedPoinAlignment = Precision + 4;

        private static readonly string s_exponentialFormat = $"{{0,{ExponentialAlignment}:E{Precision}}}";
        private static readonly string s_fixedPointFormat = $"{{0,{FixedPoinAlignment}:F{Precision}}}";

        public static double[][] Copy(double[][] source)
        {
            int height = source.Length;
            double[][] result = new double[height][];

            for (int i = 0; i < height; i++)
            {
                result[i] = new double[source[i].Length];
                source[i].CopyTo(result[i], 0);
            }
            return result;
        }

        public static double[][] ReadFromFile(string filePath)
        {
            return File.ReadAllLines(filePath)
                .Select(line =>
                    line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => double.Parse(n, CultureInfo.InvariantCulture))
                    .ToArray()
                ).ToArray();
        }

        public static double GetNorm(double[][] matrix)
        {
            return Enumerable.Range(0, matrix[0].Length)
                .Max(n => Math.Sqrt(matrix.Sum(row => row[n] * row[n])));
        }

        public static string ToString(double[][] matrix, bool exponential = false)
        {
            string format = exponential ? s_exponentialFormat : s_fixedPointFormat;
            return string.Join(
                Environment.NewLine,
                matrix.Select(row =>
                    string.Join(' ', row.Select(n => string.Format(CultureInfo.InvariantCulture, format, n)))
                )
            );
        }
    }
}
