namespace ConsoleTable
{
    abstract class TableDataReader
    {
        public abstract IEnumerable<string[]> ReadAll();
    }
}