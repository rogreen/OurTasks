/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
 */
//#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#endif

namespace OurTasks
{
    public partial class AzureDataStore
    {
        static AzureDataStore defaultInstance = new AzureDataStore();
        MobileServiceClient client;

#if OFFLINE_SYNC_ENABLED
        IMobileServiceSyncTable<ToDoItem> itemTable;
#else
        IMobileServiceTable<ToDoItem> itemTable;
#endif

        const string offlineDbPath = @"localstore.db";

        private AzureDataStore()
        {
            this.client = new MobileServiceClient(
                "https://ourtaskstemp.azurewebsites.net");

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore(offlineDbPath);
            store.DefineTable<ToDoItem>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);

            this.itemTable = client.GetSyncTable<ToDoItem>();
#else
            this.itemTable = client.GetTable<ToDoItem>();
#endif
        }

        public static AzureDataStore DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public bool IsOfflineEnabled
        {
            get
            {
                return itemTable is
                  Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<ToDoItem>;
            }
        }

        public async Task<ObservableCollection<ToDoItem>> GetItemsAsync(
            bool syncItems = false)
        {
            try
            {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                IEnumerable<ToDoItem> items;
                System.DateTime endDate = DateTime.Today;
                switch (App.TasksFilter)
                {
                    case "Today":
                        items = await itemTable
                           .Where(itemItem => !itemItem.Completed &&
                                  itemItem.DueDate == DateTime.Today)
                           .OrderBy(item => item.DueDate)
                           .ToEnumerableAsync();
                        break;
                    case "Next7Days":
                        items = await itemTable
                           .Where(itemItem => !itemItem.Completed &&
                                  itemItem.DueDate <= DateTime.Today.AddDays(7))
                           .OrderBy(item => item.DueDate)
                           .ToEnumerableAsync();
                        break;
                    case "Next2Weeks":
                        items = await itemTable
                           .Where(itemItem => !itemItem.Completed &&
                                  itemItem.DueDate <= DateTime.Today.AddDays(14))
                           .OrderBy(item => item.DueDate)
                           .ToEnumerableAsync();
                        break;
                    case "NextMonth":
                        items = await itemTable
                           .Where(itemItem => !itemItem.Completed &&
                                  itemItem.DueDate <= DateTime.Today.AddMonths(1))
                           .OrderBy(item => item.DueDate)
                           .ToEnumerableAsync();
                        break;
                    default:
                        items = await itemTable
                           .Where(itemItem => !itemItem.Completed)
                           .OrderBy(item => item.DueDate)
                           .ToEnumerableAsync();
                        break;
                }
                return new ObservableCollection<ToDoItem>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task SaveTaskAsync(ToDoItem item)
        {
            if (item.Id == null)
            {
                try
                {
                    await itemTable.InsertAsync(item);
                }
                catch (Exception ex)
                {
                    string wtf = ex.Message;
                }
            }
            else
            {
                await itemTable.UpdateAsync(item);
            }
        }

        public async Task DeleteTaskAsync(ToDoItem item)
        {
            await itemTable.DeleteAsync(item);
        }

#if OFFLINE_SYNC_ENABLED
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

                await this.itemTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allItems",
                    this.itemTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }
#endif
    }
}
