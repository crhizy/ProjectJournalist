using System.ComponentModel;

namespace ProjectJournalist.Models

{
    public class Journal
    {

        public int Id {  get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        public string URL { get; set; }

        [DisplayName("Id Researcher")]
        public int UserId { get; set; }
        public Journal() { }

    }
}
