using System.Windows.Input;
using Xamarin.Forms;

namespace OurTasks
{
    public class MenuViewModel
    {
        public ObservableRangeCollection<MenuItem> MenuItems { get; set; }

        public MenuViewModel()
        {
            InitMenuItems();
        }

        private void InitMenuItems()
        {
            MenuItems.Add(new MenuItem { Title = "Home" });
            MenuItems.Add(new MenuItem { Title = "Today's tasks"});
            MenuItems.Add(new MenuItem { Title = "Next 7 days"});
            MenuItems.Add(new MenuItem { Title = "Next 2 weeks"});
            MenuItems.Add(new MenuItem { Title = "Next month"});
        }
    }
}
