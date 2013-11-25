using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using DbPrepare.Common;
using MundlTransit.WP8.Data.Reference;

namespace DbPrepare
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.File.Delete(ReferenceDataContext.DatabaseName);   // delete old database first

            PerformImportActionsAsync().Wait();

            Console.WriteLine("All operations completed. Press any key to continue...");
            Console.Read();
        }

        private static async Task PerformImportActionsAsync()
        {
            var ctx = new ReferenceDataContext();
            
            // Re-initialize all tables
            await ctx.InitializeDatabaseAsync();

            // Perform import
            var importer = new Ogd.Importer(ctx);

            await importer.ImportAsync();
            await importer.CreateLookupTableAsync();
        }
    }
}
