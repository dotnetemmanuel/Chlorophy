namespace ChlorophyGUITests.ViewModels
{
    public class UserPageViewModel
    {
        public Models.User User { get; set; }

        public UserPageViewModel()
        {
            User = new();

            var task = Task.Run(() => ViewModels.MainPageViewModel.GetCurrentUser());
            task.Wait();
            var currentUser = task.Result;

            if (currentUser != null)
            {
                User = currentUser;
            }
        }
    }
}
