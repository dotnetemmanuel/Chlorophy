namespace ChlorophyGUITests.Views;

public partial class PlantDetailsPage : ContentPage
{
    public PlantDetailsPage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.PlantDetailsPageViewModel();
    }

    public static string keyword = null;

    private async void OnSearchCompleted(object sender, EventArgs e)
    {
        keyword = Search.Text;
        await Navigation.PushAsync(new Views.ResultsPage());
    }

    private async void OnSearchClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void OnUserClicked(object sender, EventArgs e)
    {

    }
}