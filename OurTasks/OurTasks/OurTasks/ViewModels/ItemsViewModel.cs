using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OurTasks
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Item> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Tasks";
            Items = new ObservableRangeCollection<Item>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                //foreach (var item in items)
                //{
                //    Items.Add(item);
                //}
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
