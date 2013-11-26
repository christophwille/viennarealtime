using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Services;

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
            var importer = new DefaultImportService(ctx);

            await importer.ImportBatchAsync();
            await importer.CreateLookupTableAsync();
        }
    }
}
