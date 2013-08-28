using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MundlTransit.WP8.Data.Reference;

namespace DbPrepare
{
    class Program
    {
        static void Main(string[] args)
        {
            // Clean up first
            System.IO.File.Delete(ReferenceDataContext.DatabaseName);

            // Re-initialize
            var db = ReferenceDataContext.CreateConnection();
            ReferenceDataContext.InitializeDatabase(db);

            // Perform import
            var importer = new Ogd.Importer();
            importer.Import(db);
        }
    }
}
