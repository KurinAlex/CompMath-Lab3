using CompMath_Lab3;

namespace Program
{
    public class Program
    {
        const string DirectoryPath = @"D:\Sources\University\2 course\CompMath\CompMath-Lab3";
        const string InputFolderName = "Input";
        const string MatrixAFileName = "A.txt";
        const string MatrixBFileName = "B.txt";
        const string OutputFileName = "Result.txt";

        static readonly string matrixAFilePath = Path.Combine(DirectoryPath, InputFolderName, MatrixAFileName);
        static readonly string matrixBFilePath = Path.Combine(DirectoryPath, InputFolderName, MatrixBFileName);

        const double Error = 1e-5;

        static readonly IIterationalMethod[] methods =
        {
            new JacobiMethod(),
            new SeidelMethod()
        };

        static void Main(string[] args)
        {
            using (StreamWriter fileWriter = new(OutputFileName))
            {
                Writer writer = new(fileWriter);

                try
                {
                    writer.WriteLine("A:");
                    Matrix A = new(matrixAFilePath);
                    writer.WriteLine(A.ToString());
                    writer.WriteDivider();

                    writer.WriteLine("B:");
                    Matrix B = new(matrixBFilePath);
                    writer.WriteLine(B.ToString());
                    writer.WriteDivider();

                    writer.WriteLine("A * X = B");
                    writer.WriteDivider();
                    writer.WriteDivider();

                    foreach(var method in methods)
                    {
                        writer.WriteLine($"{method.Name} method:");
                        writer.WriteDivider();

                        Matrix X = method.Solve(A, B, Error, writer);

                        writer.WriteLine("X:");
                        writer.WriteLine(X.ToString());
                        writer.WriteDivider();
                        writer.WriteDivider();
                    }
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }

            Console.ReadKey();
        }
    }
}