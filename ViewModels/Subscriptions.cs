namespace ProjectJournalist.ViewModels
{
    public class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Id del usuario suscrito
        public int JournalId { get; set; } // Id del journal al que está suscrito el usuario
    }
}
