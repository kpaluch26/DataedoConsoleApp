using System.Collections.Generic;

namespace ConsoleApp
{
    public interface IDataPrinter
    {
        void PrintData(List<ImportedObject> importedObjects);
    }
}
