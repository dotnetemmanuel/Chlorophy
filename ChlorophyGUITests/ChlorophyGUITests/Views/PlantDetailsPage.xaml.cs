using MongoDB.Driver;

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

    private async void OnUserClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.UserPage());
    }

    public DateTime GetTodaysDateAsync()
    {
        DateTime today = DateTime.Now;
        return today;
    }

    private async void OnWateringButtonClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        var selectedItem = button.CommandParameter as Models.PlantDetails;

        var currentUser = Data.Database.ProductCollection().Find(Builders<Models.User>.Filter.Eq("Email", Views.UserPage.SignedInUserEmail)).FirstOrDefault();
        var plant = currentUser.Plants.Find(p => p.id == selectedItem.id);

        DateTime wateringDate = DateTime.Now;
        plant.WateringDate = wateringDate;

    }
}