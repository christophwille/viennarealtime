using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.Services;
using SQLite;

namespace DbPrepare
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.File.Delete(ReferenceDataContext.ReferenceDatabaseName);   // delete old database first

            PerformImportActionsAsync().Wait();

            Console.WriteLine("All operations completed. Press any key to continue...");
            Console.Read();
        }

        private static async Task<ImportResults> PerformImportActionsAsync()
        {
            var ctx = new ReferenceDataContext();
            
            // Re-initialize all tables
            await ctx.InitializeDatabaseAsync();

            // Perform import
            var importer = new DefaultImportService(ctx);

            var result = await importer.ImportBatchAsync();
            result.LookupCount = await importer.CreateLookupTableAsync();

            Debug.WriteLine("# of Haltestellen: " + result.HaltestellenCount);
            Debug.WriteLine("# of Linien: " + result.LinienCount);
            Debug.WriteLine("# of Steige: " + result.SteigeCount);
            Debug.WriteLine("# of Lookups: " + result.LookupCount);

            return result;
        }
    }
}
