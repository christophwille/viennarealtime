using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace MundlTransit.WP8.Data.Reference
{
    public partial class ReferenceDataContext
    {
        //
        // http://wp.qmatteoq.com/import-an-already-existing-sqlite-database-in-a-windows-8-application/
        //
        public static async Task CopyDatabaseAsync()
        {
            bool isDatabaseExisting = false;

            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(ReferenceDatabaseName);
                isDatabaseExisting = true;
            }
            catch
            {
                isDatabaseExisting = false;
            }

            if (!isDatabaseExisting)
            {
                StorageFile databaseFile = await Package.Current.InstalledLocation.GetFileAsync(ReferenceDatabaseName);
                await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder);
            }
        }

        public static async Task DeletePreviousDatabasesAsync()
        {
            foreach (string filename in PreviousDatabases)
            {
                try
                {
                    StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(ReferenceDatabaseName);
                    await storageFile.DeleteAsync();
                }
                catch
                {
                }
            }
        }
    }
}
