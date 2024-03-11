using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{   
    public class CsvDataReader : IDataReader
    {
        private List<ImportedObject> ImportedObjects;
        private IDataPrinter DataPrinter;

        public CsvDataReader(IDataPrinter dataPrinter)
        {
            ImportedObjects = new List<ImportedObject>();
            DataPrinter = dataPrinter;
        }

        public void ReadAndPrintData(string fileToRead, bool printData = true)
        {
            var streamReader = new StreamReader(fileToRead);
            var importedLines = new List<string>();

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                importedLines.Add(line);
            }

            foreach(var line in importedLines)
            {
                var values = line.Split(';');
                if (values.Length < 7)
                {
                    var length = values.Length;
                    Array.Resize(ref values, length + (7 - values.Length));

                    for(int i = length; i < 7; i++)
                        values[i] = String.Empty;
                }

                var importedObject = new ImportedObject
                {
                    Type = values[0].ToImportedObjectProperty().ToUpper(),
                    Name = values[1].ToImportedObjectProperty(),
                    Schema = values[2].ToImportedObjectProperty(),
                    ParentName = values[3].ToImportedObjectProperty(),
                    ParentType = values[4].ToImportedObjectProperty(),
                    DataType = values[5].ToImportedObjectProperty(),
                    IsNullable = values[6].ToImportedObjectProperty() == "1" ? true : false
                };
                ImportedObjects.Add(importedObject);
            }

            CalculateNumberOfChildren();
            if (printData)
                DataPrinter.PrintData(ImportedObjects);
        }

        private void CalculateNumberOfChildren()
        {
            foreach (var importedObject in ImportedObjects)
            {
                foreach (var obj in ImportedObjects)
                {
                    if (obj.ParentType == importedObject.Type && obj.ParentName == importedObject.Name)
                    {
                        importedObject.NumberOfChildren++;
                    }
                }
            }
        }
    }
}
