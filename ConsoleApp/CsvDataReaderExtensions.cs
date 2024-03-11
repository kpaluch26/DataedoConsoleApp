using System;

namespace ConsoleApp
{
    public static class CsvDataReaderExtensions
    {
        public static string ToImportedObjectProperty(this string line)
        {
            if (string.IsNullOrEmpty(line))
                return string.Empty;

            return line.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        }
    }
}
