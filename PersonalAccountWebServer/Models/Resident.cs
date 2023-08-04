using System.ComponentModel.DataAnnotations;

namespace PersonalAccountWebServer.Models
{
    public class Resident
    {
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public int IdRoom { get; set; }
    }
}