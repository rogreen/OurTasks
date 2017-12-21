using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OurTasks
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public List<PickerItem> Occurrences { get; set; }
        private ToDoItem newItem = null;

        public ToDoItem Item { get; set; }
        public Command SaveItemCommand { get; set; }
        public Command DeleteItemCommand { get; set; }

        public ItemDetailViewModel(ToDoItem item = null)
        {
            Title = item?.Text;
            Item = item;

            SaveItemCommand = new Command(async () => await ExecuteSaveItemCommand());
            DeleteItemCommand = new Command(async () => await ExecuteDeleteItemCommand());

            PopulateOccurrences();
        }

        async Task ExecuteSaveItemCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ToDoItem item;
                int i;
                switch (Item.Occurrence)
                {
                    case "Once":
                        await App.DataStore.SaveTaskAsync(Item);

                        break;
                    case "Daily":
                        await App.DataStore.SaveTaskAsync(Item);

                        for (i = 1; i <= 364; i++)
                        {
                            item = CreateNewTodoItem();
                            item.DueDate = Item.DueDate.AddDays(i);
                            await App.DataStore.SaveTaskAsync(item);
                        }

                        break;
                    case "Weekly":
                        await App.DataStore.SaveTaskAsync(Item);

                        for (i = 1; i <= 52; i++)
                        {
                            item = CreateNewTodoItem();
                            item.DueDate = Item.DueDate.AddDays(i * 7);
                            await App.DataStore.SaveTaskAsync(item);
                        }

                        break;
                    case "TwiceMonthly":
                        await App.DataStore.SaveTaskAsync(Item);

                        for (i = 1; i <= 26; i++)
                        {
                            item = CreateNewTodoItem();
                            item.DueDate = Item.DueDate.AddDays(i * 14);
                            await App.DataStore.SaveTaskAsync(item);
                        }

                        break;
                    case "Monthly":
                        await App.DataStore.SaveTaskAsync(Item);

                        for (i = 1; i <= 12; i++)
                        {
                            item = CreateNewTodoItem();
                            item.DueDate = Item.DueDate.AddMonths(i);
                            await App.DataStore.SaveTaskAsync(item);
                        }

                        break;
                    case "TwiceYearly":
                        await App.DataStore.SaveTaskAsync(Item);

                        for (i = 1; i <= 6; i++)
                        {
                            item = CreateNewTodoItem();
                            item.DueDate = Item.DueDate.AddMonths(i * 2);
                            await App.DataStore.SaveTaskAsync(item);
                        }

                        break;
                    case "Yearly":
                        await App.DataStore.SaveTaskAsync(Item);

                        item = CreateNewTodoItem();
                        item.DueDate = Item.DueDate.AddYears(1);
                        await App.DataStore.SaveTaskAsync(item);

                        break;
                    default:
                        await App.DataStore.SaveTaskAsync(Item);
                        break;
                }
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

        private ToDoItem CreateNewTodoItem()
        {
            return new ToDoItem()
            {
                Text = Item.Text,
                Location = Item.Location,
                AssignedTo = Item.AssignedTo,
                Occurrence = Item.Occurrence
            };
        }

        async Task ExecuteDeleteItemCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await App.DataStore.DeleteTaskAsync(Item);
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

        public void SetLocation(int index)
        {
            switch (index)
            {
                case 0:
                    Item.Location = "Home";
                    break;
                case 1:
                    Item.Location = "Work";
                    break;
                case 2:
                    Item.Location = "Other";
                    break;
                default:
                    break;
            }
        }

        public int GetLocation(string location)
        {
            int index = 0;

            switch (location)
            {
                case "Home":
                    index = 0;
                    break;
                case "Work":
                    index = 1;
                    break;
                case "Other":
                    index = 2;
                    break;
                default:
                    break;
            }

            return index;
        }

        public void SetAssignedTo(int index)
        {
            switch (index)
            {
                case 0:
                    Item.AssignedTo = "Robert";
                    break;
                case 1:
                    Item.AssignedTo = "Colette";
                    break;
                case 2:
                    Item.AssignedTo = "Nobody";
                    break;
                default:
                    break;
            }
        }

        public int GetAssignedTo(string assignedTo)
        {
            int index = 0;

            switch (assignedTo)
            {
                case "Robert":
                    index = 0;
                    break;
                case "Colette":
                    index = 1;
                    break;
                case "Nobody":
                    index = 2;
                    break;
                default:
                    break;
            }

            return index;
        }

        private void PopulateOccurrences()
        {
            Occurrences = new List<PickerItem>
            {
                new PickerItem { Text = "Once" },
                new PickerItem { Text = "Daily" },
                new PickerItem { Text = "Weekly" },
                new PickerItem { Text = "TwiceMonthly" },
                new PickerItem { Text = "Monthly" },
                new PickerItem { Text = "Quarterly" },
                new PickerItem { Text = "TwiceYearly" },
                new PickerItem { Text = "Yearly" }
            };
        }

        public void SetOccurrence(int index)
        {
            switch (index)
            {
                case 0:
                    Item.Occurrence = "Once";
                    break;
                case 1:
                    Item.Occurrence = "Daily";
                    break;
                case 2:
                    Item.Occurrence = "Weekly";
                    break;
                case 3:
                    Item.Occurrence = "TwiceMonthly";
                    break;
                case 4:
                    Item.Occurrence = "Monthly";
                    break;
                case 5:
                    Item.Occurrence = "Quarterly";
                    break;
                case 6:
                    Item.Occurrence = "TwiceYearly";
                    break;
                case 7:
                    Item.Occurrence = "Yearly";
                    break;
                default:
                    break;
            }
        }

        public int GetOccurrence(string occurrence)
        {
            int index = 0;

            switch (occurrence)
            {
                case "Once":
                    index = 0;
                    break;
                case "Daily":
                    index = 1;
                    break;
                case "Weekly":
                    index = 2;
                    break;
                case "TwiceMonthly":
                    index = 3;
                    break;
                case "Monthly":
                    index = 4;
                    break;
                case "TwiceYearly":
                    index = 5;
                    break;
                case "Yearly":
                    index = 6;
                    break;
                default:
                    break;
            }

            return index;
        }

    }
}
