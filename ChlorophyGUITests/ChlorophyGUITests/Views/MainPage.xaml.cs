using MongoDB.Driver;

namespace ChlorophyGUITests
{
    public partial class MainPage : ContentPage
    {
        public static DateTime Today;

        public MainPage()
        {
            InitializeComponent();
            Today = DateTime.Now;
            var task = Task.Run(() => ReadWriteFile.ReadAll("LoggedInUser.txt"));
            var currentUser = task.Result;
            if (currentUser != null)
            {
                Views.UserPage.CurrentUser = currentUser;
                Views.UserPage.SignedInUserEmail = currentUser.Email;
            }
            else
            {
                Views.UserPage.CurrentUser = currentUser;
                Views.UserPage.SignedInUserEmail = null;
            }
            //ReadWriteFile.ReadAll("LoggedInUser.txt");
            BindingContext = new ViewModels.MainPageViewModel();
            HideWelcomeMessage();

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

        private async void OnItemTapped(object sender, TappedEventArgs e)
        {
            var frame = sender as Frame;
            var plant = frame.BindingContext as Models.PlantDetails;
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

        private async void OnCollectionViewItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var species = e.CurrentSelection.FirstOrDefault() as Models.PlantDetails;
            if (species != null)
            {
                var viewModel = new ViewModels.PlantDetailsPageViewModel();
                viewModel.SetSpeciesId((int)species.id);
                await viewModel.InitializeAPIAsync();

                var page = new Views.PlantDetailsPage();
                page.BindingContext = viewModel;
                await Navigation.PushAsync(page);
            }
        }
    }
}