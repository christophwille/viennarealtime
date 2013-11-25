using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Data.Reference;

namespace DbPrepare
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.File.Delete(ReferenceDataContext.DatabaseName);   // delete old database first

            PerformImportActionsAsync().Wait();
            Console.Read();
        }

        private static async Task PerformImportActionsAsync()
        {
            var ctx = new ReferenceDataContext();
            
            // Re-initialize all tables
            await ctx.InitializeDatabaseAsync();

            // Perform import
            var importer = new Ogd.Importer();

            await importer.ImportAsync(ctx);
            await ctx.CreateLookupTableAsync();
        }
    }
}
