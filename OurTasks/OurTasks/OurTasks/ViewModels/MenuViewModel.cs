using System.Windows.Input;
using Xamarin.Forms;

namespace OurTasks
{
    public class MenuViewModel
    {
        public ICommand GoHomeCommand { get; set; }
        public ICommand GoTodayCommand { get; set; }
        public ICommand GoNext7DaysCommand { get; set; }
        public ICommand GoNext2WeeksCommand { get; set; }
        public ICommand GoNextMonthCommand { get; set; }

        public MenuViewModel()
        {
            GoHomeCommand = new Command(GoHome);
            GoTodayCommand = new Command(GoToday);
            GoNext7DaysCommand = new Command(GoNext7Days);
            GoNext2WeeksCommand = new Command(GoNext2Weeks);
            GoNextMonthCommand = new Command(GoNextMonth);
        }

        void GoHome(object obj)
        {
            App.TasksFilter = "All";
            App.NavigationPage.Navigation.PopToRootAsync();
            App.MenuIsPresented = false;
        }

        void GoToday(object obj)
        {
            App.TasksFilter = "Today";
            App.NavigationPage.Navigation.PushAsync(new ItemsPage());
            App.MenuIsPresented = false;
        }

        void GoNext7Days(object obj)
        {
            App.TasksFilter = "Next7Days";
            App.NavigationPage.Navigation.PushAsync(new ItemsPage());
            App.MenuIsPresented = false;
        }

        void GoNext2Weeks(object obj)
        {
            App.TasksFilter = "Next2Weeks";
            App.NavigationPage.Navigation.PushAsync(new ItemsPage());
            App.MenuIsPresented = false;
        }

        void GoNextMonth(object obj)
        {
            App.TasksFilter = "NextMonth";
            App.NavigationPage.Navigation.PushAsync(new ItemsPage());
            App.MenuIsPresented = false;
        }

    }
}
