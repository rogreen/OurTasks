using Xamarin.Forms;

namespace OurTasks
{
    public partial class App : Application
    {
        public static AzureDataStore DataStore = AzureDataStore.DefaultManager;

        public static string TasksFilter = "Next7Days";

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
