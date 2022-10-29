namespace CompMath_Lab3
{
    public interface IIterationalMethod
    {
        string Name { get; }
        Matrix Solve(Matrix A, Matrix B, double error, Writer? writer = null);
    }
}
