using ProjectJournalist.ViewModels;

namespace ProjectJournalist.Models
{
    public class User
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        public User() { }

    }
}
