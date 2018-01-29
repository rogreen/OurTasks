using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OurTasks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            menuPage.ListView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuItem;
            if (item != null)
            {
                switch (item.Title)
                {
                    case "Home":
                        App.TasksFilter = "All";
                        break;
                    case "Today's tasks":
                        App.TasksFilter = "Today";
                        break;
                    case "Next 7 days":
                        App.TasksFilter = "Next7Days";
                        break;
                    case "Next 2 weeks":
                        App.TasksFilter = "Next2Weeks";
                        break;
                    case "Next month":
                        App.TasksFilter = "NextMonth";
                        break;
                    default:
                        break;
                }
                Detail = new NavigationPage(new ItemsPage());
                menuPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}