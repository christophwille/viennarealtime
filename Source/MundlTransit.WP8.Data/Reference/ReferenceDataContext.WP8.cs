using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.Storage.Search;
using MundlTransit.WP8.Data.Runtime;

namespace MundlTransit.WP8.Data.Reference
{
    public partial class ReferenceDataContext
    {
        //
        // http://wp.qmatteoq.com/import-an-already-existing-sqlite-database-in-a-windows-8-application/
        //
        public static async Task<bool> CopyDatabaseAsync()
        {
            bool isDatabaseExisting = false;

            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(ReferenceDatabaseName).AsTask().ConfigureAwait(false);
                isDatabaseExisting = true;
            }
            catch
            {
                isDatabaseExisting = false;
            }

            if (!isDatabaseExisting)
            {
                StorageFile databaseFile = await Package.Current.InstalledLocation.GetFileAsync(ReferenceDatabaseName).AsTask().ConfigureAwait(false);
                await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder).AsTask().ConfigureAwait(false);
            }

            return !isDatabaseExisting;
        }

        public static async Task DeletePreviousDatabasesAsync(string customDbToKeep)
        {
            try
            {
                var files = await ApplicationData.Current.LocalFolder.GetFilesAsync().AsTask().ConfigureAwait(false);

                customDbToKeep = customDbToKeep.ToLowerInvariant();
                string refDbName = ReferenceDatabaseName.ToLowerInvariant();
                string runtimeDbName = RuntimeDataContext.DatabaseName.ToLowerInvariant();

                foreach (var file in files)
                {
                    string name = file.Name.ToLowerInvariant();

                    // Do not touch files that don't belong to us
                    if (!name.EndsWith(".db3")) continue;

                    // If it is a file we need to keep, continue
                    if (customDbToKeep == name || refDbName == name || runtimeDbName == name) continue;

                    try
                    {
                        Debug.WriteLine("DeletePreviousDatabasesAsync - deleting file: " + name);
                        await file.DeleteAsync().AsTask().ConfigureAwait(false);
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
