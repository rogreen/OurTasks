using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OurTasks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public ListView ListView { get { return menuItemsListView; } }

        public MenuPage()
        {
            InitializeComponent();
        }
    }
}