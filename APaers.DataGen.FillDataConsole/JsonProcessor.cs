using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace APaers.DataGen.FillDataConsole
{
    internal class JsonProcessor : ProcessorBase
    {
        protected override void SaveCollection<TEntity>(string collectionName, List<TEntity> entities)
        {
            string json = JsonConvert.SerializeObject(entities, Formatting.Indented);
            string fileName = Path.Combine(@"..\..\..\APaers.DataGen.Api\App_Data", collectionName);
            fileName = Path.ChangeExtension(fileName, "json");
            File.WriteAllText(fileName, json);
            Console.WriteLine("Write to JSON file '{0}' collection of '{1}'. Count = {2}", fileName, typeof(TEntity).Name, entities.Count);
        }
    }
}