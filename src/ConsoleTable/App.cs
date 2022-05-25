using System.Text;

namespace ConsoleTable
{
    internal class App
    {
        public App()
        {
        }

        internal int Run(string[] args)
        {
            var tableDataReader = CreateTableDataReader(args);

            if (tableDataReader == null)
                return 1;

            var records = tableDataReader.ReadAll().ToList();

            var columnWidths = new List<int>();

            foreach (var record in records)
            {
                for (int index = 0; index < record.Length; index++)
                {
                    var value = record[index];
                    if (columnWidths.Count < index + 1)
                        columnWidths.Add(0);

                    var width = columnWidths[index];
                    if (value.Length > width)
                        columnWidths[index] = value.Length;
                }
            }

            var makeRow = new Func<string[], string>((record) =>
            {
                var builder = new StringBuilder();

                for (int index = 0; index < record.Length; index++)
                {
                    var value = record[index];
                    var width = columnWidths[index];

                    if (builder.Length != 0)
                        builder.Append(" | ");
                    builder.Append(value.PadRight(width));
                }

                return builder.ToString();
            });

            var header = makeRow(records[0]);
            Console.WriteLine(header);

            // SCOPE
            {
                var builder = new StringBuilder();

                for (int index = 0; index < columnWidths.Count; index++)
                {
                    var width = columnWidths[index];

                    if (builder.Length != 0)
                        builder.Append(" | ");
                    builder.Append(new string('-', width));
                }
                Console.WriteLine(builder);
            }            

            foreach (var record in records.Skip(1))
                Console.WriteLine(makeRow(record));

            return 0;
        }

        private TableDataReader? CreateTableDataReader(string[] args)
        {
            if (args.Length == 1)
            {
                var filename = args[0];

                if (!File.Exists(filename))
                {
                    Console.WriteLine($"File '{filename}' not found");
                    return null;
                }

                return new CsvReader(filename);
            }

            if(Console.IsInputRedirected)
                return new StdInReader();

            Console.WriteLine("Expected filename or redirected stdin");
            return null;
        }
    }
}