using MongoDB.Driver;

namespace ChlorophyGUITests.Views;

public partial class ResultsPage : ContentPage
{
    public bool IsVisible { get; set; } = false;

    public ResultsPage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.ResultsPageViewModel();
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    public async void OnlistViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var species = ((ListView)sender).SelectedItem as Models.Species;

        if (species != null)
        {
            var viewModel = new ViewModels.PlantDetailsPageViewModel();
            viewModel.SetSpeciesId(species.id);
            await viewModel.InitializeAPIAsync();

            var page = new PlantDetailsPage();
            page.BindingContext = viewModel;
            await Navigation.PushAsync(page);
        }
    }

    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        var selectedItem = button.BindingContext as Models.Species;

        if (selectedItem != null)
        {
            var plant = await ViewModels.PlantDetailsPageViewModel.GetPlantDetailsFromAPI(selectedItem.id);
            var existingUser = await Data.Database.ProductCollection().Find(Builders<Models.User>.Filter.Eq("Email", Views.UserPage.SignedInUserEmail)).FirstOrDefaultAsync();

            existingUser.Plants.Add(plant);
            var filter = Builders<Models.User>.Filter.Eq(x => x.Email, existingUser.Email);
            await Data.Database.ProductCollection().ReplaceOneAsync(filter, existingUser);

            await Navigation.PushAsync(new MainPage());
        }
    }

    private async void OnCollectionViewItemSelected(object sender, SelectionChangedEventArgs e)
    {
        var collectionView = (CollectionView)sender;
        //collectionView.BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;

        var species = e.CurrentSelection.FirstOrDefault() as Models.Species;
        if (species != null)
        {
            var viewModel = new ViewModels.PlantDetailsPageViewModel();
            viewModel.SetSpeciesId(species.id);
            await viewModel.InitializeAPIAsync();

            var page = new PlantDetailsPage();
            page.BindingContext = viewModel;
            await Navigation.PushAsync(page);


        }

    }
}