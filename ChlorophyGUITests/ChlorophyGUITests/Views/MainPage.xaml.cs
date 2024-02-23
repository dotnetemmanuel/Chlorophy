namespace ChlorophyGUITests
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSearchCompleted(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ResultsPage());
        }

        private void OnHomeClicked(object sender, EventArgs e)
        {

        }

        private void OnUserClicked(object sender, EventArgs e)
        {

        }
    }

}
