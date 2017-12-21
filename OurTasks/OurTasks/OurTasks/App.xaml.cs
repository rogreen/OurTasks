
using System;
using Xamarin.Forms;

namespace OurTasks
{
    public partial class App : Application
    {
        public static AzureDataStore DataStore = AzureDataStore.DefaultManager;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ItemsPage());
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
