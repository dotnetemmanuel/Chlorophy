namespace ChlorophyGUITests.Views;

public partial class ResultsPage : ContentPage
{
    public ResultsPage()
    {
        InitializeComponent();
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}