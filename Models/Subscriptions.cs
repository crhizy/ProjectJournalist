using System.ComponentModel.DataAnnotations;

namespace ProjectJournalist.Models
{

    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; } // Id del usuario suscrito
        public User User { get; set; }
        public int JournalId { get; set; } // Id del journal al que está suscrito el usuario
        public Journal Journal { get; set; }
    }
}