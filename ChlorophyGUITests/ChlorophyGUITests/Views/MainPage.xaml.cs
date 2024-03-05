﻿using MongoDB.Driver;

namespace ChlorophyGUITests
{
    public partial class MainPage : ContentPage
    {
        public static DateTime Today;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.MainPageViewModel();
            HideWelcomeMessage();
            Today = DateTime.Now;
        }

        public static string keyword = null;

        private async void OnSearchCompleted(object sender, EventArgs e)
        {
            keyword = Search.Text;
            if (Views.UserPage.SignedInUserEmail != null)
            {
                await Navigation.PushAsync(new Views.ResultsPage());
            }
            else
            {
                await Navigation.PushAsync(new Views.UserPage());
            }
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void OnUserClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.UserPage());
        }

        public void HideWelcomeMessage()
        {
            var viewModel = (ViewModels.MainPageViewModel)BindingContext;
            if (Views.UserPage.SignedInUserEmail != null && viewModel.PlantDetails.Count > 0)
            {
                Welcome.IsVisible = false;
            }
        }

        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var plant = ((ListView)sender).SelectedItem as Models.PlantDetails;
            if (plant != null)
            {
                var viewModel = new ViewModels.PlantDetailsPageViewModel();
                viewModel.SetSpeciesId((int)plant.id);
                await viewModel.InitializeDbAsync();

                var page = new Views.PlantDetailsPage();
                page.BindingContext = viewModel;
                await Navigation.PushAsync(page);
            }
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;
            var selectedItem = button.BindingContext as Models.PlantDetails;

            if (selectedItem != null)
            {
                var existingUser = await Data.Database.ProductCollection().Find(Builders<Models.User>.Filter.Eq("Email", Views.UserPage.SignedInUserEmail)).FirstOrDefaultAsync();

                var indexToRemove = existingUser.Plants.FindIndex(p => p.id == selectedItem.id);

                existingUser.Plants.RemoveAt(indexToRemove);
                var filter = Builders<Models.User>.Filter.Eq(x => x.Email, existingUser.Email);

                await Data.Database.ProductCollection().ReplaceOneAsync(filter, existingUser);

                await Navigation.PushAsync(new MainPage());
            }
        }
    }
}