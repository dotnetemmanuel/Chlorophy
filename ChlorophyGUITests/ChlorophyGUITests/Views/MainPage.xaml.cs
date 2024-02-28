namespace ChlorophyGUITests
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public static string keyword = null;

        private async void OnSearchCompleted(object sender, EventArgs e)
        {
            keyword = Search.Text;
            await Navigation.PushAsync(new Views.ResultsPage());
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void OnUserClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.UserPage());
        }
    }

}
