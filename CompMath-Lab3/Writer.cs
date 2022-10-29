namespace CompMath_Lab3
{
    public class Writer
    {
        private const int DividerLength = 54;
        private static readonly string s_divider = new('-', DividerLength);

        private readonly StreamWriter _fileWriter;

        public Writer(StreamWriter fileWriter) => _fileWriter = fileWriter;

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
            _fileWriter.WriteLine(line);
        }

        public void WriteDivider()
        {
            WriteLine(s_divider);
        }
    }
}
