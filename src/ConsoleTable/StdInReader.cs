namespace ConsoleTable
{
    internal class StdInReader : TableDataReader
    {
        public override IEnumerable<string[]> ReadAll()
        {
            using var reader = new StreamReader(Console.OpenStandardInput());

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                yield return line.Split(',');
            }
        }
    }
}