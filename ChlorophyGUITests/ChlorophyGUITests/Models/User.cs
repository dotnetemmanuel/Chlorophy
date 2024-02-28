namespace ChlorophyGUITests.Models
{
    internal class User
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Models.PlantDetails> Plants { get; set; }
    }
}
