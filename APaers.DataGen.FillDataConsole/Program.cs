using System;

namespace APaers.DataGen.FillDataConsole
{
    internal static class Program
    {
        private static void Main()
        {
            var dbCache = new DbCache();
            dbCache.FillFromFiles();
            new MongoDbProcessor().Save(dbCache);
            new JsonProcessor().Save(dbCache);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
