namespace ChlorophyGUITests.Views;

public partial class ResultsPage : ContentPage
{
    public ResultsPage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.ResultsPageViewModel();
    }


    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    public async void OnlistViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var species = ((ListView)sender).SelectedItem as Models.Species;

        if (species != null)
        {
            var viewModel = new ViewModels.PlantDetailsPageViewModel();
            viewModel.SetSpeciesId(species.id);
            await viewModel.InitializeAsync();

            var page = new PlantDetailsPage();
            page.BindingContext = viewModel;
            await Navigation.PushAsync(page);
        }

    }
}