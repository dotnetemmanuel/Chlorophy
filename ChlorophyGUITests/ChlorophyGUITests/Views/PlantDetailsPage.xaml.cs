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

    private async void OnBackToPreviousClicked(object sender, EventArgs e)
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

        DateTime wateringDateLocalTime = DateTime.Now;
        selectedItem.WateringDate = wateringDateLocalTime;

        var existingUser = await Data.Database.ProductCollection().Find(Builders<Models.User>.Filter.Eq("Email", Views.UserPage.SignedInUserEmail)).FirstOrDefaultAsync();

        int index = existingUser.Plants.FindIndex(p => p.id == selectedItem.id);

        existingUser.Plants[index] = selectedItem;

        var filter = Builders<Models.User>.Filter.Eq(x => x.Email, existingUser.Email);
        await Data.Database.ProductCollection().ReplaceOneAsync(filter, existingUser);

    }

}