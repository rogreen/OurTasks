using OurTasks.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace OurTasks
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainPage = new TabbedPage();
            var itemsPage = new NavigationPage(new ItemsPage()) { Title = "Items" };
            var speakersPage = new NavigationPage(new SpeakersPage()) { Title = "Speakers" };
            var aboutPage = new NavigationPage(new AboutPage()) { Title = "About" };

            Device.OnPlatform(iOS: () => {
                sessionsPage.Icon = "tab_feed.png";
                speakersPage.Icon = "tab_person.png";
                aboutPage.Icon = "tab_about.png";

            });
            mainPage.Children.Add(sessionsPage);
            mainPage.Children.Add(speakersPage);
            mainPage.Children.Add(aboutPage);


            MainPage = mainPage;
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
