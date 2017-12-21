using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OurTasks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            LocationPicker.SelectedIndex =
                viewModel.GetLocation(viewModel.Item.Location);
            PeoplePicker.SelectedIndex =
                viewModel.GetAssignedTo(viewModel.Item.AssignedTo);
            OccurrencePicker.SelectedIndex =
                viewModel.GetOccurrence(viewModel.Item.Occurrence);
        }

        private void OnLocationPickerSelectedIndexChanged(Object sender, EventArgs e)
        {
            viewModel.SetLocation(((Picker)sender).SelectedIndex);
        }

        private void OnPeoplePickerSelectedIndexChanged(Object sender, EventArgs e)
        {
            viewModel.SetAssignedTo(((Picker)sender).SelectedIndex);
        }

        private void OnOccurrencePickerSelectedIndexChanged(Object sender, EventArgs e)
        {
            viewModel.SetOccurrence(((Picker)sender).SelectedIndex);
        }

        private async void OnSaveClicked(Object sender, EventArgs e)
        {
            viewModel.SaveItemCommand.Execute(null);

            await Navigation.PopAsync();
        }

        private async void OnCompletedClicked(Object sender, EventArgs e)
        {
            viewModel.DeleteItemCommand.Execute(null);

            await Navigation.PopAsync();
        }

    }
}