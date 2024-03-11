using System.Collections.Generic;
using System;
using System.Linq;

namespace ConsoleApp
{
    public class ConsoleDataPrinter : IDataPrinter
    {
        public void PrintData(List<ImportedObject> importedObjects)
        {
            foreach (var database in importedObjects.Where(c => c.Type == "DATABASE"))
            {
                Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
    
                foreach (var table in importedObjects.Where(c => c.ParentType.ToUpper() == database.Type && c.ParentName == database.Name))
                {
                    Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
    
                    foreach (var column in importedObjects.Where(c => c.ParentType.ToUpper() == table.Type && c.ParentName == table.Name))
                    {
                        Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable ? "accepts nulls" : "with no nulls")}");
                    }
                }
            }
        }
    }
}
