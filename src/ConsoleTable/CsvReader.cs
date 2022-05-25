namespace ConsoleTable
{
    internal class CsvReader : TableDataReader
    {
        private string _filename;

        public CsvReader(string filename)
        {
            _filename = filename;
        }

        public override IEnumerable<string[]> ReadAll()
        {
            using var reader = new StreamReader(_filename);

            while(!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                yield return line.Split(',');
            }
        }
    }
}