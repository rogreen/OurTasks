using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OurTasks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        private ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ToDoItem;
            if (item == null)
            {
                // the item was deselected
                return;
            }

            // Navigate to the detail page
            await Navigation.PushAsync(new ItemDetailPage(
                new ItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadItemsCommand.Execute(null);
        }

        private async void OnAddClicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemDetailPage(
                new ItemDetailViewModel(new ToDoItem()
                {
                    DueDate = DateTime.Today.AddDays(7)
                }
                )));
        }

        private void OnRefreshClicked(Object sender, EventArgs e)
        {
            viewModel = new ItemsViewModel();
            viewModel.LoadItemsCommand.Execute(null);
        }

    }
}