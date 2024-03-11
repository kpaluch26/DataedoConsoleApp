using System;

namespace ConsoleApp 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var printer = new ConsoleDataPrinter();
            var reader = new CsvDataReader(dataPrinter: printer);
            reader.ReadAndPrintData("data.csv");
            Console.ReadLine();
        }
    }
}
