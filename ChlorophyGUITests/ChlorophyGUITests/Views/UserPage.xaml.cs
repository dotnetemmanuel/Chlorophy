namespace ChlorophyGUITests.Views;

public partial class UserPage : ContentPage
{
    public UserPage()
    {
        InitializeComponent();
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private async void OnUserClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.UserPage());
    }

    private async void OnCreateAccountClicked(object sender, EventArgs e)
    {
        Models.User user = new Models.User()
        {
            Id = Guid.NewGuid(),
            Firstname = FirstnameCreate.Text,
            Lastname = LastnameCreate.Text,
            Email = EmailCreate.Text,
            Password = PasswordCreate.Text
        };

        await Data.Database.ProductCollection().InsertOneAsync(user);
    }
}