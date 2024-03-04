using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace ChlorophyGUITests.Views;

public partial class UserPage : ContentPage
{
    public Models.User CurrentUser { get; set; }
    public static string SignedInUserEmail { get; private set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }

    public UserPage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.UserPageViewModel();
        CheckIfSignedIn();
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void OnUserClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.UserPage());
    }

    private void CheckIfSignedIn()
    {
        if (Views.UserPage.SignedInUserEmail != null)
        {
            SignInCreate.IsVisible = false;
            SignedIn.IsVisible = true;
        }
    }

    private async void OnCreateAccountClicked(object sender, EventArgs e)
    {
        string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        Regex regex = new Regex(emailPattern);
        string email = EmailCreate.Text;
        MatchCollection matchEmail = regex.Matches(email);

        if (matchEmail.Count > 0 && FirstnameCreate.Text.Length > 0 && LastnameCreate.Text.Length > 0 && PasswordCreate.Text.Length > 0)
        {
            var existingUser = await Data.Database.ProductCollection().Find(Builders<Models.User>.Filter.Eq("Email", email)).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                // User with this email already exists
                ErrorMessage.Text = "A user with this email already exists.";
                RegexCheck.IsVisible = true;
                return; // Stop further execution
            }

            Models.User user = new Models.User()
            {
                Id = Guid.NewGuid(),
                Firstname = FirstnameCreate.Text,
                Lastname = LastnameCreate.Text,
                Email = EmailCreate.Text,
                Password = PasswordCreate.Text,
                Plants = new()
            };

            await Data.Database.ProductCollection().InsertOneAsync(user);
            await Navigation.PopToRootAsync();
        }
        else
        {
            if (matchEmail.Count <= 0)
            {
                ErrorMessage.Text = "Invalid e-mail format";
                RegexCheck.IsVisible = true;
            }
            else if (FirstnameCreate.Text.Length < 0 && LastnameCreate.Text.Length < 0 && PasswordCreate.Text.Length < 0)
            {
                ErrorMessage.Text = "Input cannot be empty";
                RegexCheck.IsVisible = true;
            }
        }
    }

    public async void OnSignInButtonClicked(object sender, EventArgs e)
    {
        string email = EmailSignIn.Text;
        string password = PasswordSignIn.Text;
        var existingUserEmail = await Data.Database.ProductCollection().Find(Builders<Models.User>.Filter.Eq("Email", email)).FirstOrDefaultAsync();

        if (existingUserEmail != null && existingUserEmail.Password == password)
        {
            SignedInUserEmail = existingUserEmail.Email;
            CurrentUser = existingUserEmail;

            await Navigation.PushAsync(new UserPage());
        }
        else
        {
            ErrorMessage.Text = "Something went wrong";
            RegexCheck.IsVisible = true;
        }
    }

    private async void OnLogOutClicked(object sender, EventArgs e)
    {
        Views.UserPage.SignedInUserEmail = null;
        await Navigation.PushAsync(new Views.UserPage());
    }
}